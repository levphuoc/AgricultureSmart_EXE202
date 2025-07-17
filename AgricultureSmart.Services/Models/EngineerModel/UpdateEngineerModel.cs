using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.EngineerModel
{
    public class UpdateEngineerModel
    {
        // User info
        [StringLength(100)]
        public string? Username { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        public string? Password { get; set; }

        // Engineer info
        [StringLength(255)]
        public string? Specialization { get; set; }

        [Range(0, 10000)]
        public int? ExperienceYears { get; set; }

        public string? Certification { get; set; }

        public string? Bio { get; set; }
    }
}
