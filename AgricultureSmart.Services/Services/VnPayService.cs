/*using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _cfg;
        private readonly IHttpContextAccessor _http;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public VnPayService(IConfiguration cfg,
                             IHttpContextAccessor http,
                             IUnitOfWork uow,
                             IMapper mapper)
        {
            _cfg = cfg;
            _http = http;
            _uow = uow;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public async Task<string> CreatePaymentUrlAsync(int orderId, HttpContext ctx)
        {
            // 1. Lấy Order
            var orderRepo = _uow.GetRepository<Order>();
            var order = await orderRepo.Entities.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null) return "Order not found";
            if (order.PaymentStatus == "paid") return "Order already paid";

            // 2. Tạm ghi trạng thái chờ thanh toán
            order.PaymentMethod = "VNPay";
            order.PaymentStatus = "pending";
            order.UpdatedAt = DateTime.UtcNow;
            await _uow.SaveAsync();

            // 3. Tạo link VNPay
            var vnp = new VNPayLibrary();
            vnp.AddRequestData("vnp_Version", "2.1.0");
            vnp.AddRequestData("vnp_Command", "pay");
            vnp.AddRequestData("vnp_TmnCode", _cfg["VnPay:TmnCode"]);
            vnp.AddRequestData("vnp_Amount", ((int)(order.TotalAmount * 100)).ToString());
            vnp.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnp.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));
            vnp.AddRequestData("vnp_CurrCode", "VND");
            vnp.AddRequestData("vnp_IpAddr", ctx?.Connection?.RemoteIpAddress?.ToString() ?? "0.0.0.0");
            vnp.AddRequestData("vnp_Locale", "vn");
            vnp.AddRequestData("vnp_OrderInfo", $"Thanh toán đơn #{order.OrderNumber}");
            vnp.AddRequestData("vnp_OrderType", "other");
            vnp.AddRequestData("vnp_TxnRef", order.OrderNumber);                          // duy nhất
            vnp.AddRequestData("vnp_ReturnUrl", _cfg["VnPay:ReturnUrl"]);                   // /api/payment/execute-vnpay

            string payUrl = vnp.CreateRequestUrl(_cfg["VnPay:PaymentUrl"], _cfg["VnPay:HashSecret"]);
            return payUrl;
        }

        /// <inheritdoc/>
        public async Task<string> ExecutePaymentAsync(PaymentResponseModelView res)
        {
            // 1. Xác thực checksum
            var vnp = new VNPayLibrary();
            if (!vnp.ValidateSignature(res.Vnp_SecureHash, _cfg["VnPay:HashSecret"]))
                return "INVALID CHECKSUM";

            // 2. Lấy Order theo vnp_TxnRef (OrderNumber)
            var orderRepo = _uow.GetRepository<Order>();
            var order = await orderRepo.Entities.FirstOrDefaultAsync(o => o.OrderNumber == res.TxnRef);
            if (order == null) return "Order not found";

            // 3. Cập nhật trạng thái thanh toán
            if (res.ResponseCode == "00")
            {
                order.PaymentStatus = "paid";
                order.Status = "processing";           // tuỳ logic
                order.PaidAt = DateTime.UtcNow;
            }
            else
            {
                order.PaymentStatus = "failed";
            }
            order.UpdatedAt = DateTime.UtcNow;
            await _uow.SaveAsync();

            return order.PaymentStatus == "paid" ? "Thanh toán thành công." : "Thanh toán thất bại.";
        }
    }
}
*/