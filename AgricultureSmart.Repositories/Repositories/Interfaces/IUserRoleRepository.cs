using AgricultureSmart.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        Task AddAsync(UserRole userRole);

        Task<Role?> GetByNameAsync(string roleName);
    }
}
