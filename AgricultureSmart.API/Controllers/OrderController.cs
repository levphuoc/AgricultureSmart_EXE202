using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.OrderModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgricultureSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // POST: api/Order
        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            // TODO: Get the actual user ID from authentication
            int userId = 1; // Temporary hardcoded user ID for testing

            try
            {
                var order = await _orderService.CreateOrderFromCartAsync(userId, createOrderDto);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetUserOrders()
        {
            // TODO: Get the actual user ID from authentication
            int userId = 1; // Temporary hardcoded user ID for testing

            var orders = await _orderService.GetUserOrdersAsync(userId);
            return Ok(orders);
        }

        // GET: api/Order/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            // TODO: Get the actual user ID from authentication
            int userId = 1; // Temporary hardcoded user ID for testing

            var order = await _orderService.GetOrderByIdAsync(userId, id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // GET: api/Order/number/{orderNumber}
        [HttpGet("number/{orderNumber}")]
        public async Task<ActionResult<OrderDto>> GetOrderByOrderNumber(string orderNumber)
        {
            // TODO: Get the actual user ID from authentication
            int userId = 1; // Temporary hardcoded user ID for testing

            var order = await _orderService.GetOrderByOrderNumberAsync(userId, orderNumber);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Order/{id}/cancel
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(int id)
        {
            // TODO: Get the actual user ID from authentication
            int userId = 1; // Temporary hardcoded user ID for testing

            var result = await _orderService.CancelOrderAsync(userId, id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Order/{id}/payment
        [HttpPost("{id}/payment")]
        public async Task<IActionResult> ProcessPayment(int id, [FromBody] string paymentMethod)
        {
            // TODO: Get the actual user ID from authentication
            int userId = 1; // Temporary hardcoded user ID for testing

            var result = await _orderService.ProcessPaymentAsync(userId, id, paymentMethod);
            if (!result)
            {
                return BadRequest("Payment processing failed");
            }

            return Ok("Payment processed successfully");
        }

        // Admin endpoints (should be protected with proper authorization)
        // GET: api/Order/admin/all
        [HttpGet("admin/all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // GET: api/Order/admin/status/{status}
        [HttpGet("admin/status/{status}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByStatus(string status)
        {
            var orders = await _orderService.GetOrdersByStatusAsync(status);
            return Ok(orders);
        }

        // PUT: api/Order/admin/{id}/status
        [HttpPut("admin/{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusDto updateOrderStatusDto)
        {
            var result = await _orderService.UpdateOrderStatusAsync(id, updateOrderStatusDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
} 