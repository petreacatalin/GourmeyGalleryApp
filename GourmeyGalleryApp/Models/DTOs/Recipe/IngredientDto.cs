﻿namespace GourmeyGalleryApp.Models.DTOs.Recipe
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public int IngredientsTotalId { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
    }

}