/*using AgricultureSmart.Services.Models.VNPayModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Interfaces
{
    public interface IVnPayService
    {
        /// <summary>Tạo URL thanh toán cho đơn hàng.</summary>
        Task<string> CreatePaymentUrlAsync(int orderId, HttpContext ctx);

        /// <summary>Xử lý callback từ VNPay, cập nhật Order.</summary>
        Task<string> ExecutePaymentAsync(PaymentResponseModelView response);
    }

}
*/