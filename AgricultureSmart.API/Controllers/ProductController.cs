using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.ProductModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AgricultureSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // GET: api/Product/category/5
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(int categoryId)
        {
            var products = await _productService.GetByCategoryIdAsync(categoryId);
            return Ok(products);
        }

        // GET: api/Product/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetActiveProducts()
        {
            var products = await _productService.GetActiveProductsAsync();
            return Ok(products);
        }

        // GET: api/Product/search?term=seed
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> SearchProducts([FromQuery] string term)
        {
            var products = await _productService.SearchProductsAsync(term);
            return Ok(products);
        }

        // GET: api/Product/paged?pageNumber=1&pageSize=10&searchTerm=seed
        [HttpGet("paged")]
        public async Task<ActionResult<ProductListResponse>> GetPagedProducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0");
            }

            var pagedProducts = await _productService.GetPagedAsync(pageNumber, pageSize, searchTerm);
            return Ok(pagedProducts);
        }

        // GET: api/Product/category/5/paged?pageNumber=1&pageSize=10
        [HttpGet("category/{categoryId}/paged")]
        public async Task<ActionResult<ProductListResponse>> GetPagedProductsByCategory(
            int categoryId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0");
            }

            try
            {
                var pagedProducts = await _productService.GetPagedByCategoryAsync(categoryId, pageNumber, pageSize);
                return Ok(pagedProducts);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/Product/active/paged?pageNumber=1&pageSize=10
        [HttpGet("active/paged")]
        public async Task<ActionResult<ProductListResponse>> GetPagedActiveProducts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number and page size must be greater than 0");
            }

            var pagedProducts = await _productService.GetPagedActiveAsync(pageNumber, pageSize);
            return Ok(pagedProducts);
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if SKU exists and is not empty
            if (!string.IsNullOrWhiteSpace(createDto.SKU) && await _productService.SkuExistsAsync(createDto.SKU))
            {
                ModelState.AddModelError("SKU", "SKU already exists");
                return BadRequest(ModelState);
            }

            try
            {
                var createdProduct = await _productService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("CategoryId", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if SKU exists (excluding current product) and is not empty
            if (!string.IsNullOrWhiteSpace(updateDto.SKU) && await _productService.SkuExistsAsync(updateDto.SKU, id))
            {
                ModelState.AddModelError("SKU", "SKU already exists");
                return BadRequest(ModelState);
            }

            try
            {
                var updatedProduct = await _productService.UpdateAsync(id, updateDto);
                if (updatedProduct == null)
                {
                    return NotFound();
                }

                return Ok(updatedProduct);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("CategoryId", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/Product/check-sku?sku=ABC123&excludeId=1
        [HttpGet("check-sku")]
        public async Task<ActionResult<bool>> CheckSkuExists([FromQuery] string sku, [FromQuery] int? excludeId = null)
        {
            var exists = await _productService.SkuExistsAsync(sku, excludeId);
            return Ok(exists);
        }

        /// <summary>
        /// Get filtered products for regular users (only active products)
        /// </summary>
        [HttpGet("public")]
        public async Task<ActionResult<ProductListResponse>> GetPublicProducts([FromQuery] ProductFilterRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Validate request parameters
                if (request.PageNumber < 1)
                {
                    request.PageNumber = 1;
                }

                if (request.PageSize < 1)
                {
                    request.PageSize = 10;
                }

                // Log request details
                _logger.LogInformation("Processing GetPublicProducts request: {@Request}", new
                {
                    request.PageNumber,
                    request.PageSize,
                    request.Name,
                    request.Description,
                    request.CategoryName,
                    request.SortByDiscountPrice
                });

                // Force IsActive to true for public endpoint
                var products = await _productService.GetFilteredProductsAsync(
                    request.PageNumber,
                    request.PageSize,
                    request.Name,
                    request.Description,
                    request.CategoryName,
                    isActive: true,
                    request.SortByDiscountPrice);

                _logger.LogInformation("Successfully retrieved {Count} products for public API", 
                    products.Items?.Count ?? 0);

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetPublicProducts: {ErrorMessage}", ex.Message);
                return StatusCode(500, new { error = "An error occurred while retrieving products. Please try again later." });
            }
        }

        /// <summary>
        /// Get filtered products for admin users (both active and inactive products)
        /// </summary>
        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductListResponse>> GetAdminProducts([FromQuery] ProductFilterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var products = await _productService.GetFilteredProductsAsync(
                request.PageNumber,
                request.PageSize,
                request.Name,
                request.Description,
                request.CategoryName,
                request.IsActive,
                request.SortByDiscountPrice);

            return Ok(products);
        }
    }
}