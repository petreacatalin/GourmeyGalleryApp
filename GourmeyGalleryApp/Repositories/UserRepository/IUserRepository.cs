using GourmeyGalleryApp.Interfaces;
using GourmeyGalleryApp.Models.DTOs.Recipe;
using GourmeyGalleryApp.Models.Entities;

namespace GourmeyGalleryApp.Repositories.UserRepo
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task AddToFavoritesAsync(string userId, int recipeId);
        Task RemoveFromFavoritesAsync(string userId, int recipeId);
        Task<IEnumerable<RecipeDto>> GetFavoriteRecipesAsync(string userId);
    }
}
