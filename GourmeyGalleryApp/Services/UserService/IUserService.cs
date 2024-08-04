using GourmeyGalleryApp.Models.DTOs.Recipe;
using GourmeyGalleryApp.Models.Entities;

namespace GourmeyGalleryApp.Services.UserService.UserService
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(ApplicationUser user);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<IEnumerable<ApplicationUser>> GetFriendsAsync(string userId);
        Task AddFriendAsync(string userId, string friendId);
        Task AcceptFriendAsync(string userId, string friendId);
        Task AddToFavoritesAsync(string userId, int recipeId);
        Task RemoveFromFavoritesAsync(string userId, int recipeId);
        Task<IEnumerable<RecipeDto>> GetFavoriteRecipesAsync(string userId);
    }

}
