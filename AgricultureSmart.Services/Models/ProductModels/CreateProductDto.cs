using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.ProductModels
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
        public int Stock { get; set; } = 0;

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        [StringLength(100)]
        public string SKU { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Discount price cannot be negative")]
        public decimal? DiscountPrice { get; set; }
    }
} 