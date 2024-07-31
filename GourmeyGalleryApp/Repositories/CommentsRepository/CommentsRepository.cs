using GourmetGallery.Infrastructure;
using GourmetGallery.Infrastructure.Repositories;
using GourmeyGalleryApp.Interfaces;
using GourmeyGalleryApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GourmeyGalleryApp.Infrastructure
{
    public class CommentsRepository : Repository<Comment>, ICommentsRepository
    {
        private readonly GourmetGalleryContext _context;

        public CommentsRepository(GourmetGalleryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetCommentsForRecipeAsync(int recipeId)
        {
            return await _context.Comments
                .Where(c => c.RecipeId == recipeId)
                .Include(c => c.Rating)
                .Include(c => c.User)
                .Include(c=> c.Replies)
                .OrderByDescending(c => c.Timestamp)
                .ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetRepliesAsync(int parentId)
        {
           return await _context.Comments.Where(c => c.ParentCommentId == parentId).ToListAsync();
        }
    }
}
