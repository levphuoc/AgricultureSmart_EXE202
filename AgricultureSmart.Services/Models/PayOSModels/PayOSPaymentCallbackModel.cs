using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.PayOSModels
{
    public class PayOSPaymentCallbackModel
    {
        public int code { get; set; } // 0 for success, others for error
        public string desc { get; set; }
        public int orderCode { get; set; }
        public string signature { get; set; }
    }
}
