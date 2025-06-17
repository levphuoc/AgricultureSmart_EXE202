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
    }
}
