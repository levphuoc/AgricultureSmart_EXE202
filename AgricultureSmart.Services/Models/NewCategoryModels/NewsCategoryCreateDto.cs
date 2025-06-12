using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.NewCategoryModels
{
    public class NewsCategoryCreateDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string Slug { get; set; }

        public bool IsActive { get; set; }
    }

}
