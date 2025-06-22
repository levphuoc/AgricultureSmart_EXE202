using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.TicketModels
{
    // Thông tin User cơ bản – được tái sử dụng cho cả Farmer và Engineer
    public class UserViewTicketModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }

    // Thông tin Farmer đầy đủ
    public class FarmerViewTicketModel
    {
        public int Id { get; set; }
        public UserViewTicketModel User { get; set; }

        public string FarmLocation { get; set; }
        public decimal FarmSize { get; set; }
        public string CropTypes { get; set; }
        public int FarmingExperienceYears { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    // Thông tin Engineer đầy đủ
    public class EngineerViewTicketModel
    {
        public int Id { get; set; }
        public UserViewTicketModel User { get; set; }

        public string Specialization { get; set; }
        public int ExperienceYears { get; set; }
        public string Certification { get; set; }
        public string Bio { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
