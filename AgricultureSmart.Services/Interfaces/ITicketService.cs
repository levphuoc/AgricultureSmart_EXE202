using AgricultureSmart.Services.Models.TicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketViewModel>> GetAllAsync(int pageIndex, int pageSize);
        Task<TicketViewModel?> GetByIdAsync(int id);
        Task<ServiceResponse<TicketViewModel>> CreateAsync(CreateTicketModel model);
        Task<ServiceResponse<bool>> UpdateAsync(UpdateTicketModel model);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
        Task<ServiceResponse<TicketViewModel>> UpdateStatusAsync(UpdateTicketStatusModel model);
        Task<ServiceResponse<TicketViewModel>> CreateForFarmerAsync(
        int farmerId,
        CreateTicketForFarmerModel model);
        Task<IEnumerable<TicketFarmerViewModel>> GetByUserIdAsync(
        int userId);
    }
}
