using AgricultureSmart.Repositories.DbAgriContext;
using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AgricultureSmartDbContext _dbContext;

        public OrderRepository(AgricultureSmartDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order?> GetOrderByOrderNumberAsync(string orderNumber)
        {
            return await _dbContext.Orders
                .FirstOrDefaultAsync(o => o.OrderNumber == orderNumber);
        }

        public async Task<Order?> GetOrderWithItemsByIdAsync(int orderId)
        {
            return await _dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status)
        {
            return await _dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.Status == status)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<(List<Order> Items, int TotalCount)> GetFilteredOrdersAsync(string? status, string? paymentStatus, string? orderNumber, int pageNumber, int pageSize)
        {
            var query = _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                        .ThenInclude(p => p.Category) 
                .AsQueryable();


            if (!string.IsNullOrEmpty(status))
                query = query.Where(o => o.Status == status);

            if (!string.IsNullOrEmpty(paymentStatus))
                query = query.Where(o => o.PaymentStatus == paymentStatus);

            if (!string.IsNullOrEmpty(orderNumber))
                query = query.Where(o => o.OrderNumber.Contains(orderNumber));

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(o => o.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}