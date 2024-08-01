namespace GourmeyGalleryApp.Models.Entities
{
    public class Instructions
    {
        public int Id { get; set; }
        public List<Step> Steps { get; set; } = new List<Step>();
        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
