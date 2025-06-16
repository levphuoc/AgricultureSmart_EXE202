using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories.Interfaces;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.OrderModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            IProductRepository productRepository,
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<OrderService> logger)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OrderDto> CreateOrderFromCartAsync(int userId, CreateOrderDto createOrderDto)
        {
            try
            {
                // Get the user's cart with items
                var cart = await _cartRepository.GetCartWithItemsByUserIdAsync(userId);
                if (cart == null || !cart.CartItems.Any())
                {
                    throw new InvalidOperationException("Cart is empty");
                }

                // Get the user
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    throw new ArgumentException($"User with ID {userId} not found");
                }

                // Create a new order
                var order = new Order
                {
                    UserId = userId,
                    OrderNumber = GenerateOrderNumber(),
                    TotalAmount = cart.TotalAmount,
                    Status = "pending",
                    ShippingAddress = createOrderDto.ShippingAddress,
                    PaymentMethod = createOrderDto.PaymentMethod,
                    PaymentStatus = "pending",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Add the order to the database
                await _orderRepository.AddAsync(order);

                // Create order items from cart items
                foreach (var cartItem in cart.CartItems)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.Id,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        UnitPrice = cartItem.UnitPrice,
                        TotalPrice = cartItem.TotalPrice
                    };

                    await _orderItemRepository.AddAsync(orderItem);

                    // Update product stock
                    var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
                    if (product != null)
                    {
                        product.Stock -= cartItem.Quantity;
                        await _productRepository.UpdateAsync(product);
                    }
                }

                // Clear the cart
                await _cartItemRepository.RemoveAllCartItemsAsync(cart.Id);
                cart.TotalAmount = 0;
                cart.UpdatedAt = DateTime.UtcNow;
                await _cartRepository.UpdateAsync(cart);

                // Get the complete order with items
                var completeOrder = await _orderRepository.GetOrderWithItemsByIdAsync(order.Id);
                var orderDto = _mapper.Map<OrderDto>(completeOrder);
                
                // Add user information
                orderDto.UserName = user.UserName;
                orderDto.UserEmail = user.Email;

                return orderDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating order for user ID {UserId}", userId);
                throw;
            }
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int userId, int orderId)
        {
            try
            {
                var order = await _orderRepository.GetOrderWithItemsByIdAsync(orderId);
                if (order == null || order.UserId != userId)
                {
                    return null;
                }

                var user = await _userRepository.GetByIdAsync(userId);
                var orderDto = _mapper.Map<OrderDto>(order);
                
                if (user != null)
                {
                    orderDto.UserName = user.UserName;
                    orderDto.UserEmail = user.Email;
                }

                return orderDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting order ID {OrderId} for user ID {UserId}", orderId, userId);
                throw;
            }
        }

        public async Task<OrderDto?> GetOrderByOrderNumberAsync(int userId, string orderNumber)
        {
            try
            {
                var order = await _orderRepository.GetOrderByOrderNumberAsync(orderNumber);
                if (order == null || order.UserId != userId)
                {
                    return null;
                }

                var completeOrder = await _orderRepository.GetOrderWithItemsByIdAsync(order.Id);
                var user = await _userRepository.GetByIdAsync(userId);
                var orderDto = _mapper.Map<OrderDto>(completeOrder);
                
                if (user != null)
                {
                    orderDto.UserName = user.UserName;
                    orderDto.UserEmail = user.Email;
                }

                return orderDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting order number {OrderNumber} for user ID {UserId}", orderNumber, userId);
                throw;
            }
        }

        public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(int userId)
        {
            try
            {
                var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
                var user = await _userRepository.GetByIdAsync(userId);
                var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders).ToList();
                
                if (user != null)
                {
                    foreach (var orderDto in orderDtos)
                    {
                        orderDto.UserName = user.UserName;
                        orderDto.UserEmail = user.Email;
                    }
                }

                return orderDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting orders for user ID {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, UpdateOrderStatusDto updateOrderStatusDto)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(orderId);
                if (order == null)
                {
                    return false;
                }

                order.Status = updateOrderStatusDto.Status;
                order.UpdatedAt = DateTime.UtcNow;

                await _orderRepository.UpdateAsync(order);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating status for order ID {OrderId}", orderId);
                throw;
            }
        }

        public async Task<bool> CancelOrderAsync(int userId, int orderId)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(orderId);
                if (order == null || order.UserId != userId)
                {
                    return false;
                }

                // Only allow cancellation of pending or processing orders
                if (order.Status != "pending" && order.Status != "processing")
                {
                    throw new InvalidOperationException($"Cannot cancel order with status: {order.Status}");
                }

                // Update order status
                order.Status = "cancelled";
                order.UpdatedAt = DateTime.UtcNow;
                await _orderRepository.UpdateAsync(order);

                // Restore product stock
                var orderItems = await _orderItemRepository.GetOrderItemsByOrderIdAsync(orderId);
                foreach (var orderItem in orderItems)
                {
                    var product = await _productRepository.GetByIdAsync(orderItem.ProductId);
                    if (product != null)
                    {
                        product.Stock += orderItem.Quantity;
                        await _productRepository.UpdateAsync(product);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling order ID {OrderId} for user ID {UserId}", orderId, userId);
                throw;
            }
        }

        public async Task<bool> ProcessPaymentAsync(int userId, int orderId, string paymentMethod)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(orderId);
                if (order == null || order.UserId != userId)
                {
                    return false;
                }

                // In a real application, you would integrate with a payment gateway here
                // For now, just simulate a successful payment

                order.PaymentMethod = paymentMethod;
                order.PaymentStatus = "paid";
                order.PaidAt = DateTime.UtcNow;
                order.UpdatedAt = DateTime.UtcNow;
                
                // If payment is successful, update order status to processing
                if (order.Status == "pending")
                {
                    order.Status = "processing";
                }

                await _orderRepository.UpdateAsync(order);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing payment for order ID {OrderId} for user ID {UserId}", orderId, userId);
                throw;
            }
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _orderRepository.GetAllAsync();
                var orderDtos = new List<OrderDto>();

                foreach (var order in orders)
                {
                    var user = await _userRepository.GetByIdAsync(order.UserId);
                    var orderDto = _mapper.Map<OrderDto>(order);
                    
                    if (user != null)
                    {
                        orderDto.UserName = user.UserName;
                        orderDto.UserEmail = user.Email;
                    }
                    
                    orderDtos.Add(orderDto);
                }

                return orderDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all orders");
                throw;
            }
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByStatusAsync(string status)
        {
            try
            {
                var orders = await _orderRepository.GetOrdersByStatusAsync(status);
                var orderDtos = new List<OrderDto>();

                foreach (var order in orders)
                {
                    var user = await _userRepository.GetByIdAsync(order.UserId);
                    var orderDto = _mapper.Map<OrderDto>(order);
                    
                    if (user != null)
                    {
                        orderDto.UserName = user.UserName;
                        orderDto.UserEmail = user.Email;
                    }
                    
                    orderDtos.Add(orderDto);
                }

                return orderDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting orders by status {Status}", status);
                throw;
            }
        }

        public async Task<bool> UpdateOrderAfterPaymentAsync(int orderId, string transactionId)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(orderId);
                if (order == null)
                {
                    return false;
                }

                // Update both order status and payment status
                order.Status = "processing";  // Standard status value after payment
                order.PaymentStatus = "paid";
                order.PaidAt = DateTime.UtcNow;
                order.UpdatedAt = DateTime.UtcNow;
                
                await _orderRepository.UpdateAsync(order);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating order after payment for order ID {OrderId}", orderId);
                throw;
            }
        }

        private string GenerateOrderNumber()
        {
            // Generate a unique order number with format: ORD-YYYYMMDD-XXXX
            // Where XXXX is a random 4-digit number
            var dateString = DateTime.UtcNow.ToString("yyyyMMdd");
            var random = new Random();
            var randomNumber = random.Next(1000, 9999);
            return $"ORD-{dateString}-{randomNumber}";
        }
    }
} 