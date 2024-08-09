using GourmeyGalleryApp.Models.DTOs.ApplicationUser;
using GourmeyGalleryApp.Models.DTOs.Recipe;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GourmeyGalleryApp.Models.DTOs.Comments
{
    public class RatingDto
    {
        public int? Id { get; set; }
        public int? RatingValue { get; set; }
        public string? UserId { get; set; }
        [JsonIgnore]
        public ApplicationUserDto? User { get; set; }
        public int? RecipeId { get; set; }
        [JsonIgnore]
        public RecipeDto? Recipe { get; set; }
    }
}
