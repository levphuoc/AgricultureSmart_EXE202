using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Services.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Interfaces
{
    public interface IAuthServices
    {
        Task<(bool Success, string Message, Users User)> RegisterUserAsync(string username, string email, string password, string address, string phoneNumber, int roleId = 3);
        Task<(bool Success, string Message, Users User, string Token, DateTime Expiration, string RoleName)> LoginAsync(string username, string password);
        string GenerateRefreshToken();
        Task<(bool Success, string Message, string Token, DateTime Expiration)> RefreshTokenAsync(string refreshToken);
        Task<UserWithRolesViewModel?> GetUserProfileAsync(int userId);
    }
}
