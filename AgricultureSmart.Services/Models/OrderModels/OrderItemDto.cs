using AgricultureSmart.Services.Models.ProductModels;
using System;

namespace AgricultureSmart.Services.Models.OrderModels
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        
        // Include product information
        public ProductDto Product { get; set; } = null!;
    }
} 