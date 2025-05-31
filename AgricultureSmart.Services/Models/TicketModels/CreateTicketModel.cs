using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.TicketModels
{
    public class CreateTicketModel
    {
        public int FarmerId { get; set; }
        public int? AssignedEngineerId { get; set; }

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
    }
}
