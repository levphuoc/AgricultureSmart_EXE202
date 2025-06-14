using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.VNPayModels
{
    public class PaymentResponseModelView
    {
        public string OrderId { get; set; }

        public decimal TotalAmount { get; set; }
        public DateTime PaymentTime { get; set; }
        public string? BankCode { get; set; }
        public string? BankTranNo { get; set; }
        public string? CardType { get; set; }
        public string? ResponseCode { get; set; }
        public string? TransactionNo { get; set; }
        public string? TransactionStatus { get; set; }
        public string? Method { get; set; }
    }
}
