namespace GourmeyGalleryApp.Models.Entities
{
    public class InformationTimeDto
    {
        public int Id { get; set; }
        public int? PrepTime { get; set; }
        public int? CookTime { get; set; }
        public int? StandTime { get; set; }
        public int? TotalTime { get; set; }
        public int? Servings { get; set; }
        public int RecipeId { get; set; }

    }
}
