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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AgricultureSmartDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _dbSet
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            return await _dbSet
                .Where(p => p.IsActive)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllAsync();

            var term = searchTerm.ToLower();
            return await _dbSet
                .Where(p => p.Name.ToLower().Contains(term) ||
                           p.Description.ToLower().Contains(term) ||
                           p.SKU.ToLower().Contains(term) ||
                           p.Category.Name.ToLower().Contains(term))
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<bool> SkuExistsAsync(string sku, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(sku))
                return false;

            if (excludeId.HasValue)
            {
                return await _dbSet.AnyAsync(p => p.SKU == sku && p.Id != excludeId.Value);
            }
            return await _dbSet.AnyAsync(p => p.SKU == sku);
        }

        // Override GetByIdAsync to include Category
        public async new Task<Product?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Override GetAllAsync to include Category
        public async new Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbSet
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetFilteredProductsAsync(
            int pageNumber,
            int pageSize,
            string? name = null,
            string? description = null,
            string? categoryName = null,
            bool? isActive = null,
            bool sortByDiscountPrice = false)
        {
            IQueryable<Product> query = _dbSet.Include(p => p.Category);

            // Apply filters
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                query = query.Where(p => p.Description.Contains(description));
            }

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                query = query.Where(p => p.Category.Name.Contains(categoryName));
            }

            if (isActive.HasValue)
            {
                query = query.Where(p => p.IsActive == isActive.Value);
            }

            // Get total count before pagination
            int totalCount = await query.CountAsync();

            // Apply sorting
            if (sortByDiscountPrice)
            {
                // Don't filter out null discount prices, just order them last
                query = query
                    .OrderByDescending(p => p.DiscountPrice != null) // Non-null discount prices first
                    .ThenByDescending(p => p.DiscountPrice)         // Then by discount price value
                    .ThenByDescending(p => p.Price);               // Finally by regular price
            }
            else
            {
                query = query
                    .OrderByDescending(p => p.CreatedAt); 
            }

            // Apply pagination
            var products = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalCount);
        }
    }
} 