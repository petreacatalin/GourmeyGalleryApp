using GourmeyGalleryApp.Models.Entities;
using GourmeyGalleryApp.Repositories.UserRepo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GourmetGallery.Infrastructure.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly GourmetGalleryContext _context;

        public UserRepository(GourmetGalleryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
