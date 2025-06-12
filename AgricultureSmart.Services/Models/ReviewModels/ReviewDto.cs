using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.ReviewModels
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ReviewValue { get; set; }
        public string ReviewMessage { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
