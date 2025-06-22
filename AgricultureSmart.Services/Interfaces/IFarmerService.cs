using AgricultureSmart.Services.Models.FarmerModels;
using AgricultureSmart.Services.Models.PagedListResponseModels;
using AgricultureSmart.Services.Models.TicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Interfaces
{
    public interface IFarmerService
    {
        Task<PagedListResponse<FarmerViewModel>> SearchAsync(
        int pageNumber,
        int pageSize,
        string? farmLocation,
        decimal? farmSize,
        string? cropTypes);
        Task<FarmerViewModel?> GetByIdAsync(int id);
        Task<ServiceResponse<FarmerViewModel>> CreateAsync(CreateFarmerModel model);
        Task<ServiceResponse<bool>> UpdateAsync(int id, UpdateFarmerModel model);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
