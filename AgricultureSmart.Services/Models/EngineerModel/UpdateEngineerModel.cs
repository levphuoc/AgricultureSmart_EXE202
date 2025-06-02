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
        [Required]
        public int Id { get; set; }

        [StringLength(255)]
        public string Specialization { get; set; }

        [Range(0, 100)]
        public int ExperienceYears { get; set; }

        public string Certification { get; set; }

        public string Bio { get; set; }
    }
}
