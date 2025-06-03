using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.CartModels
{
    public class CartDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
        public int TotalItems => CartItems.Sum(ci => ci.Quantity);
    }
} 