using System.Text.Json.Serialization;

namespace GourmeyGalleryApp.Models.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string ApplicationUserId { get; set; }
        public int RecipeId { get; set; }
        public int? RatingId { get; set; }
        [JsonIgnore]
        public Rating? Rating { get; set; }
        public ApplicationUser User { get; set; }
        [JsonIgnore]
        public Recipe? Recipe { get; set; }
        public bool IsReply { get; set; }
        public int? ParentCommentId { get; set; }
        [JsonIgnore]
        public Comment? ParentComment { get; set; }
        public ICollection<Comment>? Replies { get; set; } = new List<Comment>();
    }
}
