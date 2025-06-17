using AgricultureSmart.Repositories.DbAgriContext;
using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AgricultureSmartDbContext _context;
        public TicketRepository(AgricultureSmartDbContext context) => _context = context;

        public async Task<(IEnumerable<Ticket> Items, int TotalCount)> SearchAsync(
            int pageNumber,
            int pageSize,
            string? title,
            int? farmerId,
            int? assignedEngineerId)
        {
            var query = _context.Tickets
                                .Include(t => t.Farmer).ThenInclude(f => f.User)
                                .Include(t => t.AssignedEngineer).ThenInclude(e => e.User)
                                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(t => t.Title.Contains(title.Trim()));

            if (farmerId.HasValue)
                query = query.Where(t => t.FarmerId == farmerId.Value);

            if (assignedEngineerId.HasValue)
                query = query.Where(t => t.AssignedEngineerId == assignedEngineerId.Value);

            // ---- Sắp xếp priority: urgent → high → medium → low ----
            query = query.OrderBy(t =>
                        t.Priority == "urgent" ? 0 :
                        t.Priority == "high" ? 1 :
                        t.Priority == "medium" ? 2 : 3)
                         .ThenByDescending(t => t.CreatedAt);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
