using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Services.Models.BlogModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Interfaces
{
    public interface IBlogService
    {
        Task<BlogListResponse> SearchBlogsByTitlelAsync(
            int pageNumber,
            int pageSize,
            string? title = null,
            int? authorId = null,
            int? categoryId = null,
            string? status = null);

        Task<IEnumerable<Blog>> GetBlogsByCategoryAsync(int categoryId);
        Task<Blog> GetBlogByIdAsync(int id);
        Task<Blog> GetBlogBySlugAsync(string slug);
        Task<Blog> CreateBlogAsync(int authorId, int categoryId, string title, string content, string featuredImage, string slug, string status);
        Task<Blog> UpdateBlogAsync(int id, int categoryId, string title, string content, string featuredImage, string slug, string status);
        Task<bool> DeleteBlogAsync(int id);
        Task<Blog> PublishBlogAsync(int id);
        Task<Blog> UnpublishBlogAsync(int id);
        Task<int> IncrementViewCountAsync(int id);
        Task<BlogListResponse> GetBlogsByUserIdAsync(
        int pageNumber,
        int pageSize,
        int authorId);

        Task<int> CountBlogsByStatusAsync(string status);
        Task<BlogStatusStatisticsResponse> GetBlogStatusStatisticsAsync();
    }
}