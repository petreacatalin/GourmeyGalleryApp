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
    public async Task<ActionResult<RecipeDto>> GetRecipe(int id)
    {
        var recipe = await _recipeService.GetRecipeByIdAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }
        var recipeDto = _mapper.Map<RecipeDto>(recipe);
        return Ok(recipeDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecipe(RecipeDto recipeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Map the DTO to the Recipe entity
        var recipe = _mapper.Map<Recipe>(recipeDto);
            var userId = User.FindFirstValue("nameId");

        try
        {
            // Extract user ID from the token

            // Check if user ID is null or empty
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token.");
            }

            // Set the ApplicationUserId in the Recipe entity
            recipe.ApplicationUserId = userId;

            // Add the recipe using the RecipeService
            await _recipeService.AddRecipeAsync(recipe);

            // Map the result back to a DTO
            var recipeDtoResult = _mapper.Map<RecipeDto>(recipe);

            // Return a CreatedAtAction response
            return CreatedAtAction(nameof(GetRecipe),
            new {
                id = recipe.Id , 
                applicationUserId= recipe.ApplicationUserId}, recipeDtoResult);
                }
        catch (Exception ex)
        {
            // Return a 500 status code with the error message
            return StatusCode(500, $"Internal server error: {ex.Message}");
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
