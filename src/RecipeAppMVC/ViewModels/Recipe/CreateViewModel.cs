using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeAppDAL.Models;

namespace RecipeAppMVC.ViewModels.Recipe
{
    public class CreateViewModel
    {
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Description { get; set; } = "";
        [Required]
        public IList<IngredientRecipe> Ingredients { get; set; } = new List<IngredientRecipe>();
        
        // [NotMapped]
        public IngredientRecipe NewIngredient { get; set; } = null!;
        [NotMapped]
        public IEnumerable<SelectListItem> IngredientsSelection { get; set; } = null!;
    }
}