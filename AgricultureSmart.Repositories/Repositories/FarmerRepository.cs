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
    public class FarmerRepository : IFarmerRepository
    {
        private readonly AgricultureSmartDbContext _context;
        public FarmerRepository(AgricultureSmartDbContext context) => _context = context;

        public async Task<(IEnumerable<Farmer> Items, int TotalCount)> SearchAsync(
            int pageNumber,
            int pageSize,
            string? farmLocation,
            decimal? farmSize,
            string? cropTypes)
        {
            var query = _context.Farmers
                                .Include(f => f.User)
                                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(farmLocation))
                query = query.Where(f => f.FarmLocation.Contains(farmLocation.Trim()));

            if (farmSize.HasValue)
                query = query.Where(f => f.FarmSize == farmSize.Value);

            if (!string.IsNullOrWhiteSpace(cropTypes))
                query = query.Where(f => f.CropTypes.Contains(cropTypes.Trim()));

            var totalCount = await query.CountAsync();

            var items = await query.OrderByDescending(f => f.CreatedAt)
                                   .Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return (items, totalCount);
        }
    }
}
