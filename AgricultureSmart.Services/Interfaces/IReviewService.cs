using AgricultureSmart.Services.Models.ReviewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Interfaces
{
    public interface IReviewService
    {
        /*Task<IEnumerable<ReviewDto>> GetAllAsync(int page, int pageSize);*/
        Task<ReviewListResponse> GetAllAsync(int pageNumber, int pageSize);
        Task<ReviewDto?> GetByIdAsync(int id);
        Task<ReviewDto> CreateAsync(ReviewCreateDto dto);
        Task<bool> UpdateAsync(int id, ReviewUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ReviewDto>> GetByProductIdAsync(int productId);
    }
}
