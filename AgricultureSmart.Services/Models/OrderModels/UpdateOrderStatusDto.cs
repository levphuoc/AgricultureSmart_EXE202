using System.ComponentModel.DataAnnotations;

namespace AgricultureSmart.Services.Models.OrderModels
{
    public class UpdateOrderStatusDto
    {
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
    }
} 