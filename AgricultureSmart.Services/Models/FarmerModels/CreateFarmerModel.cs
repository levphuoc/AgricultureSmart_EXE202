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
        [Required]
        public int UserId { get; set; }

        [StringLength(255)]
        public string FarmLocation { get; set; }

        [Range(0.01, 10000)]
        public decimal FarmSize { get; set; }

        public string CropTypes { get; set; }

        [Range(0, 100)]
        public int FarmingExperienceYears { get; set; }
    }
}
