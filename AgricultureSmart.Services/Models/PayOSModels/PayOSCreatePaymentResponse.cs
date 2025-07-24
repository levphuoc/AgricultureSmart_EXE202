using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.PayOSModels
{
    public class PayOSCreatePaymentResponse
    {
        public int code { get; set; }
        public string desc { get; set; }
        public string signature { get; set; }
        public PayOSCreatePaymentResponseData data { get; set; }
    }
}
