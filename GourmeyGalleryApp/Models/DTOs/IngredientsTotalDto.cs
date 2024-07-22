namespace GourmeyGalleryApp.Models.DTOs
{
    public class IngredientsTotalDto
    {
        public int Id { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
        public int RecipeId { get; set; }
    }
}
