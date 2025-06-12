using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Entities
{
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Excerpt { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; }

        [Required]
        [StringLength(255)]
        public string Author { get; set; }

        [Required]
        public DateTime PublishedAt { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public bool Featured { get; set; }

        public bool Urgent { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Tags { get; set; } // JSON string for tags array

        [StringLength(255)]
        public string Source { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        public int ViewCount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("CategoryId")]
        public virtual NewsCategory Category { get; set; }
    }
}
