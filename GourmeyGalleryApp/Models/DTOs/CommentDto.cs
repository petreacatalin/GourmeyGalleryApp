using GourmeyGalleryApp.Models.Entities;

namespace GourmeyGalleryApp.DTOs
{
    public class CommentDto
    {
        public string? Content { get; set; }
        public int RecipeId { get; set; }
        public string? ApplicationUserId { get; set; }
    }
}
