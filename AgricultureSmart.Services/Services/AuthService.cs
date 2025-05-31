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

        public async Task<(bool Success, string Message, Users User, string Token, DateTime Expiration)> LoginAsync(string username, string password)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserName == username);
                
            if (user == null)
            {
                return (false, "User not found", null, null, DateTime.MinValue);
            }

            if (!VerifyPassword(password, user.Password))
            {
                return (false, "Invalid password", null, null, DateTime.MinValue);
            }

            // Check if the user has a valid role (Engineer or Farmer)
            var userRole = user.UserRoles.FirstOrDefault(ur => ur.RoleId == 2 || ur.RoleId == 3);
            if (userRole == null)
            {
                return (false, "User does not have a valid role", null, null, DateTime.MinValue);
            }

            var token = GenerateJwtToken(user, userRole.Role.Name);
            var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JWT:TokenValidityInMinutes"]));

            return (true, "Login successful", user, token, expiration);
        }

        private string GenerateJwtToken(Users user, string roleName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
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
            var hashedInputPassword = HashPassword(password);
            return hashedInputPassword == hashedPassword;
        }
    }
}
