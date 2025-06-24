using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.EngineerModel;
using AgricultureSmart.Services.Models.PagedListResponseModels;
using AgricultureSmart.Services.Models.TicketModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class EngineerService : IEngineerService
    {
        private readonly IGenericRepository<Engineer> _engineerRepo;
        private readonly IGenericRepository<Users> _userRepo;
        private readonly IEngineerRepository _repo;
        private readonly IUserRoleRepository _roleRepo;
        private readonly ILogger<EngineerService> _logger;
        private readonly IEngineerFarmerAssignmentRepository _assignmentRepo;

        public EngineerService(IGenericRepository<Engineer> engineerRepo, IGenericRepository<Users> userRepo, IEngineerRepository repo,
                           ILogger<EngineerService> logger, IUserRoleRepository roleRepo, IEngineerFarmerAssignmentRepository assignmentRepo)
        {
            _engineerRepo = engineerRepo;
            _userRepo = userRepo;
            _repo = repo;
            _logger = logger;
            _roleRepo = roleRepo;
            _assignmentRepo = assignmentRepo;
        }

        public async Task<PagedListResponse<EngineerViewModel>> SearchAsync(
       int pageNumber,
       int pageSize,
       string? specialization,
       int? experienceYears)
        {
            try
            {
                var (entities, totalCount) = await _repo.SearchAsync(
                                                 pageNumber, pageSize,
                                                 specialization, experienceYears);

                var items = entities.Select(e => new EngineerViewModel
                {
                    Id = e.Id,
                    UserId = e.UserId,
                    UserName = e.User.UserName,
                    Email = e.User.Email,
                    PhoneNumber = e.User.PhoneNumber,
                    Address = e.User.Address,
                    Specialization = e.Specialization,
                    ExperienceYears = e.ExperienceYears,
                    Certification = e.Certification,
                    Bio = e.Bio,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt
                }).ToList();

                return new PagedListResponse<EngineerViewModel>
                {
                    Items = items,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching engineers");
                throw;
            }
        }

        public async Task<EngineerViewModel?> GetByIdAsync(int id)
        {
            var engineer = await _engineerRepo.GetByIdAsync(id);
            if (engineer == null) return null;

            var user = await _userRepo.GetByIdAsync(engineer.UserId);
            if (user == null) return null;

            return new EngineerViewModel
            {
                Id = engineer.Id,
                UserId = engineer.UserId,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Specialization = engineer.Specialization,
                ExperienceYears = engineer.ExperienceYears,
                Certification = engineer.Certification,
                Bio = engineer.Bio,
                CreatedAt = engineer.CreatedAt,
                UpdatedAt = engineer.UpdatedAt
            };
        }

        public async Task<ServiceResponse<EngineerViewModel>> CreateAsync(CreateEngineerModel model)
        {
            try
            {
                // Check if username/email already exists
                var existingUser = await _userRepo.FirstOrDefaultAsync(u =>
                    u.UserName == model.Username || u.Email == model.Email);
                if (existingUser != null)
                {
                    return new ServiceResponse<EngineerViewModel>
                    {
                        Success = false,
                        Message = "Username or email already exists."
                    };
                }

                // Check email must be Gmail
                if (!model.Email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
                {
                    return new ServiceResponse<EngineerViewModel>
                    {
                        Success = false,
                        Message = "Only Gmail addresses are allowed."
                    };
                }

                // Create new User
                var user = new Users
                {
                    UserName = model.Username,
                    Email = model.Email,
                    Password = HashPassword(model.Password),
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                await _userRepo.AddAsync(user);

                // Assign "Engineer" role
                var role = await _roleRepo.GetByNameAsync("Engineer");
                if (role != null)
                {
                    await _roleRepo.AddAsync(new UserRole
                    {
                        UserId = user.Id,
                        RoleId = role.Id
                    });
                }

                // Create Engineer
                var engineer = new Engineer
                {
                    UserId = user.Id,
                    Specialization = model.Specialization,
                    ExperienceYears = model.ExperienceYears,
                    Certification = model.Certification,
                    Bio = model.Bio,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _engineerRepo.AddAsync(engineer);

                // Return result
                var viewModel = await GetByIdAsync(engineer.Id);

                return new ServiceResponse<EngineerViewModel>
                {
                    Success = true,
                    Data = viewModel,
                    Message = "Engineer and account created successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<EngineerViewModel>
                {
                    Success = false,
                    Message = $"Error creating engineer: {ex.Message}"
                };
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        public async Task<ServiceResponse<bool>> UpdateAsync(int id, UpdateEngineerModel model)
        {
            try
            {
                var engineer = await _engineerRepo.GetByIdAsync(id);
                if (engineer == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Engineer not found."
                    };
                }

                var user = await _userRepo.GetByIdAsync(engineer.UserId);
                if (user == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Associated user not found."
                    };
                }

                // Check email must be Gmail
                if (!model.Email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Only Gmail addresses are allowed."
                    };
                }

                // Check trùng username/email/phone với người khác
                var duplicateUser = await _userRepo.FirstOrDefaultAsync(u =>
                    (u.UserName == model.Username || u.Email == model.Email || u.PhoneNumber == model.PhoneNumber)
                    && u.Id != user.Id);

                if (duplicateUser != null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Username, email or phone number is already used by another user."
                    };
                }

                user.UserName = model.Username;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;
                user.Password = HashPassword(model.Password); 
                user.UpdatedAt = DateTime.UtcNow;
                await _userRepo.UpdateAsync(user);

                engineer.Specialization = model.Specialization;
                engineer.ExperienceYears = model.ExperienceYears;
                engineer.Certification = model.Certification;
                engineer.Bio = model.Bio;
                engineer.UpdatedAt = DateTime.UtcNow;
                await _engineerRepo.UpdateAsync(engineer);

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Engineer and account information updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Error updating engineer: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var engineer = await _engineerRepo.GetByIdAsync(id);
                if (engineer == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Engineer not found."
                    };
                }

                // Kiểm tra có assignment không (nếu có ràng buộc với bảng trung gian như EngineerFarmerAssignment)
                var hasAssignments = await _assignmentRepo.AnyAsync(a => a.EngineerId == id); // bạn cần truyền _assignmentRepo nếu chưa có
                if (hasAssignments)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Cannot delete engineer because they have active assignments."
                    };
                }

                // Lấy user liên kết
                var user = await _userRepo.GetByIdAsync(engineer.UserId);
                if (user == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Associated user not found."
                    };
                }

                // Xóa Engineer
                await _engineerRepo.DeleteAsync(id);

                // Xóa User
                await _userRepo.DeleteAsync(user.Id);

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Engineer and associated user deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Error deleting engineer: {ex.Message}"
                };
            }
        }
    }
}
