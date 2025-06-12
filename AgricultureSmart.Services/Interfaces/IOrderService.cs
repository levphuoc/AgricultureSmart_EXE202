using AgricultureSmart.Services.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderFromCartAsync(int userId, CreateOrderDto createOrderDto);
        Task<OrderDto?> GetOrderByIdAsync(int userId, int orderId);
        Task<OrderDto?> GetOrderByOrderNumberAsync(int userId, string orderNumber);
        Task<IEnumerable<OrderDto>> GetUserOrdersAsync(int userId);
        Task<bool> UpdateOrderStatusAsync(int orderId, UpdateOrderStatusDto updateOrderStatusDto);
        Task<bool> CancelOrderAsync(int userId, int orderId);
        Task<bool> ProcessPaymentAsync(int userId, int orderId, string paymentMethod);
        
        // Admin methods
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(string status);
    }
} 