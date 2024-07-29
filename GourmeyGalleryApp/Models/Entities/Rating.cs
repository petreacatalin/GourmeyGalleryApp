using System.ComponentModel.DataAnnotations;

namespace GourmeyGalleryApp.Models.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        [Range(1, 5, ErrorMessage = "RatingValue must be between 1 and 5.")]
        public int? RatingValue { get; set; }
        public string? UserId { get; set; }
        public int? RecipeId { get; set; }
        public ApplicationUser? User { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
