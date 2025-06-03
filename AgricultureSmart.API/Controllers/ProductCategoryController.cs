using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.ProductCategoryModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgricultureSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        // GET: api/ProductCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetAllCategories()
        {
            var categories = await _productCategoryService.GetAllAsync();
            return Ok(categories);
        }

        // GET: api/ProductCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryDto>> GetCategoryById(int id)
        {
            var category = await _productCategoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // GET: api/ProductCategory/slug/hat-giong
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<ProductCategoryDto>> GetCategoryBySlug(string slug)
        {
            var category = await _productCategoryService.GetBySlugAsync(slug);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST: api/ProductCategory
        [HttpPost]
        public async Task<ActionResult<ProductCategoryDto>> CreateCategory([FromBody] CreateProductCategoryDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if slug exists
            if (await _productCategoryService.SlugExistsAsync(createDto.Slug))
            {
                ModelState.AddModelError("Slug", "Slug already exists");
                return BadRequest(ModelState);
            }

            var createdCategory = await _productCategoryService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory);
        }

        // PUT: api/ProductCategory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateProductCategoryDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if slug exists (excluding current category)
            if (await _productCategoryService.SlugExistsAsync(updateDto.Slug, id))
            {
                ModelState.AddModelError("Slug", "Slug already exists");
                return BadRequest(ModelState);
            }

            var updatedCategory = await _productCategoryService.UpdateAsync(id, updateDto);
            if (updatedCategory == null)
            {
                return NotFound();
            }

            return Ok(updatedCategory);
        }

        // DELETE: api/ProductCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _productCategoryService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/ProductCategory/check-slug?slug=hat-giong&excludeId=1
        [HttpGet("check-slug")]
        public async Task<ActionResult<bool>> CheckSlugExists([FromQuery] string slug, [FromQuery] int? excludeId = null)
        {
            var exists = await _productCategoryService.SlugExistsAsync(slug, excludeId);
            return Ok(exists);
        }
    }
} 