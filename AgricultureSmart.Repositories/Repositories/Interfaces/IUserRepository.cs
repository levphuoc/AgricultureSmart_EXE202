using AgricultureSmart.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<Users>
    {
        Task<Users?> GetByEmailAsync(string email);
        Task<Users?> GetByUsernameAsync(string username);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> PhoneNumberExistsAsync(string phoneNumber);
    }
}
