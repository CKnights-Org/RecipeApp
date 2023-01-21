using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeAppDAL.Models;
using RecipeAppMVC.Models.Recipe;

namespace RecipeAppMVC.ViewModels.Recipe
{
    public class CreateViewModel
    {
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Description { get; set; } = "";
        [Required]
        public IList<IngredientModel> Ingredients { get; set; } = new List<IngredientModel>();
        
        // [NotMapped]
        public IngredientModel? NewIngredient { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? IngredientsSelection { get; set; }
    }
}