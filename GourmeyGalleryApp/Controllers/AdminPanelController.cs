using GourmeyGalleryApp.Services.RecipeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GourmeyGalleryApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminPanelController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public AdminPanelController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("pending-recipes")]
        public async Task<IActionResult> GetPendingRecipes()
        {
            var recipes = await _recipeService.GetPendingRecipesAsync();
            return Ok(recipes);
        }

        [HttpPost("approve-recipe/{id}")]
        public async Task<IActionResult> ApproveRecipe(int id)
        {
            await _recipeService.ApproveRecipeAsync(id);
            return Ok();
        }

        [HttpPost("reject-recipe/{id}")]
        public async Task<IActionResult> RejectRecipe(int id)
        {
            await _recipeService.RejectRecipeAsync(id);
            return Ok();
        }
    }

}
