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
        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        public string Password { get; set; }

        // Engineer info
        [StringLength(255)]
        public string Specialization { get; set; }

        [Range(0, 100)]
        public int ExperienceYears { get; set; }

        public string Certification { get; set; }

        public string Bio { get; set; }
    }
}
