using System.ComponentModel.DataAnnotations;

namespace AgricultureSmart.Services.Models.OrderModels
{
    public class CreateOrderDto
    {
        [Required]
        [StringLength(255)]
        public string ShippingAddress { get; set; }
        
        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }
    }
} 