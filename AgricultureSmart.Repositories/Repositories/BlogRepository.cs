using AgricultureSmart.Repositories.DbAgriContext;
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
            int? categoryId = null)
        {
            var query = _context.Blogs
                                .Include(b => b.Category)
                                .Include(b => b.Author)
                                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(p => p.Title.Contains(title));

            if (authorId.HasValue)
                query = query.Where(b => b.AuthorId == authorId);

            if (categoryId.HasValue)
                query = query.Where(b => b.CategoryId == categoryId);

            var totalCount = await query.CountAsync();

            var items = await query.OrderByDescending(b => b.CreatedAt)
                                   .Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return (items, totalCount);
        }
    }
}
