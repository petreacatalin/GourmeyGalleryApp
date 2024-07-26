using System.Text.Json.Serialization;

namespace GourmeyGalleryApp.Models.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        // Foreign keys
        public string ApplicationUserId { get; set; }
        public int RecipeId { get; set; }
        // Navigation properties
        public ApplicationUser User { get; set; }
        [JsonIgnore]
        public Recipe Recipe { get; set; }
    }
}
