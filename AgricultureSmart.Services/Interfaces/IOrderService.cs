using AgricultureSmart.Services.Models.OrderModels;
using AgricultureSmart.Services.Models.PagedListResponseModels;
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
        Task<bool> UpdateOrderAfterPaymentAsync(int orderId, string transactionId);
        
        // Admin methods
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(string status);
        
        // Method to get order without user check (for payment callbacks)
        Task<OrderDto?> GetOrderByIdWithoutUserCheckAsync(int orderId);

        Task<PagedListResponse<OrderDto>> GetFilteredOrdersAsync(string? status, string? paymentStatus, string? orderNumber, int pageNumber, int pageSize);


        // <-- THÊM CÁC PH??NG TH?C NÀY CHO WEBHOOK -->
        Task<bool> UpdateOrderStatusToCancelledAsync(int orderId);
        Task<bool> UpdateOrderStatusToExpiredAsync(int orderId);
        Task<bool> UpdateOrderStatusToPendingAsync(int orderId); 
    }
} 