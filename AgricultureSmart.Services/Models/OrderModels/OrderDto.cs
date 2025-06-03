using System;
using System.Collections.Generic;
using System.Linq;

namespace AgricultureSmart.Services.Models.OrderModels
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
        public int TotalItems => OrderItems.Sum(oi => oi.Quantity);
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
} 