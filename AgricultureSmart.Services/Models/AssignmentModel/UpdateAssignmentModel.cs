using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.AssignmentModel
{
    public class UpdateAssignmentModel
    {
        [Required]
        public int Id { get; set; }

        public bool IsActive { get; set; }

        public string Notes { get; set; }
    }
}
