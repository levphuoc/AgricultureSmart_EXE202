using System.ComponentModel.DataAnnotations;

namespace AgricultureSmart.API.Models
{
    public class BlogCategoryModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Slug is required")]
        [StringLength(255, ErrorMessage = "Slug cannot exceed 255 characters")]
        public string Slug { get; set; }

        public bool IsActive { get; set; } = true;
    }

    // Model for creating a new blog - without ID field
    public class BlogCreateModel
    {
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(500, ErrorMessage = "Title cannot exceed 500 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        public string FeaturedImage { get; set; }

        [Required(ErrorMessage = "Slug is required")]
        [StringLength(500, ErrorMessage = "Slug cannot exceed 500 characters")]
        public string Slug { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters")]
        public string Status { get; set; } = "draft";

        public DateTime? PublishedAt { get; set; }
    }

    // Model for updating an existing blog - with ID field
    public class BlogModel
    {
        // ID will be auto-generated, not required for creation
        public int Id { get; set; } = 0;

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(500, ErrorMessage = "Title cannot exceed 500 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        public string FeaturedImage { get; set; }

        [Required(ErrorMessage = "Slug is required")]
        [StringLength(500, ErrorMessage = "Slug cannot exceed 500 characters")]
        public string Slug { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters")]
        public string Status { get; set; } = "draft";

        public DateTime? PublishedAt { get; set; }
    }

    public class BlogListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Status { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int ViewCount { get; set; }
    }

    public class BlogDetailModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string FeaturedImage { get; set; }
        public string Slug { get; set; }
        public string Status { get; set; }
        public int ViewCount { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string AuthorName { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
    }
} 