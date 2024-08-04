namespace GourmeyGalleryApp.Models.Entities
{
    public class UserFavoriteRecipe
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
