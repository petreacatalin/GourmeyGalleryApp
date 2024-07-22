using System.ComponentModel.DataAnnotations;

namespace GourmeyGalleryApp.Models.DTOs
{
    public class UserRegistrationRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } 

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } 
        public string? ProfilePictureUrl { get; set; }
    }
}

