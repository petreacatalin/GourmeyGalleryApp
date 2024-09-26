using System.ComponentModel.DataAnnotations.Schema;

namespace GourmeyGalleryApp.Models.Entities
{
    public class InformationTime
    {
        public int Id { get; set; }
        public int? PrepTime { get; set; }
        public int? CookTime { get; set; }
        public int? StandTime { get; set; }
        public int? TotalTime { get; set; }
        public int? Servings { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

    }
}
