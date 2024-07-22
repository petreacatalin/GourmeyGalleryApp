using static GourmeyGalleryApp.Utils.Enums;
using System.Text.Json.Serialization;

namespace GourmeyGalleryApp.Models.Entities
{

    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public string? Tags { get; set; }
        public string? ImageUrl { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MealType? MealType { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Cuisine? Cuisine { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DietaryRestrictions? DietaryRestrictions { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CookingMethod? CookingMethod { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MainIngredient? MainIngredient { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Occasion? Occasion { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DifficultyLevel? DifficultyLevel { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OtherCategories? OtherCategories { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }

}
