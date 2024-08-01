using GourmeyGalleryApp.Models.DTOs.Comments;
using GourmeyGalleryApp.Models.Entities;
using System.Text.Json.Serialization;
using static GourmeyGalleryApp.Utils.RecipeEnums;

namespace GourmeyGalleryApp.Models.DTOs.Recipe
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int IngredientsTotalId { get; set; }
        public int InstructionsId { get; set; }
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
        public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();
        // public ICollection<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
        public IngredientsTotalDto IngredientsTotal { get; set; } // Updated DTO
        public InstructionsDto Instructions { get; set; } // Updated DTO
        public NutritionFactsDto? NutritionFacts { get; set; }
        public InformationTimeDto? InformationTime { get; set; }
    }


}
