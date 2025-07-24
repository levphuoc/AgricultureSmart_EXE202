using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.PayOSModels
{
    public class PayOSCreatePaymentResponseData
    {
        public string bin { get; set; }
        public string accountNumber { get; set; }
        public string accountName { get; set; }
        public int amount { get; set; }
        public string description { get; set; }
        public int orderCode { get; set; }
        public string qrCode { get; set; }
        public string checkoutUrl { get; set; }
    }
}
