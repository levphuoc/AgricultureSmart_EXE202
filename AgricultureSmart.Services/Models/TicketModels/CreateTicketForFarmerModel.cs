using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.TicketModels
{
    public class CreateTicketForFarmerModel
    {
        [Required] public string Title { get; set; } = default!;
        [Required] public string Category { get; set; } = default!;
        public string? CropType { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }

        // Ưu tiên mặc định (low, normal, high, urgent…)
        public string Priority { get; set; } = "normal";

        // Cách liên lạc (phone, email, zalo…)
        public string ContactMethod { get; set; } = "phone";
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
    }
}
