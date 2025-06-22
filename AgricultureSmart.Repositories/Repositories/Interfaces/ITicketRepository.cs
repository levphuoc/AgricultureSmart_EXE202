using AgricultureSmart.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task<(IEnumerable<Ticket> Items, int TotalCount)> SearchAsync(
            int pageNumber,
            int pageSize,
            string? title,
            string? farmerName,
            string? assignedEngineerName,
            string? priority,
            string? status);
        Task<Ticket?> GetByIdAsync(int id);

        Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(int userId);

        Task<IEnumerable<Ticket>> GetTicketsByEngineerIdAsync(int userId);

        Task<Dictionary<string, int>> GetTicketStatusCountsAsync();
    }
}
