using AgricultureSmart.Services.Models.NewCategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Interfaces
{
    public interface INewsCategoryService
    {
        Task<IEnumerable<NewsCategoryDto>> GetAllAsync(int page, int pageSize);
        Task<NewsCategoryDto> GetByIdAsync(int id);
        Task<NewsCategoryDto> CreateAsync(NewsCategoryCreateDto dto);
        Task<bool> UpdateAsync(int id, NewsCategoryUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
