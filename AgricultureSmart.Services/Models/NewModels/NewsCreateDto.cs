using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.NewModels
{
    public class NewsCreateDto
    {
        [Required, StringLength(500)]
        public string Title { get; set; }

        [Required]
        public string Excerpt { get; set; }

        [Required]
        public string Content { get; set; }

        [Required, StringLength(255)]
        public string Author { get; set; }

        [Required]
        public DateTime PublishedAt { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public bool Featured { get; set; }
        public bool Urgent { get; set; }
        public string Tags { get; set; }
        public string Source { get; set; }
        public string ImageUrl { get; set; }
    }
}
