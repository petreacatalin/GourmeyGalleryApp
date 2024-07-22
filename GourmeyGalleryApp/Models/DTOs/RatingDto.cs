namespace GourmeyGalleryApp.DTOs
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; } // Include user's name for display purposes
        public int RecipeId { get; set; }
    }
}
