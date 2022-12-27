using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeAppDAL.Models;

namespace RecipeAppMVC.ViewModels.Recipe
{
    public class CreateViewModel
    {
        [BindProperty, Required]
        public string Name { get; set; } = "";
        [BindProperty, Required]
        public string Description { get; set; } = "";
        [BindProperty, Required]
        public IList<IngredientRecipe> Ingredients { get; set; } = new List<IngredientRecipe>();
        [NotMapped]
        public IngredientRecipe? NewIngredient { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> IngredientsSelection { get; set; } = null!;
    }
}