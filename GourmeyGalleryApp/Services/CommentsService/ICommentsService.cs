using GourmeyGalleryApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GourmeyGalleryApp.Interfaces
{
    public interface ICommentsService
    {
        Task<CommentDto> AddCommentAsync(CommentDto commentDto);
        Task<CommentDto> GetCommentAsync(int id);
        Task<IEnumerable<CommentDto>> GetCommentsForRecipeAsync(int recipeId);
    }
}
