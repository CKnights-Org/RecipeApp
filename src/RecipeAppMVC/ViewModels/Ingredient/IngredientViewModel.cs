using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeAppMVC.Models.Recipe;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeAppMVC.ViewModels.Ingredient
{
    public class IngredientViewModel
    {
        public IngredientModel? Ingredient { get; set; }

        public IEnumerable<SelectListItem>? IngredientsSelection { get; set; }

        /// <summary>
        /// Helper property for keeping track of how many ingredients there are on web.
        /// </summary>
        public int? ListingIndex { get; set; }
    }
}
