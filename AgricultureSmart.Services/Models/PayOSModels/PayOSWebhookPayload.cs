using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.PayOSModels
{
    public class PayOSWebhookPayload
    {
        public int code { get; set; }
        public string desc { get; set; }
        public int orderCode { get; set; }
        public int amount { get; set; }
        public string signature { get; set; }
        public string checkoutUrl { get; set; }
        public string paymentLinkId { get; set; }
        public string status { get; set; } 
        public long? transactionDateTime { get; set; } 
        public string transactionId { get; set; } 
    }
}
