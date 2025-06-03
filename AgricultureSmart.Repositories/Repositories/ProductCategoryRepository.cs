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
    public class ProductCategoryRepository : GenericRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(AgricultureSmartDbContext context) : base(context)
        {
        }

        public async Task<ProductCategory?> GetBySlugAsync(string slug)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Slug == slug);
        }

        public async Task<bool> SlugExistsAsync(string slug, int? excludeId = null)
        {
            if (excludeId.HasValue)
            {
                return await _dbSet.AnyAsync(c => c.Slug == slug && c.Id != excludeId.Value);
            }
            return await _dbSet.AnyAsync(c => c.Slug == slug);
        }
    }
} 