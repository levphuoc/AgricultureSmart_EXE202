/*using AgricultureSmart.Services.Models.VNPayModels;
using AgricultureSmart.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgricultureSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;

        public PaymentController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }

        /// <summary>
        /// Tạo URL thanh toán VNPay (FE lấy link và redirect người dùng).
        /// </summary>
        [HttpPost("create-vnpay")]
        public async Task<ActionResult> CreateVnPayLink([FromBody] PaymentRequestModelView model)
        {
            var url = await _vnPayService.CreatePaymentUrlAsync(model, HttpContext);
            return Ok(new { Url = url });
        }

        /// <summary>
        /// VNPay gọi lại URL này (ReturnUrl) hoặc FE redirect về đây kèm query‑string.
        /// Kiểm tra chữ ký, lưu DB, trả về thông báo.
        /// </summary>
        [AllowAnonymous]             // VNPay sẽ gọi mà không có JWT
        [HttpGet("execute-vnpay")]
        public async Task<IActionResult> ExecuteVnPayCallback([FromQuery] PaymentResponseModelView response)
        {
            string message = await _vnPayService.ExecutePaymentAsync(response);
            return Ok(new { Message = message });
        }
    }
}
*/