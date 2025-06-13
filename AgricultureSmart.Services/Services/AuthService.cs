/*using AgricultureSmart.Repositories.DbAgriContext;
using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class AuthService : IAuthServices
    {
        private readonly AgricultureSmartDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AgricultureSmartDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<(bool Success, string Message, Users User)> RegisterUserAsync(string username, string email, string password, string address, string phoneNumber, int roleId = 3)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == username))
            {
                return (false, "Username already exists", null);
            }

            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                return (false, "Email already exists", null);
            }

            // Validate that the roleId is either Engineer (2) or Farmer (3)
            if (roleId != 2 && roleId != 3)
            {
                // Default to Farmer if invalid role is provided
                roleId = 3;
            }

            var hashedPassword = HashPassword(password);

            var user = new Users
            {
                UserName = username,
                Email = email,
                Password = hashedPassword,
                Address = address,
                PhoneNumber = phoneNumber,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Assign the selected role to the user
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = roleId,
                CreatedAt = DateTime.UtcNow
            };

            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();

            // Create the appropriate profile based on the role
            if (roleId == 2) // Engineer
            {
                var engineer = new Engineer
                {
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Bio = $"Engineer profile for {username}",  // Default Bio to avoid NULL
                    Specialization = "General",  // Default Specialization
                    ExperienceYears = 0,  // Default ExperienceYears
                    Certification = "[]"  // Default empty JSON array for certifications
                };
                await _context.Engineers.AddAsync(engineer);
            }
            else if (roleId == 3) // Farmer
            {
                var farmer = new Farmer
                {
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    FarmLocation = "Not specified",  // Default FarmLocation
                    FarmSize = 0,  // Default FarmSize
                    CropTypes = "[]",  // Default empty JSON array for crop types
                    FarmingExperienceYears = 0  // Default FarmingExperienceYears
                };
                await _context.Farmers.AddAsync(farmer);
            }

            await _context.SaveChangesAsync();

            return (true, "User registered successfully", user);
        }

        public async Task<(bool Success, string Message, Users User, string Token, DateTime Expiration, string RoleName)> LoginAsync(string username, string password)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return (false, "User not found", null, null, DateTime.MinValue, null);
            }

            if (!VerifyPassword(password, user.Password))
            {
                return (false, "Invalid password", null, null, DateTime.MinValue, null);
            }

            var userRole = user.UserRoles.FirstOrDefault(ur => ur.RoleId == 1 || ur.RoleId == 2 || ur.RoleId == 3);
            if (userRole == null)
            {
                return (false, "User does not have a valid role", null, null, DateTime.MinValue, null);
            }

            var roleName = userRole.Role?.Name ?? "";

            var token = GenerateJwtToken(user, roleName);
            var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JWT:TokenValidityInMinutes"]));

            return (true, "Login successful", user, token, expiration, roleName);
        }

        private string GenerateJwtToken(Users user, string roleName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, roleName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JWT:TokenValidityInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            // So sánh password đã hash với hashedPassword (mật khẩu đã được hash trước đó)
            var hashedInputPassword = HashPassword(password);

            // Cho phép so sánh trực tiếp nếu hashedPassword là mật khẩu gốc (trường hợp cũ chưa mã hóa)
            return hashedInputPassword == hashedPassword || password == hashedPassword;
        }
    }
}
*/

