using GourmetGallery.Infrastructure;
using GourmeyGalleryApp.DTOs;
using GourmeyGalleryApp.Interfaces;
using GourmeyGalleryApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly IRepository<Comment> _commentRepository;
    private readonly IRepository<ApplicationUser> _userRepository;

    public CommentsController(IRepository<Comment> commentRepository, IRepository<ApplicationUser> userRepository)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> PostComment([FromBody] CommentDto commentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userId = User.FindFirstValue("nameId");
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return NotFound("User not found.");
        }

        var comment = new Comment
        {
            Content = commentDto.Content,
            RecipeId = commentDto.RecipeId,
            ApplicationUserId = commentDto.ApplicationUserId,
            Timestamp = DateTime.UtcNow,
            User = user
        };

        await _commentRepository.AddAsync(comment);
        await _commentRepository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Comment>> GetComment(int id)
    {
        var comment = await _commentRepository.GetFirstOrDefaultAsync(
            c => c.Id == id,
            include: query => query.Include(c => c.User)
        );

        if (comment == null)
        {
            return NotFound();
        }

        return comment;
    }

    [HttpGet("recipe/{recipeId}")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsForRecipe(int recipeId)
    {
        var comments = await _commentRepository.GetWhereAsync(
            c => c.RecipeId == recipeId,
            include: query => query.Include(c => c.User)
        );

        return Ok(comments.OrderByDescending(c => c.Timestamp).ToList());
    }
}
