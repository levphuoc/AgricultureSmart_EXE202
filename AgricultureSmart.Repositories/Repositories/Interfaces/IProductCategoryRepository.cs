using AgricultureSmart.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories.Interfaces
{
    public interface IProductCategoryRepository : IGenericRepository<ProductCategory>
    {
        Task<ProductCategory?> GetBySlugAsync(string slug);
        Task<bool> SlugExistsAsync(string slug, int? excludeId = null);
    }
} 