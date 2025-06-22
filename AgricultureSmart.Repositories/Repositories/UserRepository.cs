using AgricultureSmart.Repositories.DbAgriContext;
using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories
{
    public class UserRepository : GenericRepository<Users>, IUserRepository
    {
        private readonly AgricultureSmartDbContext _dbContext;

        public UserRepository(AgricultureSmartDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Users?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Users?> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbContext.Users
                .AnyAsync(u => u.Email == email);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _dbContext.Users
                .AnyAsync(u => u.UserName == username);
        }

        public async Task<bool> PhoneNumberExistsAsync(string phoneNumber)
        {
            return await _dbContext.Users
                .AnyAsync(u => u.PhoneNumber == phoneNumber);
        }
        public async Task<Users?> GetByIdWithRolesAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
