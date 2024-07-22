namespace GourmeyGalleryApp.Models.Entities
{
    public class MealPlan
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
