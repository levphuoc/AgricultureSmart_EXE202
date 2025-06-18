using AgricultureSmart.Services.Extension;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.OrderModels;
using AgricultureSmart.Services.Models.VnPayModels;
using AgricultureSmart.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using System.Text;

namespace AgricultureSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(
            IVnPayService vnPayService, 
            IOrderService orderService, 
            IConfiguration configuration, 
            ILogger<PaymentController> logger = null)
        {
            _vnPayService = vnPayService;
            _orderService = orderService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequestDto request)
        {
            int userId = User.GetUserId();
            if (userId == 0) return Unauthorized();

            try
            {
                var order = await _orderService.GetOrderByIdAsync(userId, request.OrderId);
                if (order == null)
                {
                    return NotFound("Order not found");
                }

                var vnPayRequest = new VnPaymentRequestModel
                {
                    OrderId = order.OrderNumber,
                    OrderIdNumeric = order.Id,
                    Amount = Convert.ToDouble(order.TotalAmount),
                    Description = $"Payment for order {order.OrderNumber}",
                    CreatedDate = DateTime.Now,
                    FullName = request.FullName
                };

                var paymentUrl = _vnPayService.CreatePaymentUrl(HttpContext, vnPayRequest);

                return Ok(new { PaymentUrl = paymentUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("payment-callback")]
        [AllowAnonymous]
        [EnableCors("AllowAll")]
        public async Task<IActionResult> PaymentCallback()
        {
            try
            {
                if (Request.Query.Count == 0)
                {
                    return BadRequest("No payment information received");
                }

                var response = _vnPayService.PaymentExcute(Request.Query);
                string frontendUrl = _configuration["FrontendUrl"] ?? "http://localhost:3000";
                
                if (response.Success)
                {
                    string orderNumber = response.OrderId;
                    
                    if (string.IsNullOrEmpty(orderNumber))
                    {
                        return Redirect($"{frontendUrl}/payment/error?message=Order number is empty");
                    }
                    
                    if (orderNumber.StartsWith("ORD-"))
                    {
                        var allOrders = await _orderService.GetAllOrdersAsync();
                        var matchingOrder = allOrders.FirstOrDefault(o => o.OrderNumber == orderNumber);
                        
                        if (matchingOrder != null)
                        {
                            int orderId = matchingOrder.Id;
                            var order = await _orderService.GetOrderByIdWithoutUserCheckAsync(orderId);
                            
                            if (order == null)
                            {
                                return Redirect($"{frontendUrl}/payment/error?message=Order not found&orderId={orderId}");
                            }
                            
                            bool updateResult = await _orderService.UpdateOrderAfterPaymentAsync(orderId, response.TransactionId);
                            
                            if (updateResult)
                            {
                                return Redirect($"{frontendUrl}/payment/success?orderId={orderId}");
                            }
                            else
                            {
                                return Redirect($"{frontendUrl}/payment/error?message=Failed to update order&orderId={orderId}");
                            }
                        }
                        else
                        {
                            return Redirect($"{frontendUrl}/payment/error?message=Order not found with number {orderNumber}");
                        }
                    }
                    
                    string[] orderParts = orderNumber.Split('_');
                    
                    if (orderParts.Length > 0)
                    {
                        int orderId;
                        if (int.TryParse(orderParts[0], out orderId))
                        {
                            var order = await _orderService.GetOrderByIdWithoutUserCheckAsync(orderId);
                            
                            if (order == null)
                            {
                                return Redirect($"{frontendUrl}/payment/error?message=Order not found&orderId={orderId}");
                            }
                            
                            bool updateResult = await _orderService.UpdateOrderAfterPaymentAsync(orderId, response.TransactionId);
                            
                            if (updateResult)
                            {
                                return Redirect($"{frontendUrl}/payment/success?orderId={orderId}");
                            }
                            else
                            {
                                return Redirect($"{frontendUrl}/payment/error?message=Failed to update order&orderId={orderId}");
                            }
                        }
                    }

                    return Redirect($"{frontendUrl}/payment/error?message=Could not determine order ID");
                }
                else
                {
                    return Redirect($"{frontendUrl}/payment/error?message=Payment failed with code {Request.Query["vnp_ResponseCode"]}");
                }
            }
            catch (Exception ex)
            {
                string frontendUrl = _configuration["FrontendUrl"] ?? "http://localhost:3000";
                return Redirect($"{frontendUrl}/payment/error?message={WebUtility.UrlEncode(ex.Message)}");
            }
        }

        [HttpGet("check-order/{orderId}")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckOrder(int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderByIdWithoutUserCheckAsync(orderId);
                if (order == null)
                {
                    return NotFound(new { message = $"Order with ID {orderId} not found" });
                }
                
                return Ok(new { 
                    orderId = order.Id,
                    orderNumber = order.OrderNumber,
                    status = order.Status,
                    paymentStatus = order.PaymentStatus,
                    total = order.TotalAmount,
                    createdAt = order.CreatedAt,
                    updatedAt = order.UpdatedAt
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }

    public class PaymentRequestDto
    {
        public int OrderId { get; set; }
        public string FullName { get; set; }
    }
}
