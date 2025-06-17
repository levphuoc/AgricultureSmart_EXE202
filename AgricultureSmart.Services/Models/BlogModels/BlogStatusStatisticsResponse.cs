using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.BlogModels
{
    public class BlogStatusStatisticsResponse
    {
        public int All { get; set; }
        public int Draft { get; set; }
        public int Published { get; set; }
        public int Archived { get; set; }
    }
}
