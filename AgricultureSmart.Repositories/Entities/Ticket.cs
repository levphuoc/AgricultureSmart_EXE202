using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Entities
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int FarmerId { get; set; }

        public int? AssignedEngineerId { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; } // Tiêu đề yêu cầu

        [Required]
        [StringLength(100)]
        public string Category { get; set; } // Danh mục

        [Required]
        [StringLength(100)]
        public string CropType { get; set; } // Loại cây trồng

        [Required]
        [StringLength(200)]
        public string Location { get; set; } // Vị trí

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; } // Mô tả chi tiết

        [StringLength(100)]
        public string Priority { get; set; } // Mức độ ưu tiên: "low", "medium", "high", "urgent"

        [StringLength(50)]
        public string ContactMethod { get; set; } // Phương thức liên hệ: "Điện thoại", "Email", v.v.

        [StringLength(100)]
        public string PhoneNumber { get; set; } // Số điện thoại

        [StringLength(500)]
        public string ImageUrl { get; set; } // Hình ảnh minh họa

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // "open", "assigned", "in_progress", "resolved", "closed"

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? ResolvedAt { get; set; }

        // Navigation properties
        [ForeignKey("FarmerId")]
        public virtual Farmer Farmer { get; set; }

        [ForeignKey("AssignedEngineerId")]
        public virtual Engineer AssignedEngineer { get; set; }

        public virtual ICollection<TicketComment> Comments { get; set; }

    }
}
