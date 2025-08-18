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
            var farmer = await _farmerRepo.FirstOrDefaultAsync(f => f.UserId == id);
            if (farmer == null) return null;

            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return null;

            return new FarmerViewModel
            {
                Id = user.Id,
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                FarmLocation = farmer.FarmLocation,
                FarmSize = farmer.FarmSize,
                CropTypes = farmer.CropTypes,
                FarmingExperienceYears = farmer.FarmingExperienceYears,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public async Task<ServiceResponse<FarmerViewModel>> CreateAsync(CreateFarmerModel model)
        {
            try
            {
                /* ---------- VALIDATION ĐƠN GIẢN ---------- */
                if (model.FarmSize <= 0)
                {
                    return new ServiceResponse<FarmerViewModel>
                    {
                        Success = false,
                        Message = "Farm size must be greater than 0."
                    };
                }

                if (model.FarmingExperienceYears <= 0)
                {
                    return new ServiceResponse<FarmerViewModel>
                    {
                        Success = false,
                        Message = "Farming experience years must be greater than 0."
                    };
                }

                /* ---------- KIỂM TRA USERNAME / EMAIL ĐÃ TỒN TẠI ---------- */
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

                /* ---------- TẠO USER ---------- */
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

                /* ---------- GÁN ROLE “Farmer” ---------- */
                var farmerRole = await _roleRepo.GetByNameAsync("Farmer");
                if (farmerRole != null)
                {
                    await _roleRepo.AddAsync(new UserRole
                    {
                        UserId = user.Id,
                        RoleId = farmerRole.Id
                    });
                }

                /* ---------- TẠO FARMER PROFILE ---------- */
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

                /* ---------- TRẢ VỀ VIEWMODEL ---------- */
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

                *//*if (!model.Email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Only Gmail addresses are allowed."
                    };
                }*//*

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
        }*/

        public async Task<ServiceResponse<bool>> UpdateAsync(int id, UpdateFarmerModel model)
        {
            try
            {
                /* ---------- LẤY HIỆN TRẠNG ---------- */
                var farmer = await _farmerRepo.GetByIdAsync(id);
                if (farmer is null)
                    return new ServiceResponse<bool> { Success = false, Message = "Farmer not found." };

                var user = await _userRepo.GetByIdAsync(farmer.UserId);
                if (user is null)
                    return new ServiceResponse<bool> { Success = false, Message = "Associated user not found." };

                /* ---------- XÁC ĐỊNH CÓ THAY ĐỔI THẬT SỰ ---------- */
                bool wantUserName = !string.IsNullOrWhiteSpace(model.Username) &&
                                    !string.Equals(model.Username, user.UserName, StringComparison.Ordinal);

                bool wantEmail = !string.IsNullOrWhiteSpace(model.Email) &&
                                    !string.Equals(model.Email, user.Email, StringComparison.OrdinalIgnoreCase);

                bool wantPhone = !string.IsNullOrWhiteSpace(model.PhoneNumber) &&
                                    !string.Equals(model.PhoneNumber, user.PhoneNumber, StringComparison.Ordinal);

                bool wantAddress = !string.IsNullOrWhiteSpace(model.Address) &&
                                    !string.Equals(model.Address, user.Address, StringComparison.Ordinal);

                bool wantPassword = !string.IsNullOrWhiteSpace(model.Password);

                bool wantLocation = !string.IsNullOrWhiteSpace(model.FarmLocation) &&
                                    !string.Equals(model.FarmLocation, farmer.FarmLocation, StringComparison.Ordinal);

                bool wantSize = model.FarmSize.HasValue &&
                                    model.FarmSize.Value != farmer.FarmSize;

                bool wantCrops = !string.IsNullOrWhiteSpace(model.CropTypes) &&
                                    !string.Equals(model.CropTypes, farmer.CropTypes, StringComparison.Ordinal);

                bool wantExpYears = model.FarmingExperienceYears.HasValue &&
                                    model.FarmingExperienceYears.Value != farmer.FarmingExperienceYears;

                /* ---------- KHÔNG CÓ GÌ ĐỔI ---------- */
                if (!(wantUserName || wantEmail || wantPhone || wantAddress || wantPassword ||
                      wantLocation || wantSize || wantCrops || wantExpYears))
                {
                    return new ServiceResponse<bool>
                    {
                        Success = true,
                        Data = false,
                        Message = "No actual changes detected."
                    };
                }

                /* ---------- KIỂM TRA GIÁ TRỊ MỚI HỢP LỆ ---------- */
                if (wantEmail && !model.Email!.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
                    return new ServiceResponse<bool> { Success = false, Message = "Only Gmail addresses are allowed." };

                if (wantSize && model.FarmSize!.Value <= 0)
                    return new ServiceResponse<bool> { Success = false, Message = "Farm size must be greater than 0." };

                if (wantExpYears && model.FarmingExperienceYears!.Value <= 0)
                    return new ServiceResponse<bool> { Success = false, Message = "Farming experience years must be greater than 0." };

                if (wantUserName || wantEmail || wantPhone)
                {
                    var duplicate = await _userRepo.FirstOrDefaultAsync(u =>
                            (wantUserName && u.UserName == model.Username) ||
                            (wantEmail && u.Email == model.Email) ||
                            (wantPhone && u.PhoneNumber == model.PhoneNumber)
                         && u.Id != user.Id);

                    if (duplicate is not null)
                        return new ServiceResponse<bool> { Success = false, Message = "Username, email or phone number is already used by another user." };
                }

                /* ---------- CẬP NHẬT USER ---------- */
                if (wantUserName) user.UserName = model.Username!;
                if (wantEmail) user.Email = model.Email!;
                if (wantPhone) user.PhoneNumber = model.PhoneNumber!;
                if (wantAddress) user.Address = model.Address!;
                if (wantPassword) user.Password = HashPassword(model.Password!);

                if (wantUserName || wantEmail || wantPhone || wantAddress || wantPassword)
                {
                    user.UpdatedAt = DateTime.UtcNow;
                    await _userRepo.UpdateAsync(user);
                }

                /* ---------- CẬP NHẬT FARMER ---------- */
                if (wantLocation) farmer.FarmLocation = model.FarmLocation!;
                if (wantSize) farmer.FarmSize = model.FarmSize!.Value;
                if (wantCrops) farmer.CropTypes = model.CropTypes!;
                if (wantExpYears) farmer.FarmingExperienceYears = model.FarmingExperienceYears!.Value;

                if (wantLocation || wantSize || wantCrops || wantExpYears)
                {
                    farmer.UpdatedAt = DateTime.UtcNow;
                    await _farmerRepo.UpdateAsync(farmer);
                }

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Data = true,
                    Message = "Update successful."
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
