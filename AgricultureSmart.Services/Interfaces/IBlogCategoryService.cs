using AgricultureSmart.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Interfaces
{
    public interface IBlogCategoryService
    {
        Task<IEnumerable<BlogCategory>> GetAllCategoriesAsync();
        Task<BlogCategory> GetCategoryByIdAsync(int id);
        Task<BlogCategory> GetCategoryBySlugAsync(string slug);
        Task<BlogCategory> CreateCategoryAsync(string name, string description, string slug);
        Task<BlogCategory> UpdateCategoryAsync(int id, string name, string description, string slug, bool isActive);
        Task<bool> DeleteCategoryAsync(int id);
    }
} 