using AgricultureSmart.Repositories.DbAgriContext;
using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class BlogService : IBlogService
    {
        private readonly AgricultureSmartDbContext _context;

        public BlogService(AgricultureSmartDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetAllBlogsAsync()
        {
            return await _context.Blogs
                .Include(b => b.Category)
                .Include(b => b.Author)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Blog>> GetBlogsByCategoryAsync(int categoryId)
        {
            return await _context.Blogs
                .Include(b => b.Category)
                .Include(b => b.Author)
                .Where(b => b.CategoryId == categoryId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            return await _context.Blogs
                .Include(b => b.Category)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Blog> GetBlogBySlugAsync(string slug)
        {
            return await _context.Blogs
                .Include(b => b.Category)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Slug == slug);
        }

        public async Task<Blog> CreateBlogAsync(int authorId, int categoryId, string title, string content, string featuredImage, string slug, string status)
        {
            try
            {
                // Check if category exists
                var categoryExists = await _context.BlogCategories.AnyAsync(c => c.Id == categoryId);
                if (!categoryExists)
                    throw new ArgumentException("Invalid category ID");

                // Check if slug already exists
                var slugExists = await _context.Blogs.AnyAsync(b => b.Slug == slug);
                if (slugExists)
                    throw new ArgumentException($"A blog with slug '{slug}' already exists. Please use a different slug.");

                // Normalize status value
                if (status != "draft" && status != "published" && status != "archived")
                {
                    status = "draft"; // Default to draft if invalid status
                }

                var blog = new Blog
                {
                    AuthorId = authorId,
                    CategoryId = categoryId,
                    Title = title,
                    Content = content,
                    FeaturedImage = featuredImage ?? "",
                    Slug = slug,
                    Status = status,
                    ViewCount = 0,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    PublishedAt = status == "published" ? DateTime.UtcNow : null
                };

                await _context.Blogs.AddAsync(blog);
                await _context.SaveChangesAsync();

                return await GetBlogByIdAsync(blog.Id);
            }
            catch (DbUpdateException dbEx)
            {
                // Log the detailed exception
                Console.WriteLine($"Database error: {dbEx.Message}");
                if (dbEx.InnerException != null)
                    Console.WriteLine($"Inner exception: {dbEx.InnerException.Message}");
                
                throw new Exception($"Failed to save blog: {dbEx.Message}", dbEx);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error creating blog: {ex.Message}");
                throw;
            }
        }

        public async Task<Blog> UpdateBlogAsync(int id, int categoryId, string title, string content, string featuredImage, string slug, string status)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
                return null;

            // Check if category exists
            var categoryExists = await _context.BlogCategories.AnyAsync(c => c.Id == categoryId);
            if (!categoryExists)
                throw new ArgumentException("Invalid category ID");

            blog.CategoryId = categoryId;
            blog.Title = title;
            blog.Content = content;
            blog.FeaturedImage = featuredImage ?? blog.FeaturedImage;
            blog.Slug = slug;
            blog.Status = status;
            blog.UpdatedAt = DateTime.UtcNow;

            // Update published date if status changed to published
            if (blog.Status == "published" && blog.PublishedAt == null)
            {
                blog.PublishedAt = DateTime.UtcNow;
            }

            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();

            return await GetBlogByIdAsync(blog.Id);
        }

        public async Task<bool> DeleteBlogAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
                return false;

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Blog> PublishBlogAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
                return null;

            blog.Status = "published";
            blog.PublishedAt = DateTime.UtcNow;
            blog.UpdatedAt = DateTime.UtcNow;

            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();

            return await GetBlogByIdAsync(blog.Id);
        }

        public async Task<Blog> UnpublishBlogAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
                return null;

            blog.Status = "draft";
            blog.UpdatedAt = DateTime.UtcNow;

            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();

            return await GetBlogByIdAsync(blog.Id);
        }

        public async Task<int> IncrementViewCountAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
                return 0;

            blog.ViewCount++;
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();

            return blog.ViewCount;
        }
    }
} 