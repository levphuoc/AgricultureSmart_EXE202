using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.VnPayModels
{
    public class VnPaymentRequestModel
    {
        public string FullName { get; set; }
        public string Description { get; set; }

        public double Amount { get; set; }

        public DateTime CreatedDate { get; set; }
        public string OrderId { get; set; }
    }
}
