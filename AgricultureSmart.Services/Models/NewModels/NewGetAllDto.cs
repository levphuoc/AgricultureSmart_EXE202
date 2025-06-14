using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Models.NewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace AgricultureSmart.Services.Models.NewModels
    {
        public class NewGetAllDto
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Excerpt { get; set; }
            public string Author { get; set; }
            public DateTime PublishedAt { get; set; }
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public bool Featured { get; set; }
            public bool Urgent { get; set; }
            public string Tags { get; set; }
            public string Source { get; set; }
            public string ImageUrl { get; set; }
            public int ViewCount { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
        }
    }
}
