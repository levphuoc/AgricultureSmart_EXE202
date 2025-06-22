using AgricultureSmart.Services.Models.EngineerModel;
using AgricultureSmart.Services.Models.PagedListResponseModels;
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
        Task<PagedListResponse<EngineerViewModel>> SearchAsync(
            int pageNumber,
            int pageSize,
            string? specialization,
            int? experienceYears);
        Task<EngineerViewModel?> GetByIdAsync(int id);
        Task<ServiceResponse<EngineerViewModel>> CreateAsync(CreateEngineerModel model);
        Task<ServiceResponse<bool>> UpdateAsync(int id, UpdateEngineerModel model);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
