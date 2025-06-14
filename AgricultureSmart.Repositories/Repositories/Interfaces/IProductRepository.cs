using AgricultureSmart.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Product>> GetActiveProductsAsync();
        Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm);
        Task<bool> SkuExistsAsync(string sku, int? excludeId = null);

        Task<(IEnumerable<Product> Products, int TotalCount)> GetFilteredProductsAsync(
            int pageNumber,
            int pageSize,
            string? name = null,
            string? description = null,
            string? categoryName = null,
            bool? isActive = null,
            bool sortByDiscountPrice = false);

    }
} 