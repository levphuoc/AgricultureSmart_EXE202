// AgricultureSmart.Services/Models/PayOSModels/PayOSCreatePaymentRequest.cs
using System.Collections.Generic;

namespace AgricultureSmart.Services.Models.PayOSModels
{
    public class PayOSCreatePaymentRequest
    {
        public int orderCode { get; set; }
        public int amount { get; set; }
        public string description { get; set; }
        public List<Item> items { get; set; }
        public string cancelUrl { get; set; }
        public string returnUrl { get; set; }
        public long? expiredAt { get; set; } 
        public string signature { get; set; } 
    }
}