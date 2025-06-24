using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.FarmerModels
{
    public class UpdateFarmerModel
    {
        // User fields
        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; } 

        // Farmer fields
        [StringLength(255)]
        public string FarmLocation { get; set; }

        [Range(0.01, 10000)]
        public decimal FarmSize { get; set; }

        public string CropTypes { get; set; }

        [Range(0, 100)]
        public int FarmingExperienceYears { get; set; }
    }
}
