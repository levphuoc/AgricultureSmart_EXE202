using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.ProductCategoryModels
{
    public class UpdateProductCategoryDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Slug { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
} 