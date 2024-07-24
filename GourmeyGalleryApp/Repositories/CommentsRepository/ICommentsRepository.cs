using GourmeyGalleryApp.Interfaces;
using GourmeyGalleryApp.Models.Entities;

namespace GourmeyGalleryApp.Infrastructure
{
        public interface ICommentsRepository : IRepository<Comment>
        {
            Task<IEnumerable<Comment>> GetCommentsForRecipeAsync(int recipeId);
        }
}
