using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.ProductModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IProductCategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            IProductRepository repository,
            IProductCategoryRepository categoryRepository,
            IMapper mapper,
            ILogger<ProductService> logger)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            try
            {
                var product = await _repository.GetByIdAsync(id);
                return product != null ? _mapper.Map<ProductDto>(product) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting product with ID {Id}", id);
                throw;
            }
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            try
            {
                var products = await _repository.GetAllAsync();
                return _mapper.Map<List<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all products");
                throw;
            }
        }

        public async Task<List<ProductDto>> GetByCategoryIdAsync(int categoryId)
        {
            try
            {
                var products = await _repository.GetProductsByCategoryIdAsync(categoryId);
                return _mapper.Map<List<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting products for category ID {CategoryId}", categoryId);
                throw;
            }
        }

        public async Task<List<ProductDto>> GetActiveProductsAsync()
        {
            try
            {
                var products = await _repository.GetActiveProductsAsync();
                return _mapper.Map<List<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active products");
                throw;
            }
        }

        public async Task<List<ProductDto>> SearchProductsAsync(string searchTerm)
        {
            try
            {
                var products = await _repository.SearchProductsAsync(searchTerm);
                return _mapper.Map<List<ProductDto>>(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching products with term {SearchTerm}", searchTerm);
                throw;
            }
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto createDto)
        {
            try
            {
                // Validate that the category exists
                var category = await _categoryRepository.GetByIdAsync(createDto.CategoryId);
                if (category == null)
                {
                    throw new ArgumentException($"Category with ID {createDto.CategoryId} does not exist");
                }

                var product = _mapper.Map<Product>(createDto);
                await _repository.AddAsync(product);
                
                // Fetch the product with category included
                var createdProduct = await _repository.GetByIdAsync(product.Id);
                return _mapper.Map<ProductDto>(createdProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating product");
                throw;
            }
        }

        public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto updateDto)
        {
            try
            {
                // Validate that the product exists
                var existingProduct = await _repository.GetByIdAsync(id);
                if (existingProduct == null)
                {
                    return null;
                }

                // Validate that the category exists
                var category = await _categoryRepository.GetByIdAsync(updateDto.CategoryId);
                if (category == null)
                {
                    throw new ArgumentException($"Category with ID {updateDto.CategoryId} does not exist");
                }

                _mapper.Map(updateDto, existingProduct);
                await _repository.UpdateAsync(existingProduct);
                
                // Fetch the updated product with category included
                var updatedProduct = await _repository.GetByIdAsync(id);
                return _mapper.Map<ProductDto>(updatedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with ID {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var product = await _repository.GetByIdAsync(id);
                if (product == null)
                {
                    return false;
                }

                await _repository.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product with ID {Id}", id);
                throw;
            }
        }

        public async Task<bool> SkuExistsAsync(string sku, int? excludeId = null)
        {
            try
            {
                return await _repository.SkuExistsAsync(sku, excludeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking if SKU {Sku} exists", sku);
                throw;
            }
        }

        public async Task<ProductListResponse> GetPagedAsync(int pageNumber, int pageSize, string? searchTerm = null)
        {
            try
            {
                Expression<Func<Product, bool>>? predicate = null;
                
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    var term = searchTerm.ToLower();
                    predicate = p => p.Name.ToLower().Contains(term) || 
                                     p.Description.ToLower().Contains(term) || 
                                     p.SKU.ToLower().Contains(term) ||
                                     p.Category.Name.ToLower().Contains(term);
                }

                var (items, totalCount) = await _repository.GetPagedAsync(
                    pageNumber,
                    pageSize,
                    predicate,
                    q => q.OrderByDescending(p => p.CreatedAt)
                );

                var response = new ProductListResponse
                {
                    Items = _mapper.Map<List<ProductDto>>(items),
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paged products");
                throw;
            }
        }

        public async Task<ProductListResponse> GetPagedByCategoryAsync(int categoryId, int pageNumber, int pageSize)
        {
            try
            {
                // Verify that the category exists
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null)
                {
                    throw new ArgumentException($"Category with ID {categoryId} does not exist");
                }

                Expression<Func<Product, bool>> predicate = p => p.CategoryId == categoryId;

                var (items, totalCount) = await _repository.GetPagedAsync(
                    pageNumber,
                    pageSize,
                    predicate,
                    q => q.OrderByDescending(p => p.CreatedAt)
                );

                var response = new ProductListResponse
                {
                    Items = _mapper.Map<List<ProductDto>>(items),
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paged products for category ID {CategoryId}", categoryId);
                throw;
            }
        }

        public async Task<ProductListResponse> GetPagedActiveAsync(int pageNumber, int pageSize)
        {
            try
            {
                Expression<Func<Product, bool>> predicate = p => p.IsActive;

                var (items, totalCount) = await _repository.GetPagedAsync(
                    pageNumber,
                    pageSize,
                    predicate,
                    q => q.OrderByDescending(p => p.CreatedAt)
                );

                var response = new ProductListResponse
                {
                    Items = _mapper.Map<List<ProductDto>>(items),
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting paged active products");
                throw;
            }
        }
    }
} 