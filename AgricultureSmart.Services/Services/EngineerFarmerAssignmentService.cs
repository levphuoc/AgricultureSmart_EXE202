using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.AssignmentModel;
using AgricultureSmart.Services.Models.TicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class EngineerFarmerAssignmentService : IEngineerFarmerAssignmentService
    {
        private readonly IGenericRepository<EngineerFarmerAssignment> _assignmentRepo;
        private readonly IGenericRepository<Engineer> _engineerRepo;
        private readonly IGenericRepository<Farmer> _farmerRepo;
        private readonly IGenericRepository<Users> _userRepo;

        public EngineerFarmerAssignmentService(
            IGenericRepository<EngineerFarmerAssignment> assignmentRepo,
            IGenericRepository<Engineer> engineerRepo,
            IGenericRepository<Farmer> farmerRepo,
            IGenericRepository<Users> userRepo)
        {
            _assignmentRepo = assignmentRepo;
            _engineerRepo = engineerRepo;
            _farmerRepo = farmerRepo;
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<EngineerFarmerAssignmentViewModel>> GetAllAsync(int pageIndex, int pageSize)
        {
            var assignments = await _assignmentRepo.GetAllAsync();
            var engineers = await _engineerRepo.GetAllAsync();
            var farmers = await _farmerRepo.GetAllAsync();
            var users = await _userRepo.GetAllAsync();

            var assignmentViewModels = new List<EngineerFarmerAssignmentViewModel>();

            foreach (var assignment in assignments)
            {
                var engineer = engineers.FirstOrDefault(e => e.Id == assignment.EngineerId);
                var farmer = farmers.FirstOrDefault(f => f.Id == assignment.FarmerId);

                if (engineer != null && farmer != null)
                {
                    var engineerUser = users.FirstOrDefault(u => u.Id == engineer.UserId);
                    var farmerUser = users.FirstOrDefault(u => u.Id == farmer.UserId);

                    if (engineerUser != null && farmerUser != null)
                    {
                        assignmentViewModels.Add(new EngineerFarmerAssignmentViewModel
                        {
                            Id = assignment.Id,
                            EngineerId = engineer.Id,
                            EngineerName = engineerUser.UserName,
                            EngineerSpecialization = engineer.Specialization,
                            FarmerId = farmer.Id,
                            FarmerName = farmerUser.UserName,
                            FarmLocation = farmer.FarmLocation,
                            AssignedAt = assignment.AssignedAt,
                            IsActive = assignment.IsActive,
                            Notes = assignment.Notes
                        });
                    }
                }
            }

            var pagedAssignments = assignmentViewModels
                .OrderByDescending(a => a.AssignedAt)
                .Skip(pageIndex * pageSize)
                .Take(pageSize);

            return pagedAssignments;
        }

        public async Task<EngineerFarmerAssignmentViewModel?> GetByIdAsync(int id)
        {
            var assignment = await _assignmentRepo.GetByIdAsync(id);
            if (assignment == null) return null;

            var engineer = await _engineerRepo.GetByIdAsync(assignment.EngineerId);
            if (engineer == null) return null;

            var farmer = await _farmerRepo.GetByIdAsync(assignment.FarmerId);
            if (farmer == null) return null;

            var engineerUser = await _userRepo.GetByIdAsync(engineer.UserId);
            if (engineerUser == null) return null;

            var farmerUser = await _userRepo.GetByIdAsync(farmer.UserId);
            if (farmerUser == null) return null;

            return new EngineerFarmerAssignmentViewModel
            {
                Id = assignment.Id,
                EngineerId = engineer.Id,
                EngineerName = engineerUser.UserName,
                EngineerSpecialization = engineer.Specialization,
                FarmerId = farmer.Id,
                FarmerName = farmerUser.UserName,
                FarmLocation = farmer.FarmLocation,
                AssignedAt = assignment.AssignedAt,
                IsActive = assignment.IsActive,
                Notes = assignment.Notes
            };
        }

        public async Task<IEnumerable<EngineerFarmerAssignmentViewModel>> GetByEngineerIdAsync(int engineerId, int pageIndex, int pageSize)
        {
            var assignments = await _assignmentRepo.GetAllAsync();
            var engineers = await _engineerRepo.GetAllAsync();
            var farmers = await _farmerRepo.GetAllAsync();
            var users = await _userRepo.GetAllAsync();

            var assignmentViewModels = new List<EngineerFarmerAssignmentViewModel>();

            foreach (var assignment in assignments.Where(a => a.EngineerId == engineerId))
            {
                var engineer = engineers.FirstOrDefault(e => e.Id == assignment.EngineerId);
                var farmer = farmers.FirstOrDefault(f => f.Id == assignment.FarmerId);

                if (engineer != null && farmer != null)
                {
                    var engineerUser = users.FirstOrDefault(u => u.Id == engineer.UserId);
                    var farmerUser = users.FirstOrDefault(u => u.Id == farmer.UserId);

                    if (engineerUser != null && farmerUser != null)
                    {
                        assignmentViewModels.Add(new EngineerFarmerAssignmentViewModel
                        {
                            Id = assignment.Id,
                            EngineerId = engineer.Id,
                            EngineerName = engineerUser.UserName,
                            EngineerSpecialization = engineer.Specialization,
                            FarmerId = farmer.Id,
                            FarmerName = farmerUser.UserName,
                            FarmLocation = farmer.FarmLocation,
                            AssignedAt = assignment.AssignedAt,
                            IsActive = assignment.IsActive,
                            Notes = assignment.Notes
                        });
                    }
                }
            }

            var pagedAssignments = assignmentViewModels
                .OrderByDescending(a => a.AssignedAt)
                .Skip(pageIndex * pageSize)
                .Take(pageSize);

            return pagedAssignments;
        }

        public async Task<IEnumerable<EngineerFarmerAssignmentViewModel>> GetByFarmerIdAsync(int farmerId, int pageIndex, int pageSize)
        {
            var assignments = await _assignmentRepo.GetAllAsync();
            var engineers = await _engineerRepo.GetAllAsync();
            var farmers = await _farmerRepo.GetAllAsync();
            var users = await _userRepo.GetAllAsync();

            var assignmentViewModels = new List<EngineerFarmerAssignmentViewModel>();

            foreach (var assignment in assignments.Where(a => a.FarmerId == farmerId))
            {
                var engineer = engineers.FirstOrDefault(e => e.Id == assignment.EngineerId);
                var farmer = farmers.FirstOrDefault(f => f.Id == assignment.FarmerId);

                if (engineer != null && farmer != null)
                {
                    var engineerUser = users.FirstOrDefault(u => u.Id == engineer.UserId);
                    var farmerUser = users.FirstOrDefault(u => u.Id == farmer.UserId);

                    if (engineerUser != null && farmerUser != null)
                    {
                        assignmentViewModels.Add(new EngineerFarmerAssignmentViewModel
                        {
                            Id = assignment.Id,
                            EngineerId = engineer.Id,
                            EngineerName = engineerUser.UserName,
                            EngineerSpecialization = engineer.Specialization,
                            FarmerId = farmer.Id,
                            FarmerName = farmerUser.UserName,
                            FarmLocation = farmer.FarmLocation,
                            AssignedAt = assignment.AssignedAt,
                            IsActive = assignment.IsActive,
                            Notes = assignment.Notes
                        });
                    }
                }
            }

            var pagedAssignments = assignmentViewModels
                .OrderByDescending(a => a.AssignedAt)
                .Skip(pageIndex * pageSize)
                .Take(pageSize);

            return pagedAssignments;
        }

        public async Task<ServiceResponse<EngineerFarmerAssignmentViewModel>> CreateAsync(CreateAssignmentModel model)
        {
            try
            {
                // Check if engineer exists
                var engineer = await _engineerRepo.GetByIdAsync(model.EngineerId);
                if (engineer == null)
                {
                    return new ServiceResponse<EngineerFarmerAssignmentViewModel>
                    {
                        Success = false,
                        Message = "Engineer not found."
                    };
                }

                // Check if farmer exists
                var farmer = await _farmerRepo.GetByIdAsync(model.FarmerId);
                if (farmer == null)
                {
                    return new ServiceResponse<EngineerFarmerAssignmentViewModel>
                    {
                        Success = false,
                        Message = "Farmer not found."
                    };
                }

                // Check if assignment already exists and is active
                var assignments = await _assignmentRepo.GetAllAsync();
                var existingAssignment = assignments.FirstOrDefault(a =>
                    a.EngineerId == model.EngineerId &&
                    a.FarmerId == model.FarmerId &&
                    a.IsActive);

                if (existingAssignment != null)
                {
                    return new ServiceResponse<EngineerFarmerAssignmentViewModel>
                    {
                        Success = false,
                        Message = "An active assignment already exists between this engineer and farmer."
                    };
                }

                var assignment = new EngineerFarmerAssignment
                {
                    EngineerId = model.EngineerId,
                    FarmerId = model.FarmerId,
                    AssignedAt = DateTime.UtcNow,
                    IsActive = model.IsActive,
                    Notes = model.Notes
                };

                await _assignmentRepo.AddAsync(assignment);

                var viewModel = await GetByIdAsync(assignment.Id);

                return new ServiceResponse<EngineerFarmerAssignmentViewModel>
                {
                    Data = viewModel,
                    Message = "Assignment created successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<EngineerFarmerAssignmentViewModel>
                {
                    Success = false,
                    Message = $"Error creating assignment: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResponse<bool>> UpdateAsync(UpdateAssignmentModel model)
        {
            try
            {
                var assignment = await _assignmentRepo.GetByIdAsync(model.Id);
                if (assignment == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Assignment not found."
                    };
                }

                assignment.IsActive = model.IsActive;
                assignment.Notes = model.Notes;

                await _assignmentRepo.UpdateAsync(assignment);

                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Assignment updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Error updating assignment: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var assignment = await _assignmentRepo.GetByIdAsync(id);
                if (assignment == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Assignment not found."
                    };
                }

                await _assignmentRepo.DeleteAsync(id);

                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Assignment deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Error deleting assignment: {ex.Message}"
                };
            }
        }
    }
}
