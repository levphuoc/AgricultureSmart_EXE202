using AgricultureSmart.Services.Models.EngineerModel;
using AgricultureSmart.Services.Models.TicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Interfaces
{
    public interface IEngineerService
    {
        Task<IEnumerable<EngineerViewModel>> GetAllAsync(int pageIndex, int pageSize);
        Task<EngineerViewModel?> GetByIdAsync(int id);
        Task<ServiceResponse<EngineerViewModel>> CreateAsync(CreateEngineerModel model);
        Task<ServiceResponse<bool>> UpdateAsync(UpdateEngineerModel model);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
