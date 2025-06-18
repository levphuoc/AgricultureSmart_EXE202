using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.VnPayModels;
using AgricultureSmart.Services.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<VnPayService> _logger;

        public VnPayService(IConfiguration config, ILogger<VnPayService> logger = null)
        {
            _config = config;
            _logger = logger;
        }
        public string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model)
        {
            var tick = DateTime.Now.ToString("yyyyMMddHHmmss");
            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", _config["VnPay:Version"]);
            vnpay.AddRequestData("vnp_Command", _config["VnPay:Command"]);
            vnpay.AddRequestData("vnp_TmnCode", _config["VnPay:TmnCode"]);
            vnpay.AddRequestData("vnp_Amount", (model.Amount * 100).ToString());
            //Số tiền thanh toán. Số tiền không 
            //mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND
            //(một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần(khử phần thập phân), sau đó gửi sang VNPAY
            //là: 10000000

            vnpay.AddRequestData("vnp_CreateDate", model.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _config["VnPay:CurrCode"]);
            vnpay.AddRequestData("vnp_IpAddr", AgricultureSmart.Services.Utils.Utils.GetIpAddress(context));
            vnpay.AddRequestData("vnp_Locale", _config["VnPay:Locale"]);

            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + model.OrderId);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", _config["VnPay:ReturnUrl"]);
            vnpay.AddRequestData("vnp_TxnRef", $"{model.OrderIdNumeric}_{tick}"); //Mã đơn hàng, merchant tự sinh ra và đảm bảo duy nhất trong hệ thống của mình
            var paymentUrl = vnpay.CreateRequestUrl(_config["VnPay:BaseUrl"], _config["VnPay:HashSecret"]);
            return paymentUrl;
        }

        public VnPaymentResponseModel PaymentExcute(IQueryCollection collection)
        {
            try
            {
                _logger?.LogInformation("Processing VnPay payment execution with {Count} parameters", collection.Count);
                
                var vnpay = new VnPayLibrary();
                foreach (var (key, value) in collection)
                {
                    if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(key, value.ToString());
                        _logger?.LogDebug("Added VnPay response data: {Key}={Value}", key, value);
                    }
                }

                // Kiểm tra chi tiết về vnp_TxnRef nếu có
                if (collection.ContainsKey("vnp_TxnRef"))
                {
                    var txnRef = collection["vnp_TxnRef"].ToString();
                    _logger?.LogInformation("Found vnp_TxnRef in collection: {TxnRef}", txnRef);
                }
                else
                {
                    _logger?.LogWarning("No vnp_TxnRef found in VNPay response");
                }
                
                // Check if we have the essential parameters
                if (!collection.ContainsKey("vnp_TxnRef") || !collection.ContainsKey("vnp_TransactionNo"))
                {
                    _logger?.LogWarning("Missing essential VnPay parameters in callback");
                    return new VnPaymentResponseModel
                    {
                        Success = false,
                        PaymentMethod = "VnPay",
                        VnPayResponseCode = "99", // Custom code for missing parameters
                        OrderId = "unknown"
                    };
                }
                
                var vnp_orderId = Convert.ToString(vnpay.GetResponseData("vnp_TxnRef"));
                _logger?.LogInformation("Original vnp_TxnRef value: {TxnRef}", vnp_orderId);
                
                // Check if the order ID is in the format "ORD-YYYYMMDD-XXXX_timestamp"
                if (vnp_orderId.StartsWith("ORD-") && vnp_orderId.Contains("_"))
                {
                    // Extract just the order number part (before the underscore)
                    vnp_orderId = vnp_orderId.Split('_')[0];
                    _logger?.LogInformation("Extracted order number from vnp_TxnRef: {OrderNumber}", vnp_orderId);
                }
                // Bỏ qua việc kiểm tra format nếu vnp_TxnRef không chứa dấu gạch dưới
                else if (!vnp_orderId.Contains("_"))
                {
                    _logger?.LogWarning("vnp_TxnRef does not contain underscore, using as is: {TxnRef}", vnp_orderId);
                    // Không cần xử lý gì thêm, sử dụng nguyên giá trị
                }
                
                var vnp_TransactionId = Convert.ToString(vnpay.GetResponseData("vnp_TransactionNo"));
                var vnp_ResponseCode = Convert.ToString(vnpay.GetResponseData("vnp_ResponseCode"));
                var vnp_OrderInfo = Convert.ToString(vnpay.GetResponseData("vnp_OrderInfo"));

                bool checkSignature = true;
                var vnp_SecureHash = collection.FirstOrDefault(x => x.Key == "vnp_SecureHash").Value;
                
                if (!string.IsNullOrEmpty(vnp_SecureHash))
                {
                    checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config["VnPay:HashSecret"]);
                }
                else
                {
                    _logger?.LogWarning("Missing VnPay secure hash in callback");
                    checkSignature = false;
                }

                if (!checkSignature)
                {
                    _logger?.LogWarning("VnPay signature validation failed");
                }
                
                if (string.IsNullOrEmpty(vnp_ResponseCode))
                {
                    _logger?.LogWarning("Missing VnPay response code in callback");
                    vnp_ResponseCode = "99"; // Custom code for missing response code
                }

                if (!checkSignature || vnp_ResponseCode != "00")
                {
                    return new VnPaymentResponseModel
                    {
                        Success = false,
                        PaymentMethod = "VnPay",
                        OrderDescription = vnp_OrderInfo,
                        OrderId = vnp_orderId,
                        PaymentId = vnp_TransactionId,
                        TransactionId = vnp_TransactionId,
                        Token = vnp_SecureHash,
                        VnPayResponseCode = vnp_ResponseCode
                    };
                }
                
                _logger?.LogInformation("VnPay payment successful for order {OrderId}", vnp_orderId);
                return new VnPaymentResponseModel
                {
                    Success = true,
                    PaymentMethod = "VnPay",
                    OrderDescription = vnp_OrderInfo,
                    OrderId = vnp_orderId,
                    PaymentId = vnp_TransactionId,
                    TransactionId = vnp_TransactionId,
                    Token = vnp_SecureHash,
                    VnPayResponseCode = vnp_ResponseCode
                };
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error processing VnPay payment execution");
                return new VnPaymentResponseModel
                {
                    Success = false,
                    PaymentMethod = "VnPay",
                    VnPayResponseCode = "99", // Custom code for exception
                    OrderId = "error"
                };
            }
        }
    }
}
