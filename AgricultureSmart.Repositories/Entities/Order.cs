using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } // "pending", "processing", "shipped", "delivered", "cancelled"

        [StringLength(255)]
        public string ShippingAddress { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; } // "wallet", "cod", "bank_transfer"

        [StringLength(50)]
        public string PaymentStatus { get; set; } // "pending", "paid", "failed"

        public DateTime? PaidAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
