using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipeAppDAL.Models;
using RecipeAppMVC.Models.Recipe;
using RecipeAppMVC.ViewModels.Ingredient;

namespace RecipeAppMVC.ViewModels.Recipe
{
    public class CreateViewModel
    {
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Description { get; set; } = "";
        [Required]
        public IList<IngredientViewModel> Ingredients { get; set; } = new List<IngredientViewModel>();
    }
}