using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Services.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto?> GetByIdAsync(int id);
        Task<List<ProductDto>> GetAllAsync();
        Task<List<ProductDto>> GetByCategoryIdAsync(int categoryId);
        Task<List<ProductDto>> GetActiveProductsAsync();
        Task<List<ProductDto>> SearchProductsAsync(string searchTerm);
        Task<ProductDto> CreateAsync(CreateProductDto createDto);
        Task<ProductDto?> UpdateAsync(int id, UpdateProductDto updateDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> SkuExistsAsync(string sku, int? excludeId = null);
        
        // Pagination methods
        Task<ProductListResponse> GetPagedAsync(int pageNumber, int pageSize, string? searchTerm = null);
        Task<ProductListResponse> GetPagedByCategoryAsync(int categoryId, int pageNumber, int pageSize);
        Task<ProductListResponse> GetPagedActiveAsync(int pageNumber, int pageSize);

        Task<ProductListResponse> GetFilteredProductsAsync(
            int pageNumber,
            int pageSize,
            string? name = null,
            string? description = null,
            string? categoryName = null,
            bool? isActive = null,
            bool sortByDiscountPrice = false);
    }
} 