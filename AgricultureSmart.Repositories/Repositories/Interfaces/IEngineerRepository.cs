using AgricultureSmart.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories.Interfaces
{
    public interface IEngineerRepository
    {
        Task<(IEnumerable<Engineer> Items, int TotalCount)> SearchAsync(
            int pageNumber,
            int pageSize,
            string? specialization,
            int? experienceYears);
    }
}
