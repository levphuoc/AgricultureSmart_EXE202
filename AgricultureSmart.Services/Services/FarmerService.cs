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
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class FarmerService : IFarmerService
    {
        private readonly IGenericRepository<Farmer> _farmerRepo;
        private readonly IGenericRepository<Users> _userRepo;
        private readonly IFarmerRepository _repo;
        private readonly ILogger<FarmerService> _logger;

        public FarmerService(IGenericRepository<Farmer> farmerRepo, IGenericRepository<Users> userRepo, IFarmerRepository repo, ILogger<FarmerService> logger)
        {
            _farmerRepo = farmerRepo;
            _userRepo = userRepo;
            _repo = repo;
            _logger = logger;
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
                    UserName = f.User.UserName,
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
                UserName = user.UserName,
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
                // Check if user exists
                var user = await _userRepo.GetByIdAsync(model.UserId);
                if (user == null)
                {
                    return new ServiceResponse<FarmerViewModel>
                    {
                        Success = false,
                        Message = "User not found."
                    };
                }

                // Check if user is already a farmer
                var farmers = await _farmerRepo.GetAllAsync();
                var existingFarmer = farmers.FirstOrDefault(f => f.UserId == model.UserId);
                if (existingFarmer != null)
                {
                    return new ServiceResponse<FarmerViewModel>
                    {
                        Success = false,
                        Message = "User is already registered as a farmer."
                    };
                }

                var farmer = new Farmer
                {
                    UserId = model.UserId,
                    FarmLocation = model.FarmLocation,
                    FarmSize = model.FarmSize,
                    CropTypes = model.CropTypes,
                    FarmingExperienceYears = model.FarmingExperienceYears,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _farmerRepo.AddAsync(farmer);

                var viewModel = await GetByIdAsync(farmer.Id);

                return new ServiceResponse<FarmerViewModel>
                {
                    Data = viewModel,
                    Message = "Farmer created successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<FarmerViewModel>
                {
                    Success = false,
                    Message = $"Error creating farmer: {ex.Message}"
                };
            }
        }

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

                await _farmerRepo.DeleteAsync(id);

                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Farmer deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Error deleting farmer: {ex.Message}"
                };
            }
        }
    }
}
