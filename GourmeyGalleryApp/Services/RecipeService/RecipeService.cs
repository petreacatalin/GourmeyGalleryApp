using AutoMapper;
using GourmeyGalleryApp.Interfaces;
using GourmeyGalleryApp.Models.Entities;
using GourmeyGalleryApp.Repositories.RecipeRepository;
using Microsoft.EntityFrameworkCore;


namespace GourmeyGalleryApp.Services.RecipeService
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe> _recipeRepository;
        private readonly IRecipeRepository _recipeCustomRepository;
        private readonly IMapper _mapper;
        public RecipeService(IRepository<Recipe> recipeRepository, IRecipeRepository recipeCustomRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _recipeCustomRepository = recipeCustomRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            var recipes = await _recipeCustomRepository.GetAllRecipesWithDetailsAsync();
            return _mapper.Map<List<Recipe>>(recipes);
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            var recipe = await _recipeCustomRepository.GetRecipeByIdAsync(id);
            if (recipe == null)
            {
                return null;
            }
            return _mapper.Map<Recipe>(recipe);
        }

        public async Task<List<Rating>> GetRatingsByRecipeId(int id)
        {
            var recipe = await _recipeCustomRepository.GetRatingsByRecipeId(id);
            if (recipe == null)
            {
                return null;
            }
            return _mapper.Map<List<Rating>>(recipe);
        }
        public async Task<IEnumerable<Recipe>> GetRecipesByUserIdAsync(string userId)
        {
            return await _recipeCustomRepository.GetRecipesByUserIdAsync(userId);
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            await _recipeCustomRepository.AddRecipeAsync(recipe);
            await _recipeCustomRepository.SaveChangesAsync();

            // Set the InstructionsId and IngredientsTotalId
            if (recipe.Instructions != null)
            {
                recipe.Instructions.RecipeId = recipe.Id;
                recipe.InstructionsId = recipe.Instructions.Id;
            }
            if (recipe.IngredientsTotal != null)
            {
                recipe.IngredientsTotal.RecipeId = recipe.Id;
                recipe.IngredientsTotalId = recipe.IngredientsTotal.Id;
            }
            if (recipe.InformationTime != null)
            {
                recipe.InformationTime.RecipeId = recipe.Id;
            }
            if (recipe.NutritionFacts != null)
            {
                recipe.NutritionFacts.RecipeId = recipe.Id;
            }

            await _recipeCustomRepository.SaveChangesAsync();
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            _recipeRepository.Update(recipe);
            await _recipeRepository.SaveChangesAsync();  // Ensure SaveChanges is implemented in the Repository
        }

        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await _recipeRepository.GetByIdAsync(id);
            if (recipe != null)
            {
                _recipeRepository.Delete(recipe);
                await _recipeRepository.SaveChangesAsync();  // Ensure SaveChanges is implemented in the Repository
            }
        }
    }
}
