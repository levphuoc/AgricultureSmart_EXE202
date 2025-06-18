using AgricultureSmart.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories.Interfaces
{
    public interface IBlogRepository
    {
        Task<(IEnumerable<Blog> Items, int TotalCount)> GetBlogsAsync(
            int pageNumber,
            int pageSize,
            string? title = null,
            int? authorId = null,
            int? categoryId = null,
            string? status = null);

        Task<int> CountBlogsByStatusAsync(string status);
        Task<Dictionary<string, int>> GetBlogStatusCountsAsync();
        Task<Blog?> GetByIdAsync(int id);
        Task<Blog?> GetBySlugAsync(string slug);
        Task<bool> CategoryExistsAsync(int categoryId);
        Task UpdateAsync(Blog blog);
    }
}