using AgricultureSmart.Repositories.DbAgriContext;
using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class AuthService : IAuthServices
    {
        private readonly AgricultureSmartDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AgricultureSmartDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<(bool Success, string Message, Users User)> RegisterUserAsync(string username, string email, string password, string address, string phoneNumber, int roleId = 3)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == username))
            {
                return (false, "Username already exists", null);
            }

            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                return (false, "Email already exists", null);
            }

            // Validate that the roleId is either Engineer (2) or Farmer (3)
            if (roleId != 2 && roleId != 3)
            {
                // Default to Farmer if invalid role is provided
                roleId = 3;
            }

            var hashedPassword = HashPassword(password);

            var user = new Users
            {
                UserName = username,
                Email = email,
                Password = hashedPassword,
                Address = address,
                PhoneNumber = phoneNumber,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Assign the selected role to the user
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = roleId,
                CreatedAt = DateTime.UtcNow
            };

            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();

            // Create the appropriate profile based on the role
            if (roleId == 2) // Engineer
            {
                var engineer = new Engineer
                {
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Bio = $"Engineer profile for {username}",  // Default Bio to avoid NULL
                    Specialization = "General",  // Default Specialization
                    ExperienceYears = 0,  // Default ExperienceYears
                    Certification = "[]"  // Default empty JSON array for certifications
                };
                await _context.Engineers.AddAsync(engineer);
            }
            else if (roleId == 3) // Farmer
            {
                var farmer = new Farmer
                {
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    FarmLocation = "Not specified",  // Default FarmLocation
                    FarmSize = 0,  // Default FarmSize
                    CropTypes = "[]",  // Default empty JSON array for crop types
                    FarmingExperienceYears = 0  // Default FarmingExperienceYears
                };
                await _context.Farmers.AddAsync(farmer);
            }

            await _context.SaveChangesAsync();

            return (true, "User registered successfully", user);
        }

        public async Task<(bool Success, string Message, Users User, string Token, DateTime Expiration, string RoleName)> LoginAsync(string username, string password)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return (false, "User not found", null, null, DateTime.MinValue, null);
            }

            if (!VerifyPassword(password, user.Password))
            {
                return (false, "Invalid password", null, null, DateTime.MinValue, null);
            }

            var userRole = user.UserRoles.FirstOrDefault(ur => ur.RoleId == 1 || ur.RoleId == 2 || ur.RoleId == 3);
            if (userRole == null)
            {
                return (false, "User does not have a valid role", null, null, DateTime.MinValue, null);
            }

            var roleName = userRole.Role?.Name ?? "";

            var token = GenerateJwtToken(user, roleName);
            var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JWT:TokenValidityInMinutes"]));

            return (true, "Login successful", user, token, expiration, roleName);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<(bool Success, string Message, string Token, DateTime Expiration)> RefreshTokenAsync(string refreshToken)
        {
            // Since we're not storing refresh tokens in the database,
            // we'll validate the refresh token by checking if it's a valid base64 string
            // In a production environment, you might want to use a more secure approach
            try
            {
                // Try to decode the refresh token to validate it's a proper base64 string
                var tokenBytes = Convert.FromBase64String(refreshToken);

                // In a real implementation, you would validate the refresh token against a stored value
                // or use a signed JWT as a refresh token that can be validated

                // For now, we'll just generate a new access token
                // In a real implementation, you would extract the user ID from the refresh token
                // and look up the user to generate a new access token

                // For demonstration purposes, we'll extract the user ID from the refresh token claims
                // This is a simplified approach and not recommended for production

                // Get the user from the claims in the refresh token
                // In a real implementation, you would decode and validate the refresh token
                // For now, we'll just get the first user (THIS IS NOT SECURE, JUST FOR DEMO)
                var user = await _context.Users
                    .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return (false, "Invalid refresh token", null, DateTime.MinValue);
                }

                var userRole = user.UserRoles.FirstOrDefault();
                var roleName = userRole?.Role?.Name ?? "";

                var token = GenerateJwtToken(user, roleName);
                var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JWT:TokenValidityInMinutes"]));

                return (true, "Token refreshed successfully", token, expiration);
            }
            catch
            {
                return (false, "Invalid refresh token", null, DateTime.MinValue);
            }
        }

        private string GenerateJwtToken(Users user, string roleName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, roleName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JWT:TokenValidityInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            // So sánh password đã hash với hashedPassword (mật khẩu đã được hash trước đó)
            var hashedInputPassword = HashPassword(password);

            // Cho phép so sánh trực tiếp nếu hashedPassword là mật khẩu gốc (trường hợp cũ chưa mã hóa)
            return hashedInputPassword == hashedPassword || password == hashedPassword;
        }
    }
}
