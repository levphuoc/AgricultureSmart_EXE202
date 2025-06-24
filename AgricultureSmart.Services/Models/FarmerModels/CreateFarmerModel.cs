using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.FarmerModels
{
    public class CreateFarmerModel
    {
        // User fields
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;

        // Farmer fields
        public string FarmLocation { get; set; } = default!;
        public decimal FarmSize { get; set; }
        public string CropTypes { get; set; } = default!;
        public int FarmingExperienceYears { get; set; }
    }
}
