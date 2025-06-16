using AgricultureSmart.Services.Extension;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.OrderModels;
using AgricultureSmart.Services.Models.VnPayModels;
using AgricultureSmart.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IOrderService _orderService;

        public PaymentController(IVnPayService vnPayService, IOrderService orderService)
        {
            _vnPayService = vnPayService;
            _orderService = orderService;
        }

        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequestDto request)
        {
            int userId = User.GetUserId();
            if (userId == 0) return Unauthorized();

            try
            {
                // Lấy thông tin đơn hàng từ OrderService
                var order = await _orderService.GetOrderByIdAsync(userId, request.OrderId);
                if (order == null)
                {
                    return NotFound("Không tìm thấy đơn hàng");
                }

                // Tạo model cho VnPay
                var vnPayRequest = new VnPaymentRequestModel
                {
                    OrderId = order.OrderNumber,
                    Amount = Convert.ToDouble(order.TotalAmount),
                    Description = $"Thanh toán đơn hàng {order.OrderNumber}",
                    CreatedDate = DateTime.Now,
                    FullName = request.FullName
                };

                // Tạo URL thanh toán
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
        public async Task<IActionResult> PaymentCallback()
        {
            try
            {
                var response = _vnPayService.PaymentExcute(Request.Query);

                if (response.Success)
                {
                    // Cập nhật trạng thái đơn hàng
                    string orderNumber = response.OrderId;
                    // Tách orderNumber để lấy ID đơn hàng từ format "{orderId}_{timestamp}"
                    string[] orderParts = orderNumber.Split('_');
                    if (orderParts.Length > 0)
                    {
                        int orderId;
                        if (int.TryParse(orderParts[0], out orderId))
                        {
                            // Cập nhật cả trạng thái đơn hàng và trạng thái thanh toán
                            await _orderService.UpdateOrderAfterPaymentAsync(orderId, response.TransactionId);

                            // Redirect về trang thành công
                            return Redirect($"/payment/success?orderId={orderId}&transactionId={response.TransactionId}");
                        }
                    }

                    return Redirect("/payment/success");
                }
                else
                {
                    // Redirect về trang thất bại
                    return Redirect("/payment/failed");
                }
            }
            catch (Exception ex)
            {
                return Redirect($"/payment/error?message={ex.Message}");
            }
        }
    }

    public class PaymentRequestDto
    {
        public int OrderId { get; set; }
        public string FullName { get; set; }
    }
}
