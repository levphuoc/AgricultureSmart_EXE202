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
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly AgricultureSmartDbContext _context;

        public BlogCategoryService(AgricultureSmartDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BlogCategory>> GetAllCategoriesAsync()
        {
            return await _context.BlogCategories
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<BlogCategory> GetCategoryByIdAsync(int id)
        {
            return await _context.BlogCategories.FindAsync(id);
        }

        public async Task<BlogCategory> GetCategoryBySlugAsync(string slug)
        {
            return await _context.BlogCategories
                .FirstOrDefaultAsync(c => c.Slug == slug);
        }

        public async Task<BlogCategory> CreateCategoryAsync(string name, string description, string slug)
        {
            var category = new BlogCategory
            {
                Name = name,
                Description = description,
                Slug = slug,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _context.BlogCategories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<BlogCategory> UpdateCategoryAsync(int id, string name, string description, string slug, bool isActive)
        {
            var category = await _context.BlogCategories.FindAsync(id);
            if (category == null)
                return null;

            category.Name = name;
            category.Description = description;
            category.Slug = slug;
            category.IsActive = isActive;

            _context.BlogCategories.Update(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            // Check if category has blogs
            var hasBlogs = await _context.Blogs.AnyAsync(b => b.CategoryId == id);
            if (hasBlogs)
                return false;

            var category = await _context.BlogCategories.FindAsync(id);
            if (category == null)
                return false;

            _context.BlogCategories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }
    }
} 