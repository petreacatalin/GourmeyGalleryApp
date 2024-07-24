using GourmeyGalleryApp.DTOs;
using GourmeyGalleryApp.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GourmeyGalleryApp.Interfaces
{
    public interface ICommentsService
    {
        Task<Comment> AddCommentAsync(CommentDto commentDto, string userId);
        Task<Comment> GetCommentAsync(int id);
        Task<IEnumerable<CommentDto>> GetCommentsForRecipeAsync(int recipeId);
    }
}
