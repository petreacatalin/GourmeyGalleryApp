using GourmeyGalleryApp.Interfaces;
using GourmeyGalleryApp.Models.Entities;


namespace GourmeyGalleryApp.Services.RecipeService
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe> _recipeRepository;

        public RecipeService(IRepository<Recipe> recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await _recipeRepository.GetAllAsync();
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _recipeRepository.GetByIdAsync(id);
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            await _recipeRepository.AddAsync(recipe);
            await _recipeRepository.SaveChangesAsync();  // Ensure SaveChanges is implemented in the Repository
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
