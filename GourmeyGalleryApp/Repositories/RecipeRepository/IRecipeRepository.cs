using GourmeyGalleryApp.Models.Entities;

namespace GourmeyGalleryApp.Repositories.RecipeRepository
{
    public interface IRecipeRepository
    {
        Task AddRecipeAsync(Recipe recipe);
        Task<List<Recipe>> GetAllRecipesWithDetailsAsync();
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task<List<Rating>> GetRatingsByRecipeId(int id);
        Task SaveChangesAsync();
        Task<List<Recipe>> GetRecipesByUserIdAsync(string userId);

    }

}
