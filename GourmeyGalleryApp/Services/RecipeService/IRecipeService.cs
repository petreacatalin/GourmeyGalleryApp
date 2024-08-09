using GourmeyGalleryApp.Models.Entities;

namespace GourmeyGalleryApp.Services.RecipeService
{
    public interface IRecipeService
    {
        Task<IEnumerable<Recipe>> GetAllRecipesAsync();
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task<List<Rating>> GetRatingsByRecipeId(int id);
        Task AddRecipeAsync(Recipe recipe);
        Task UpdateRecipeAsync(Recipe recipe);
        Task DeleteRecipeAsync(int id);
    }
}
