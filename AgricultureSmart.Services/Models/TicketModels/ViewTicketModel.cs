﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.TicketModels
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string CropType { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public string Priority { get; set; }
        public string? ContactMethod { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }

        public int FarmerId { get; set; }
        public string? FarmerName { get; set; }

        public int? AssignedEngineerId { get; set; }
        public string? EngineerName { get; set; }
    }
}
