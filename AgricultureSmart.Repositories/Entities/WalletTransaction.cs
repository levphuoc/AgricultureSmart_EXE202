using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Entities
{
    public class WalletTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int WalletId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; } // "deposit", "withdrawal", "payment", "refund"

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string ReferenceId { get; set; } // For linking to orders or other transactions

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        [ForeignKey("WalletId")]
        public virtual Wallet Wallet { get; set; }
    }
}
