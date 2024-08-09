using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GourmeyGalleryApp.Models.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        [Range(1, 5, ErrorMessage = "RatingValue must be between 1 and 5.")]
        public int? RatingValue { get; set; }
        public string? UserId { get; set; }
        public int? RecipeId { get; set; }
        [JsonIgnore]
        public ApplicationUser? User { get; set; }
        [JsonIgnore]
        public Recipe? Recipe { get; set; }
    }
}
