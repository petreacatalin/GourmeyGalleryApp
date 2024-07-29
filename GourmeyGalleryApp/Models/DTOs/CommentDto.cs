using GourmeyGalleryApp.Models.DTOs;
using GourmeyGalleryApp.Models.Entities;
using System.Text.Json.Serialization;

namespace GourmeyGalleryApp.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int RecipeId { get; set; }
        public int? RatingId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? ApplicationUserId { get; set; }
        
        public RatingDto? Rating { get; set; }
        public ApplicationUserDto? User { get; set; }
        [JsonIgnore]
        public RecipeDto? Recipe { get; set; }
    }
}
