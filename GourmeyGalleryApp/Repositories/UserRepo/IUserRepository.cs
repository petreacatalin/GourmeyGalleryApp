using GourmeyGalleryApp.Interfaces;
using GourmeyGalleryApp.Models.Entities;

namespace GourmeyGalleryApp.Repositories.UserRepo
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    }
}
