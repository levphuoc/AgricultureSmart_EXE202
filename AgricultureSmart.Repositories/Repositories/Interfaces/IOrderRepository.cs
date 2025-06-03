using AgricultureSmart.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Repositories.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order?> GetOrderByOrderNumberAsync(string orderNumber);
        Task<Order?> GetOrderWithItemsByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status);
    }
} 