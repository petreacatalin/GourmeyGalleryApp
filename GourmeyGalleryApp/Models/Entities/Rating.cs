namespace GourmeyGalleryApp.Models.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string UserId { get; set; }
        public int RecipeId { get; set; }
        public ApplicationUser User { get; set; }
        public Recipe Recipe { get; set; }
    }
}
