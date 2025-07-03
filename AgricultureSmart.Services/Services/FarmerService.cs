using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.FarmerModels;
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
    public class FarmerService : IFarmerService
    {
        private readonly IGenericRepository<Farmer> _farmerRepo;
        private readonly IGenericRepository<Users> _userRepo;
        private readonly IFarmerRepository _repo;
        private readonly IUserRoleRepository _roleRepo;
        private readonly ILogger<FarmerService> _logger;

        public FarmerService(IGenericRepository<Farmer> farmerRepo, IGenericRepository<Users> userRepo, IFarmerRepository repo, ILogger<FarmerService> logger, IUserRoleRepository roleRepo)
        {
            _farmerRepo = farmerRepo;
            _userRepo = userRepo;
            _repo = repo;
            _logger = logger;
            _roleRepo = roleRepo;
        }

        public async Task<PagedListResponse<FarmerViewModel>> SearchAsync(
      int pageNumber,
      int pageSize,
      string? farmLocation,
      decimal? farmSize,
      string? cropTypes)
        {
            try
            {
                var (entities, totalCount) = await _repo.SearchAsync(
                                                 pageNumber, pageSize,
                                                 farmLocation, farmSize, cropTypes);

                var items = entities.Select(f => new FarmerViewModel
                {
                    Id = f.Id,
                    UserId = f.UserId,
                    Username = f.User.UserName,
                    Email = f.User.Email,
                    PhoneNumber = f.User.PhoneNumber,
                    Address = f.User.Address,
                    FarmLocation = f.FarmLocation,
                    FarmSize = f.FarmSize,
                    CropTypes = f.CropTypes,
                    FarmingExperienceYears = f.FarmingExperienceYears,
                    CreatedAt = f.CreatedAt,
                    UpdatedAt = f.UpdatedAt
                }).ToList();

                return new PagedListResponse<FarmerViewModel>
                {
                    Items = items,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching farmers");
                throw;
            }
        }

        public async Task<FarmerViewModel?> GetByIdAsync(int id)
        {
            var farmer = await _farmerRepo.GetByIdAsync(id);
            if (farmer == null) return null;

            var user = await _userRepo.GetByIdAsync(farmer.UserId);
            if (user == null) return null;

            return new FarmerViewModel
            {
                Id = farmer.Id,
                UserId = farmer.UserId,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                FarmLocation = farmer.FarmLocation,
                FarmSize = farmer.FarmSize,
                CropTypes = farmer.CropTypes,
                FarmingExperienceYears = farmer.FarmingExperienceYears,
                CreatedAt = farmer.CreatedAt,
                UpdatedAt = farmer.UpdatedAt
            };
        }

        public async Task<ServiceResponse<FarmerViewModel>> CreateAsync(CreateFarmerModel model)
        {
            try
            {
                // Check if email or username already exists
                var existingUser = await _userRepo.FirstOrDefaultAsync(u =>
                    u.Email == model.Email || u.UserName == model.Username);
                if (existingUser != null)
                {
                    return new ServiceResponse<FarmerViewModel>
                    {
                        Success = false,
                        Message = "Username or email already exists."
                    };
                }

                // Create new user
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

                // Assign "Farmer" role
                var farmerRole = await _roleRepo.GetByNameAsync("Farmer");
                if (farmerRole != null)
                {
                    var userRole = new UserRole
                    {
                        UserId = user.Id,
                        RoleId = farmerRole.Id
                    };
                    await _roleRepo.AddAsync(userRole);
                }

                // Create farmer profile
                var farmer = new Farmer
                {
                    UserId = user.Id,
                    FarmLocation = model.FarmLocation,
                    FarmSize = model.FarmSize,
                    CropTypes = model.CropTypes,
                    FarmingExperienceYears = model.FarmingExperienceYears,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _farmerRepo.AddAsync(farmer);

                // Map to ViewModel
                var viewModel = await GetByIdAsync(farmer.Id);

                return new ServiceResponse<FarmerViewModel>
                {
                    Success = true,
                    Data = viewModel,
                    Message = "Farmer and account created successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<FarmerViewModel>
                {
                    Success = false,
                    Message = $"Error creating farmer and account: {ex.Message}"
                };
            }
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        /*public async Task<ServiceResponse<bool>> UpdateAsync(int id, UpdateFarmerModel model)
        {
            try
            {
                var farmer = await _farmerRepo.GetByIdAsync(id);
                if (farmer == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Farmer not found."
                    };
                }

                farmer.FarmLocation = model.FarmLocation;
                farmer.FarmSize = model.FarmSize;
                farmer.CropTypes = model.CropTypes;
                farmer.FarmingExperienceYears = model.FarmingExperienceYears;
                farmer.UpdatedAt = DateTime.UtcNow;

                await _farmerRepo.UpdateAsync(farmer);

                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Farmer updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Error updating farmer: {ex.Message}"
                };
            }
        }*/

        public async Task<ServiceResponse<bool>> UpdateAsync(int id, UpdateFarmerModel model)
        {
            try
            {
                var farmer = await _farmerRepo.GetByIdAsync(id);
                if (farmer == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Farmer not found."
                    };
                }

                // Update User info
                var user = await _userRepo.GetByIdAsync(farmer.UserId);
                if (user == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Associated user not found."
                    };
                }

                /*if (!model.Email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Only Gmail addresses are allowed."
                    };
                }*/

                // Kiểm tra username/email đã bị người khác dùng chưa
                var duplicateUser = await _userRepo.FirstOrDefaultAsync(u =>
                    (u.UserName == model.Username || u.Email == model.Email) && u.Id != user.Id);

                if (duplicateUser != null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Username or email is already taken by another user."
                    };
                }

                user.UserName = model.Username;
                user.Email = model.Email;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                user.Password = HashPassword(model.Password); // SHA256 hoặc BCrypt
                user.UpdatedAt = DateTime.UtcNow;

                await _userRepo.UpdateAsync(user);

                // Update Farmer info
                farmer.FarmLocation = model.FarmLocation;
                farmer.FarmSize = model.FarmSize;
                farmer.CropTypes = model.CropTypes;
                farmer.FarmingExperienceYears = model.FarmingExperienceYears;
                farmer.UpdatedAt = DateTime.UtcNow;

                await _farmerRepo.UpdateAsync(farmer);

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Farmer and user information updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Error updating farmer: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var farmer = await _farmerRepo.GetByIdAsync(id);
                if (farmer == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Farmer not found."
                    };
                }

                // Lấy User liên quan
                var user = await _userRepo.GetByIdAsync(farmer.UserId);
                if (user == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Associated user not found."
                    };
                }

                // Xóa Farmer trước (nếu có ràng buộc FK cascade thì có thể bỏ qua bước này)
                await _farmerRepo.DeleteAsync(id);

                // Xóa User (có thể dùng id: await _userRepo.DeleteAsync(user.Id);)
                await _userRepo.DeleteAsync(user.Id);

                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Farmer and associated user deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Error deleting farmer and user: {ex.Message}"
                };
            }
        }
    }
}
