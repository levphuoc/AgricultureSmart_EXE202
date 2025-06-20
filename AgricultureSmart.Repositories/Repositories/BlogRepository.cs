﻿using AgricultureSmart.Repositories.DbAgriContext;
using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AgricultureSmartDbContext _context;
        public BlogRepository(AgricultureSmartDbContext context) => _context = context;

        public async Task<(IEnumerable<Blog> Items, int TotalCount)> GetBlogsAsync(
        int pageNumber,
        int pageSize,
        string? title = null,
        int? authorId = null,
        int? categoryId = null,
        string? status = null) 
        {
            var query = _context.Blogs
                                .Include(b => b.Category)
                                .Include(b => b.Author)
                                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(b => b.Title.Contains(title));

            if (authorId.HasValue)
                query = query.Where(b => b.AuthorId == authorId);

            if (categoryId.HasValue)
                query = query.Where(b => b.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(b => b.Status.ToLower().Contains(status.ToLower())); 

            var totalCount = await query.CountAsync();

            var items = await query.OrderByDescending(b => b.CreatedAt)
                                   .Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return (items, totalCount);
        }


        public async Task<int> CountBlogsByStatusAsync(string status)
        {
            return await _context.Blogs
                .Where(b => b.Status.ToLower() == status.ToLower())
                .CountAsync();
        }

        public async Task<Dictionary<string, int>> GetBlogStatusCountsAsync()
        {
            var result = await _context.Blogs
                .GroupBy(b => b.Status.ToLower())
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            var dict = result.ToDictionary(x => x.Status, x => x.Count);
            dict["all"] = dict.Values.Sum();
            return dict;
        }

        public async Task<Blog?> GetByIdAsync(int id)
        {
            return await _context.Blogs.FindAsync(id);
        }

        public async Task<Blog?> GetBySlugAsync(string slug)
        {
            return await _context.Blogs.FirstOrDefaultAsync(b => b.Slug == slug);
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await _context.BlogCategories.AnyAsync(c => c.Id == categoryId);
        }

        public async Task UpdateAsync(Blog blog)
        {
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
        }
    }
}
