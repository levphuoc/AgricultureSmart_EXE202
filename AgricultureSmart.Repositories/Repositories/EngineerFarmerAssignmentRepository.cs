using AgricultureSmart.Repositories.DbAgriContext;
using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories
{
    public class EngineerFarmerAssignmentRepository : IEngineerFarmerAssignmentRepository
    {
        private readonly AgricultureSmartDbContext _context;

        public EngineerFarmerAssignmentRepository(AgricultureSmartDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AnyAsync(Expression<Func<EngineerFarmerAssignment, bool>> predicate)
        {
            return await _context.EngineerFarmerAssignments.AnyAsync(predicate);
        }

        public async Task<List<EngineerFarmerAssignment>> GetAllAsync()
        {
            return await _context.EngineerFarmerAssignments.ToListAsync();
        }
    }
}
