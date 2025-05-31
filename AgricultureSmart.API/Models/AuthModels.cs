using System.ComponentModel.DataAnnotations;

namespace AgricultureSmart.API.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        
        [Phone]
        public string PhoneNumber { get; set; }

        [Range(0, 3, ErrorMessage = "Role must be either Engineer (2) or Farmer (3), or 0 for default")]
        public int RoleId { get; set; } = 3; // Default to Farmer role
    }

    public class JwtResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
} 