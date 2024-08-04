using GourmetGallery;
using GourmeyGalleryApp.Interfaces;
using GourmeyGalleryApp.Models;
using GourmeyGalleryApp.Models.DTOs;
using GourmeyGalleryApp.Models.DTOs.Recipe;
using GourmeyGalleryApp.Models.Entities;
using GourmeyGalleryApp.Repositories.UserRepo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GourmeyGalleryApp.Services.UserService.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetFriendsAsync(string userId)
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(u => u.Id == userId, include: u => u.Include(u => u.FriendsAdded));
            return user?.FriendsAdded.Select(f => f.FriendUser);
        }

        public async Task AddFriendAsync(string userId, string friendId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var friend = await _userRepository.GetByIdAsync(friendId);

            if (user != null && friend != null)
            {
                user.FriendsAdded.Add(new Friend { UserId = userId, FriendId = friendId });
                await _userRepository.SaveChangesAsync();
            }
        }

        public async Task AcceptFriendAsync(string userId, string friendId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            var friend = await _userRepository.GetByIdAsync(friendId);

            if (user != null && friend != null)
            {
                user.FriendsAccepted.Add(new Friend { UserId = friendId, FriendId = userId });
                await _userRepository.SaveChangesAsync();
            }
        }

        public async Task AddToFavoritesAsync(string userId, int recipeId)
        {
            await _userRepository.AddToFavoritesAsync(userId, recipeId);
        }

        public async Task RemoveFromFavoritesAsync(string userId, int recipeId)
        {
            await _userRepository.RemoveFromFavoritesAsync(userId, recipeId);
        }

        public async Task<IEnumerable<RecipeDto>> GetFavoriteRecipesAsync(string userId)
        {
            return await _userRepository.GetFavoriteRecipesAsync(userId);
        }

    }
}
