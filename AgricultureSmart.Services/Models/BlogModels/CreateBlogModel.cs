using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.BlogModels
{
    public class CreateBlogModel
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        public string? FeaturedImage { get; set; }

        [Required]
        public string Slug { get; set; } = null!;

        public string Status { get; set; } = "draft";
    }
}
