using AgricultureSmart.API.Models;
using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AgricultureSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCategoryController : ControllerBase
    {
        private readonly IBlogCategoryService _blogCategoryService;

        public BlogCategoryController(IBlogCategoryService blogCategoryService)
        {
            _blogCategoryService = blogCategoryService;
        }

        // GET: api/BlogCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogCategoryModel>>> GetCategories()
        {
            var categories = await _blogCategoryService.GetAllCategoriesAsync();
            var result = categories.Select(c => new BlogCategoryModel
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Slug = c.Slug,
                IsActive = c.IsActive
            });

            return Ok(result);
        }

        // GET: api/BlogCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogCategoryModel>> GetCategory(int id)
        {
            var category = await _blogCategoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(new BlogCategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Slug = category.Slug,
                IsActive = category.IsActive
            });
        }

        // GET: api/BlogCategory/slug/agriculture
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<BlogCategoryModel>> GetCategoryBySlug(string slug)
        {
            var category = await _blogCategoryService.GetCategoryBySlugAsync(slug);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(new BlogCategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Slug = category.Slug,
                IsActive = category.IsActive
            });
        }

        // POST: api/BlogCategory
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BlogCategoryModel>> CreateCategory(BlogCategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var category = await _blogCategoryService.CreateCategoryAsync(
                    model.Name,
                    model.Description,
                    model.Slug);

                return CreatedAtAction(
                    nameof(GetCategory),
                    new { id = category.Id },
                    new BlogCategoryModel
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Description = category.Description,
                        Slug = category.Slug,
                        IsActive = category.IsActive
                    });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // PUT: api/BlogCategory/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id, BlogCategoryModel model)
        {
            if (id != model.Id)
            {
                return BadRequest("ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var category = await _blogCategoryService.UpdateCategoryAsync(
                    id,
                    model.Name,
                    model.Description,
                    model.Slug,
                    model.IsActive);

                if (category == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // DELETE: api/BlogCategory/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _blogCategoryService.DeleteCategoryAsync(id);

            if (!result)
            {
                return BadRequest(new { Message = "Cannot delete category. It may not exist or has associated blogs." });
            }

            return NoContent();
        }
    }
} 