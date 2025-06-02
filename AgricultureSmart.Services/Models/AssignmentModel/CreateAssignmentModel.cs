using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.AssignmentModel
{
    public class CreateAssignmentModel
    {
        [Required]
        public int EngineerId { get; set; }

        [Required]
        public int FarmerId { get; set; }

        public bool IsActive { get; set; } = true;

        public string Notes { get; set; }
    }
}
