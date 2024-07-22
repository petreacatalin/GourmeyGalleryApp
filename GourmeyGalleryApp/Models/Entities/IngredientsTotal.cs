namespace GourmeyGalleryApp.Models.Entities
{
    public class IngredientsTotal
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
