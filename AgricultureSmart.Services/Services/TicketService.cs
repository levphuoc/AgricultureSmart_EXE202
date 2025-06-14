using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.TicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class TicketService : ITicketService
    {
        private readonly IGenericRepository<Ticket> _ticketRepo;

        public TicketService(IGenericRepository<Ticket> ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }

        public async Task<IEnumerable<TicketViewModel>> GetAllAsync(int pageIndex, int pageSize)
        {
            var tickets = await _ticketRepo.GetAllAsync();

            var pagedTickets = tickets
                .OrderByDescending(t => t.CreatedAt) // optional: sort by date
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select(t => new TicketViewModel
                {
                    Id = t.Id,
                    FarmerId = t.FarmerId,
                    AssignedEngineerId = t.AssignedEngineerId,
                    Title = t.Title,
                    Category = t.Category,
                    CropType = t.CropType,
                    Location = t.Location,
                    Description = t.Description,
                    Priority = t.Priority,
                    ContactMethod = t.ContactMethod,
                    PhoneNumber = t.PhoneNumber,
                    ImageUrl = t.ImageUrl,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                    ResolvedAt = t.ResolvedAt
                });

            return pagedTickets;
        }


        public async Task<TicketViewModel?> GetByIdAsync(int id)
        {
            var t = await _ticketRepo.GetByIdAsync(id);
            if (t == null) return null;

            return new TicketViewModel
            {
                Id = t.Id,
                FarmerId = t.FarmerId,
                AssignedEngineerId = t.AssignedEngineerId,
                Title = t.Title,
                Category = t.Category,
                CropType = t.CropType,
                Location = t.Location,
                Description = t.Description,
                Priority = t.Priority,
                ContactMethod = t.ContactMethod,
                PhoneNumber = t.PhoneNumber,
                ImageUrl = t.ImageUrl,
                Status = t.Status,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt,
                ResolvedAt = t.ResolvedAt
            };
        }

        public async Task<ServiceResponse<TicketViewModel>> CreateAsync(CreateTicketModel model)
        {
            try
            {
                var ticket = new Ticket
                {
                    FarmerId = model.FarmerId,
                    AssignedEngineerId = model.AssignedEngineerId,
                    Title = model.Title,
                    Category = model.Category,
                    CropType = model.CropType,
                    Location = model.Location,
                    Description = model.Description,
                    Priority = model.Priority,
                    ContactMethod = model.ContactMethod,
                    PhoneNumber = model.PhoneNumber,
                    ImageUrl = model.ImageUrl,
                    Status = model.Status,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _ticketRepo.AddAsync(ticket);

                var viewModel = await GetByIdAsync(ticket.Id);

                return new ServiceResponse<TicketViewModel>
                {
                    Data = viewModel,
                    Message = "Ticket created successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<TicketViewModel>
                {
                    Success = false,
                    Message = $"Error creating ticket: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResponse<bool>> UpdateAsync(UpdateTicketModel model)
        {
            try
            {
                var ticket = await _ticketRepo.GetByIdAsync(model.Id);
                if (ticket == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Ticket not found."
                    };
                }

                /*ticket.FarmerId = model.FarmerId;
                ticket.AssignedEngineerId = model.AssignedEngineerId;*/
                ticket.Title = model.Title;
                ticket.Category = model.Category;
                ticket.CropType = model.CropType;
                ticket.Location = model.Location;
                ticket.Description = model.Description;
                ticket.Priority = model.Priority;
                ticket.ContactMethod = model.ContactMethod;
                ticket.PhoneNumber = model.PhoneNumber;
                ticket.ImageUrl = model.ImageUrl;
                ticket.Status = model.Status;
                ticket.UpdatedAt = DateTime.UtcNow;

                await _ticketRepo.UpdateAsync(ticket);

                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Ticket updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Error updating ticket: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var ticket = await _ticketRepo.GetByIdAsync(id);
                if (ticket == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Ticket not found."
                    };
                }

                await _ticketRepo.DeleteAsync(id);

                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Ticket deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = $"Error deleting ticket: {ex.Message}"
                };
            }
        }
        public async Task<ServiceResponse<TicketViewModel>> UpdateStatusAsync(UpdateTicketStatusModel model)
        {
            try
            {
                var ticket = await _ticketRepo.GetByIdAsync(model.Id);
                if (ticket == null)
                {
                    return new ServiceResponse<TicketViewModel>
                    {
                        Success = false,
                        Message = "Ticket not found."
                    };
                }

                // Validate status
                if (!TicketStatusConstants.ValidStatuses.Contains(model.Status))
                {
                    return new ServiceResponse<TicketViewModel>
                    {
                        Success = false,
                        Message = $"Invalid status: {model.Status}"
                    };
                }

                // Validate status transition
                if (!IsValidStatusTransition(ticket.Status, model.Status))
                {
                    return new ServiceResponse<TicketViewModel>
                    {
                        Success = false,
                        Message = $"Invalid status transition from '{ticket.Status}' to '{model.Status}'"
                    };
                }

                // Validate assigned engineer when status is "assigned"
                if (model.Status == TicketStatusConstants.Assigned && !model.AssignedEngineerId.HasValue)
                {
                    return new ServiceResponse<TicketViewModel>
                    {
                        Success = false,
                        Message = "AssignedEngineerId is required when status is 'assigned'"
                    };
                }

                // Update ticket properties
                var oldStatus = ticket.Status;
                ticket.Status = model.Status;
                ticket.UpdatedAt = DateTime.UtcNow;

                // Set assigned engineer if provided
                if (model.AssignedEngineerId.HasValue)
                {
                    ticket.AssignedEngineerId = model.AssignedEngineerId.Value;
                }

                // Set resolved date if status is resolved
                if (model.Status == TicketStatusConstants.Resolved && oldStatus != TicketStatusConstants.Resolved)
                {
                    ticket.ResolvedAt = DateTime.UtcNow;
                }

                // Clear resolved date if moving away from resolved status
                if (oldStatus == TicketStatusConstants.Resolved && model.Status != TicketStatusConstants.Resolved)
                {
                    ticket.ResolvedAt = null;
                }

                await _ticketRepo.UpdateAsync(ticket);

                var updatedTicket = await GetByIdAsync(ticket.Id);

                return new ServiceResponse<TicketViewModel>
                {
                    Data = updatedTicket,
                    Message = $"Ticket status updated from '{oldStatus}' to '{model.Status}' successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<TicketViewModel>
                {
                    Success = false,
                    Message = $"Error updating ticket status: {ex.Message}"
                };
            }
        }

        private bool IsValidStatusTransition(string currentStatus, string newStatus)
        {
            if (currentStatus == newStatus)
                return true;

            if (!TicketStatusConstants.ValidTransitions.ContainsKey(currentStatus))
                return false;

            return TicketStatusConstants.ValidTransitions[currentStatus].Contains(newStatus);
        }

        public async Task<ServiceResponse<TicketViewModel>>
    CreateForFarmerAsync(int farmerId, CreateTicketForFarmerModel model)
        {
            try
            {
                var ticket = new Ticket
                {
                    FarmerId = farmerId,
                    AssignedEngineerId = null,              // chưa gán kỹ sư
                    Title = model.Title,
                    Category = model.Category,
                    CropType = model.CropType,
                    Location = model.Location,
                    Description = model.Description,
                    Priority = model.Priority,
                    ContactMethod = model.ContactMethod,
                    PhoneNumber = model.PhoneNumber,
                    ImageUrl = model.ImageUrl,
                    Status = TicketStatusConstants.Open,   // mặc định “open”
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _ticketRepo.AddAsync(ticket);
                var viewModel = await GetByIdAsync(ticket.Id);

                return new ServiceResponse<TicketViewModel>
                {
                    Data = viewModel,
                    Message = "Ticket created successfully."
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<TicketViewModel>
                {
                    Success = false,
                    Message = $"Error creating ticket: {ex.Message}"
                };
            }
        }

        public async Task<IEnumerable<TicketFarmerViewModel>> GetByUserIdAsync(
    int userId)
        {
            var tickets = await _ticketRepo.GetAllAsync();

            var userTickets = tickets
                .Where(t => t.FarmerId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new TicketFarmerViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Category = t.Category,
                    CropType = t.CropType,
                    Location = t.Location,
                    Description = t.Description,
                    Priority = t.Priority,
                    ContactMethod = t.ContactMethod,
                    PhoneNumber = t.PhoneNumber,
                    ImageUrl = t.ImageUrl,
                    Status = t.Status,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                    ResolvedAt = t.ResolvedAt
                });

            return userTickets;
        }
    }
}
