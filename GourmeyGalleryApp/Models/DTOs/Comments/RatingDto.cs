using GourmeyGalleryApp.Models.DTOs.ApplicationUser;
using GourmeyGalleryApp.Models.DTOs.Recipe;
using System.ComponentModel.DataAnnotations;

namespace GourmeyGalleryApp.Models.DTOs.Comments
{
    public class RatingDto
    {
        public int? Id { get; set; }
        [Range(1, 5, ErrorMessage = "RatingValue must be between 1 and 5.")]
        public int? RatingValue { get; set; }
        public string? UserId { get; set; }
        public ApplicationUserDto? User { get; set; }
        public int? RecipeId { get; set; }
        public RecipeDto? Recipe { get; set; }
    }
}
