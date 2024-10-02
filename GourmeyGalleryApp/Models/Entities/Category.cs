namespace GourmeyGalleryApp.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string Slug { get; set; } 
        // Self-referencing relationship to represent parent-child structure
        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public ICollection<Category>? Subcategories { get; set; } = new List<Category>();

        public ICollection<RecipeCategory>? RecipeCategories { get; set; }

    }

}