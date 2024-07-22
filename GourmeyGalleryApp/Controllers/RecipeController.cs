using AutoMapper;
using GourmeyGalleryApp.DTOs;
using GourmeyGalleryApp.Models.Entities;
using GourmeyGalleryApp.Services;
using GourmeyGalleryApp.Services.RecipeService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

[ApiController]
[Route("api/recipes")]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    public RecipeController(IRecipeService recipeService, IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _recipeService = recipeService;
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecipeDto>>> GetRecipes()
    {
        var recipes = await _recipeService.GetAllRecipesAsync();
        var recipesDto = _mapper.Map<IEnumerable<RecipeDto>>(recipes);
        return Ok(recipesDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipe(int id)
    {
        var recipe = await _recipeService.GetRecipeByIdAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }
        var recipeMapped = _mapper.Map<RecipeDto>(recipe);
        return Ok(recipeMapped);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecipe(RecipeDto recipeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Map the DTO to the Recipe entity
            var recipe = _mapper.Map<Recipe>(recipeDto);

            // Extract user ID from the token
            var userId = User.FindFirstValue("nameId");

            // Check if user ID is null or empty
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            // Set the ApplicationUserId in the Recipe entity
            recipe.ApplicationUserId = userId;

            // Ensure Instructions and IngredientsTotal entities are created if not null
            if (recipeDto.Instructions != null)
            {
                recipe.Instructions = new Instructions
                {
                    Steps = recipeDto.Instructions.Steps.Select(stepDto => _mapper.Map<Step>(stepDto)).ToList()
                };
            }
            if (recipeDto.IngredientsTotal != null)
            {
                recipe.IngredientsTotal = new IngredientsTotal
                {
                    Ingredients = recipeDto.IngredientsTotal.Ingredients.Select(ingredientDto => _mapper.Map<Ingredient>(ingredientDto)).ToList()
                };
            }

            // Add the recipe using the RecipeService
            await _recipeService.AddRecipeAsync(recipe);

            // Fetch the updated recipe to get the correct IDs
            var savedRecipe = await _recipeService.GetRecipeByIdAsync(recipe.Id);

            // Map the result back to a DTO
            var recipeDtoResult = _mapper.Map<RecipeDto>(savedRecipe);

            // Return a CreatedAtAction response
            return CreatedAtAction(nameof(GetRecipe),
                new { id = savedRecipe.Id, applicationUserId = savedRecipe.ApplicationUserId }, recipeDtoResult);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error.");
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRecipe(int id, [FromBody] RecipeDto recipeDto)
    {
        if (id != recipeDto.Id)
        {
            return BadRequest();
        }

        var recipe = await _recipeService.GetRecipeByIdAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }

        _mapper.Map(recipeDto, recipe);

        try
        {
            await _recipeService.UpdateRecipeAsync(recipe);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe(int id)
    {
        try
        {
            await _recipeService.DeleteRecipeAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
