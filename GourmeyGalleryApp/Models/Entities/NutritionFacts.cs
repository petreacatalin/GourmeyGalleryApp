namespace GourmeyGalleryApp.Models.Entities
{
    public class NutritionFacts
    {
        public int Id { get; set; }
        public int Calories { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set; }
        public int Protein { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

    }
}
