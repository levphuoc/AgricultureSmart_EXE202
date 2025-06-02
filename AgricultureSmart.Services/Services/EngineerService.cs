using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.EngineerModel;
using AgricultureSmart.Services.Models.TicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class EngineerService : IEngineerService
    {
        private readonly IGenericRepository<Engineer> _engineerRepo;
        private readonly IGenericRepository<Users> _userRepo;

        public EngineerService(IGenericRepository<Engineer> engineerRepo, IGenericRepository<Users> userRepo)
        {
            _engineerRepo = engineerRepo;
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<EngineerViewModel>> GetAllAsync(int pageIndex, int pageSize)
        {
            var engineers = await _engineerRepo.GetAllAsync();
            var users = await _userRepo.GetAllAsync();

            var pagedEngineers = engineers
                .Join(users, e => e.UserId, u => u.Id, (e, u) => new EngineerViewModel
                {
                    Id = e.Id,
                    UserId = e.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Address = u.Address,
                    Specialization = e.Specialization,
                    ExperienceYears = e.ExperienceYears,
                    Certification = e.Certification,
                    Bio = e.Bio,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt
                })
                .OrderByDescending(e => e.CreatedAt)
                .Skip(pageIndex * pageSize)
                .Take(pageSize);

            return pagedEngineers;
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
                // Check if user exists
                var user = await _userRepo.GetByIdAsync(model.UserId);
                if (user == null)
                {
                    return new ServiceResponse<EngineerViewModel>
                    {
                        Success = false,
                        Message = "User not found."
                    };
                }

                // Check if user is already an engineer
                var engineers = await _engineerRepo.GetAllAsync();
                var existingEngineer = engineers.FirstOrDefault(e => e.UserId == model.UserId);
                if (existingEngineer != null)
                {
                    return new ServiceResponse<EngineerViewModel>
                    {
                        Success = false,
                        Message = "User is already registered as an engineer."
                    };
                }

                var engineer = new Engineer
                {
                    UserId = model.UserId,
                    Specialization = model.Specialization,
                    ExperienceYears = model.ExperienceYears,
                    Certification = model.Certification,
                    Bio = model.Bio,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _engineerRepo.AddAsync(engineer);

                var viewModel = await GetByIdAsync(engineer.Id);

                return new ServiceResponse<EngineerViewModel>
                {
                    Data = viewModel,
                    Message = "Engineer created successfully."
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

        public async Task<ServiceResponse<bool>> UpdateAsync(UpdateEngineerModel model)
        {
            try
            {
                var engineer = await _engineerRepo.GetByIdAsync(model.Id);
                if (engineer == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Engineer not found."
                    };
                }

                engineer.Specialization = model.Specialization;
                engineer.ExperienceYears = model.ExperienceYears;
                engineer.Certification = model.Certification;
                engineer.Bio = model.Bio;
                engineer.UpdatedAt = DateTime.UtcNow;

                await _engineerRepo.UpdateAsync(engineer);

                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Engineer updated successfully."
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

                // Check if engineer has related records
                var assignments = await _engineerRepo.GetAllAsync();
                var hasAssignments = assignments.Any(a => a.Id == id);

                if (hasAssignments)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Cannot delete engineer because they have active assignments."
                    };
                }

                await _engineerRepo.DeleteAsync(id);

                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Engineer deleted successfully."
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
