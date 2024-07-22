using System.ComponentModel.DataAnnotations;

namespace GourmeyGalleryApp.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
        public string UserId { get; set; }
        public int RecipeId { get; set; }
        public ApplicationUser User { get; set; }
        public Recipe Recipe { get; set; }
    }
}
