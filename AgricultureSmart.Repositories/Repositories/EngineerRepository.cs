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
    public class EngineerRepository : IEngineerRepository
    {
        private readonly AgricultureSmartDbContext _context;
        public EngineerRepository(AgricultureSmartDbContext context) => _context = context;

        public async Task<(IEnumerable<Engineer> Items, int TotalCount)> SearchAsync(
            int pageNumber,
            int pageSize,
            string? specialization,
            int? experienceYears)
        {
            var query = _context.Engineers
                                .Include(e => e.User)
                                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(specialization))
                query = query.Where(e => e.Specialization.Contains(specialization.Trim()));

            if (experienceYears.HasValue)
                query = query.Where(e => e.ExperienceYears == experienceYears.Value);

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(e => e.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
