using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.BlogModels
{
    public class UpdateBlogModel
    {
        public int CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? FeaturedImage { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
