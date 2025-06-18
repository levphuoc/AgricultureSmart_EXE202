using AgricultureSmart.API.Models;
using AgricultureSmart.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgricultureSmart.Services.Models.BlogModels;
using AgricultureSmart.Services.Extension;

namespace AgricultureSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("search")]
        public async Task<ActionResult<BlogListResponse>> GetBlogs(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? title = null,
        [FromQuery] int? authorId = null,
        [FromQuery] int? categoryId = null,
        [FromQuery] string? status = null)
        {
            var result = await _blogService.SearchBlogsByTitlelAsync(
                             pageNumber, pageSize,
                             title, authorId, categoryId, status); 

            return Ok(result);
        }


        // GET: api/Blog/category/5
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<BlogListItem>>> GetBlogsByCategory(int categoryId)
        {
            var blogs = await _blogService.GetBlogsByCategoryAsync(categoryId);
            var result = blogs.Select(b => new BlogListItem
            {
                Id = b.Id,
                Title = b.Title,
                Slug = b.Slug,
                Status = b.Status,
                CategoryName = b.Category?.Name,
                AuthorName = b.Author?.UserName,
                CreatedAt = b.CreatedAt,
                PublishedAt = b.PublishedAt,
                ViewCount = b.ViewCount
            });

            return Ok(result);
        }

        // GET: api/Blog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDetailModel>> GetBlog(int id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(new BlogDetailModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                FeaturedImage = blog.FeaturedImage,
                Slug = blog.Slug,
                Status = blog.Status,
                ViewCount = blog.ViewCount,
                CategoryName = blog.Category?.Name,
                CategoryId = blog.CategoryId,
                AuthorName = blog.Author?.UserName,
                AuthorId = blog.AuthorId,
                CreatedAt = blog.CreatedAt,
                UpdatedAt = blog.UpdatedAt,
                PublishedAt = blog.PublishedAt
            });
        }

        // GET: api/Blog/slug/my-blog-post
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<BlogDetailModel>> GetBlogBySlug(string slug)
        {
            var blog = await _blogService.GetBlogBySlugAsync(slug);

            if (blog == null)
            {
                return NotFound();
            }

            // Increment view count for public access
            if (blog.Status == "published")
            {
                await _blogService.IncrementViewCountAsync(blog.Id);
            }

            return Ok(new BlogDetailModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                FeaturedImage = blog.FeaturedImage,
                Slug = blog.Slug,
                Status = blog.Status,
                ViewCount = blog.ViewCount + 1, // Add the current view
                CategoryName = blog.Category?.Name,
                CategoryId = blog.CategoryId,
                AuthorName = blog.Author?.UserName,
                AuthorId = blog.AuthorId,
                CreatedAt = blog.CreatedAt,
                UpdatedAt = blog.UpdatedAt,
                PublishedAt = blog.PublishedAt
            });
        }

        // POST: api/Blog
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<BlogDetailModel>> CreateBlog(BlogCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
                
                // Using BlogCreateModel which doesn't have an ID field
                var blog = await _blogService.CreateBlogAsync(
                    userId,
                    model.CategoryId,
                    model.Title,
                    model.Content,
                    model.FeaturedImage,
                    model.Slug,
                    model.Status);

                return CreatedAtAction(
                    nameof(GetBlog),
                    new { id = blog.Id },
                    new BlogDetailModel
                    {
                        Id = blog.Id,
                        Title = blog.Title,
                        Content = blog.Content,
                        FeaturedImage = blog.FeaturedImage,
                        Slug = blog.Slug,
                        Status = blog.Status,
                        ViewCount = blog.ViewCount,
                        CategoryName = blog.Category?.Name,
                        CategoryId = blog.CategoryId,
                        AuthorName = blog.Author?.UserName,
                        AuthorId = blog.AuthorId,
                        CreatedAt = blog.CreatedAt,
                        UpdatedAt = blog.UpdatedAt,
                        PublishedAt = blog.PublishedAt
                    });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // PUT: api/Blog/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(int id, BlogModel model)
        {
            // Always use the ID from the route parameter, ignore the ID in the model
            // This allows updating a blog without requiring the ID in the model
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var blog = await _blogService.UpdateBlogAsync(
                    id,
                    model.CategoryId,
                    model.Title,
                    model.Content,
                    model.FeaturedImage,
                    model.Slug,
                    model.Status);

                if (blog == null)
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

        // DELETE: api/Blog/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var result = await _blogService.DeleteBlogAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Blog/5/publish
        [HttpPost("{id}/publish")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> PublishBlog(int id)
        {
            var blog = await _blogService.PublishBlogAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Blog/5/unpublish
        [HttpPost("{id}/unpublish")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnpublishBlog(int id)
        {
            var blog = await _blogService.UnpublishBlogAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize]
        [HttpGet("my-blogs")]
        public async Task<IActionResult> GetMyBlogs(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? title = null,
        /*[FromQuery] int? authorId = null,*/
        [FromQuery] int? categoryId = null,
        [FromQuery] string? status = null)
        {
            int userId = User.GetUserId();
            if (userId == 0)
                return Unauthorized(new { success = false, message = "Invalid or missing user token." });

            var result = await _blogService.GetBlogsByUserIdAsync(pageNumber, pageSize, title, userId, categoryId, status);

            return Ok(result);
        }

        /// <summary>
        /// Count blogs by status (e.g., draft, published, archived)
        /// </summary>
        /// <param name="status">Blog status</param>
        /// <returns>Number of blogs with the given status</returns>
        [HttpGet("count-by-status")]
        public async Task<IActionResult> CountBlogsByStatus([FromQuery] string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return BadRequest(new { success = false, message = "Status is required." });
            }

            var count = await _blogService.CountBlogsByStatusAsync(status);
            return Ok(new
            {
                success = true,
                status,
                count
            });
        }

        [HttpGet("statistics/status")]
        public async Task<ActionResult<BlogStatusStatisticsResponse>> GetBlogStatusStatistics()
        {
            var result = await _blogService.GetBlogStatusStatisticsAsync();
            return Ok(result);
        }
    }
} 