using GourmeyGalleryApp.DTOs;
using GourmeyGalleryApp.Infrastructure;
using GourmeyGalleryApp.Interfaces;
using GourmeyGalleryApp.Models.DTOs;
using GourmeyGalleryApp.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GourmeyGalleryApp.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IRepository<ApplicationUser> _userRepository;

        public CommentsService(ICommentsRepository commentsRepository, IRepository<ApplicationUser> userRepository)
        {
            _commentsRepository = commentsRepository;
            _userRepository = userRepository;
        }

        public async Task<Comment> AddCommentAsync(CommentDto commentDto, string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            var comment = new Comment
            {
                Content = commentDto.Content,
                RecipeId = commentDto.RecipeId,
                ApplicationUserId = commentDto.ApplicationUserId,
                Timestamp = DateTime.UtcNow,
                User = user
            };

            await _commentsRepository.AddAsync(comment);
            await _commentsRepository.SaveChangesAsync();

            return comment;
        }

        public async Task<Comment> GetCommentAsync(int id)
        {
            return await _commentsRepository.GetFirstOrDefaultAsync(
                c => c.Id == id,
                include: query => query.Include(c => c.User)
            );
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsForRecipeAsync(int recipeId)
        {
            var comments = await _commentsRepository.GetCommentsForRecipeAsync(recipeId);
            return comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Content = c.Content,
                RecipeId = c.RecipeId,
                ApplicationUserId = c.ApplicationUserId,
                Timestamp = c.Timestamp,
                User = new ApplicationUserDto
                {
                    Id = c.User.Id,
                    FirstName = c.User.FirstName,
                    LastName = c.User.LastName,
                    ProfilePictureUrl = c.User.ProfilePictureUrl
                }
            });
        }
    }
}
