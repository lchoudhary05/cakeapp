using System.ComponentModel.DataAnnotations; // For validation attributes

namespace CakeApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string? Password { get; set; }

        // Add other fields like FirstName, LastName if needed
        // public string FirstName { get; set; }
        // public string LastName { get; set; }
    }

}
