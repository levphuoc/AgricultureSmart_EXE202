using AgricultureSmart.Services.Models.ProductCategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Interfaces
{
    public interface IProductCategoryService
    {
        Task<ProductCategoryDto?> GetByIdAsync(int id);
        Task<ProductCategoryDto?> GetBySlugAsync(string slug);
        Task<List<ProductCategoryDto>> GetAllAsync();
        Task<ProductCategoryDto> CreateAsync(CreateProductCategoryDto createDto);
        Task<ProductCategoryDto?> UpdateAsync(int id, UpdateProductCategoryDto updateDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> SlugExistsAsync(string slug, int? excludeId = null);
    }
} 