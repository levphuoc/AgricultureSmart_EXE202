using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.VNPayModels
{
    public class PaymentRequestModelView
    {
        public double Amount { get; set; }
        public string Information { get; set; }
        public string Type { get; set; }

        public string TimeExpire { get; set; }

    }
}
