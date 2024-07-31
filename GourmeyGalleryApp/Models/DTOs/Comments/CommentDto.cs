using GourmeyGalleryApp.Models.DTOs.ApplicationUser;
using GourmeyGalleryApp.Models.DTOs.Recipe;
using System.Text.Json.Serialization;

namespace GourmeyGalleryApp.Models.DTOs.Comments
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
        public bool? IsReply { get; set; }
        public int? ParentCommentId { get; set; }
        [JsonIgnore]
        public CommentDto? ParentComment { get; set; }
        public ICollection<CommentDto>? Replies { get; set; } = new List<CommentDto>();
    }
}
