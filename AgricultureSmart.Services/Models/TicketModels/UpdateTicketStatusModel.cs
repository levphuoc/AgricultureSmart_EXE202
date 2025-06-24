using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.TicketModels
{
    public class UpdateTicketStatusModel
    {
        [Required]
        [StringLength(20)]
        public string Status { get; set; } // "open", "assigned", "in_progress", "resolved", "closed"

        public int? AssignedEngineerId { get; set; } // Required when status is "assigned"

        public string? Notes { get; set; } // Optional notes for status change
    }
}
