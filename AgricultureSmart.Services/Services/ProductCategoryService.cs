using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.ProductCategoryModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductCategoryService> _logger;

        public ProductCategoryService(
            IProductCategoryRepository repository,
            IMapper mapper,
            ILogger<ProductCategoryService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductCategoryDto?> GetByIdAsync(int id)
        {
            try
            {
                var category = await _repository.GetByIdAsync(id);
                return category != null ? _mapper.Map<ProductCategoryDto>(category) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product category with ID {Id}", id);
                throw;
            }
        }

        public async Task<ProductCategoryDto?> GetBySlugAsync(string slug)
        {
            try
            {
                var category = await _repository.GetBySlugAsync(slug);
                return category != null ? _mapper.Map<ProductCategoryDto>(category) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product category with slug {Slug}", slug);
                throw;
            }
        }

        public async Task<List<ProductCategoryDto>> GetAllAsync()
        {
            try
            {
                var categories = await _repository.GetAllAsync();
                return _mapper.Map<List<ProductCategoryDto>>(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all product categories");
                throw;
            }
        }

        public async Task<ProductCategoryDto> CreateAsync(CreateProductCategoryDto createDto)
        {
            try
            {
                var category = _mapper.Map<ProductCategory>(createDto);
                await _repository.AddAsync(category);
                return _mapper.Map<ProductCategoryDto>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product category");
                throw;
            }
        }

        public async Task<ProductCategoryDto?> UpdateAsync(int id, UpdateProductCategoryDto updateDto)
        {
            try
            {
                var existingCategory = await _repository.GetByIdAsync(id);
                if (existingCategory == null)
                {
                    return null;
                }

                _mapper.Map(updateDto, existingCategory);
                await _repository.UpdateAsync(existingCategory);
                return _mapper.Map<ProductCategoryDto>(existingCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product category with ID {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var category = await _repository.GetByIdAsync(id);
                if (category == null)
                {
                    return false;
                }

                await _repository.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product category with ID {Id}", id);
                throw;
            }
        }

        public async Task<bool> SlugExistsAsync(string slug, int? excludeId = null)
        {
            try
            {
                return await _repository.SlugExistsAsync(slug, excludeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if slug {Slug} exists", slug);
                throw;
            }
        }
    }
} 