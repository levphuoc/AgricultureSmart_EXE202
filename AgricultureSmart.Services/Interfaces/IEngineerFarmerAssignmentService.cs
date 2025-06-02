using AgricultureSmart.Services.Models.AssignmentModel;
using AgricultureSmart.Services.Models.TicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Interfaces
{
    public interface IEngineerFarmerAssignmentService
    {
        Task<IEnumerable<EngineerFarmerAssignmentViewModel>> GetAllAsync(int pageIndex, int pageSize);
        Task<EngineerFarmerAssignmentViewModel?> GetByIdAsync(int id);
        Task<IEnumerable<EngineerFarmerAssignmentViewModel>> GetByEngineerIdAsync(int engineerId, int pageIndex, int pageSize);
        Task<IEnumerable<EngineerFarmerAssignmentViewModel>> GetByFarmerIdAsync(int farmerId, int pageIndex, int pageSize);
        Task<ServiceResponse<EngineerFarmerAssignmentViewModel>> CreateAsync(CreateAssignmentModel model);
        Task<ServiceResponse<bool>> UpdateAsync(UpdateAssignmentModel model);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
