using GourmeyGalleryApp.Models.Entities;

namespace GourmeyGalleryApp.Services.CategoryService
{
    public interface ICategoryService
    { 
            Task<List<Category>> GetAllCategoriesAsync();
            Task<Category?> GetCategoryByIdAsync(int id);
            Task AddCategoryAsync(Category category);
            Task UpdateCategoryAsync(Category category);
            Task DeleteCategoryAsync(int id);

    }
}
