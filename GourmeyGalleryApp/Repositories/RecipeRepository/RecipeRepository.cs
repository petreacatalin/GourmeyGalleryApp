using GourmetGallery.Infrastructure;
using GourmeyGalleryApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GourmeyGalleryApp.Repositories.RecipeRepository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly GourmetGalleryContext _context;

        public RecipeRepository(GourmetGalleryContext context)
        {
            _context = context;
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            // Add the Recipe to the context
            await _context.Set<Recipe>().AddAsync(recipe);

            // Save changes to get the Recipe ID
            await _context.SaveChangesAsync();

        }

        public async Task<List<Recipe>> GetAllRecipesWithDetailsAsync()
        {
            return await _context.Recipes
                .Include(r => r.IngredientsTotal)
                    .ThenInclude(it => it.Ingredients)
                .Include(r => r.Instructions)
                    .ThenInclude(i => i.Steps)
                .Include(r => r.Comments)
                //.Include(r => r.Reviews)
                .ToListAsync();
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes
           .Include(r => r.IngredientsTotal)
               .ThenInclude(it => it.Ingredients)
           .Include(r => r.Instructions)
               .ThenInclude(i => i.Steps)
           .Include(r => r.Comments)
           //.Include(r => r.Reviews)
            .FirstOrDefaultAsync(r => r.Id == id);
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}


