using AgricultureSmart.Services.Models.FarmerModels;
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
        Task<IEnumerable<FarmerViewModel>> GetAllAsync(int pageIndex, int pageSize);
        Task<FarmerViewModel?> GetByIdAsync(int id);
        Task<ServiceResponse<FarmerViewModel>> CreateAsync(CreateFarmerModel model);
        Task<ServiceResponse<bool>> UpdateAsync(UpdateFarmerModel model);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
