using AgricultureSmart.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories.Interfaces
{
    public interface IEngineerFarmerAssignmentRepository
    {
        Task<bool> AnyAsync(Expression<Func<EngineerFarmerAssignment, bool>> predicate);
        Task<List<EngineerFarmerAssignment>> GetAllAsync();
    }
}
