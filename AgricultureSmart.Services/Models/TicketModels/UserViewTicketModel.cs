using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.TicketModels
{
    // Thông tin Farmer (đã flatten User)
    public class FarmerViewTicketModel
    {
        public int Id { get; set; }          // Farmer.Id
        public int UserId { get; set; }      // Users.Id
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public string FarmLocation { get; set; }
        public decimal FarmSize { get; set; }
        public string CropTypes { get; set; }
        public int FarmingExperienceYears { get; set; }

        public DateTime CreatedAt { get; set; }  // Farmer.CreatedAt
        public DateTime UpdatedAt { get; set; }  // Farmer.UpdatedAt
    }

    // Thông tin Engineer (cũng flatten User)
    public class EngineerViewTicketModel
    {
        public int Id { get; set; }          // Engineer.Id
        public int UserId { get; set; }      // Users.Id
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public string Specialization { get; set; }
        public int ExperienceYears { get; set; }
        public string Certification { get; set; }
        public string Bio { get; set; }

        public DateTime CreatedAt { get; set; }  // Engineer.CreatedAt
        public DateTime UpdatedAt { get; set; }  // Engineer.UpdatedAt
    }
}
