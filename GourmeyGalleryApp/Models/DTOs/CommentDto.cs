using GourmeyGalleryApp.Models.DTOs;
using GourmeyGalleryApp.Models.Entities;

namespace GourmeyGalleryApp.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int RecipeId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? ApplicationUserId { get; set; }
        public ApplicationUserDto? User { get; set; }
        public RecipeDto? Recipe { get; set; }
    }
}
