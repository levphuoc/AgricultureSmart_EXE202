using AgricultureSmart.Services.Models.NewModels;
using AgricultureSmart.Services.Models.NewModels.AgricultureSmart.Services.Models.NewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewGetAllDto>> GetAllAsync(int page, int pageSize);
        Task<PagedResult<NewGetAllDto>> SearchAsync(string? title, string? author,
                                            int? categoryId, int page, int pageSize);
        Task<NewsDto> GetByIdAsync(int id);
        Task<NewsDto> CreateAsync(NewsCreateDto dto);
        Task<bool> UpdateAsync(int id, NewsUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
