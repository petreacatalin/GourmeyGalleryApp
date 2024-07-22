using GourmeyGalleryApp.DTOs;
using GourmeyGalleryApp.Models.Entities;

namespace GourmeyGalleryApp.Repositories.RecipeRepository
{
    public interface IRecipeRepository
    {
        Task AddRecipeAsync(Recipe recipe);
        Task<List<Recipe>> GetAllRecipesWithDetailsAsync();
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task SaveChangesAsync();
    }

}
