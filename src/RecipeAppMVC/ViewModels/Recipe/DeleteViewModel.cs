using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeAppDAL.Models;

namespace RecipeAppMVC.ViewModels.Recipe
{
    public class DeleteViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IList<IngredientRecipe> Ingredients { get; set; } = null!;
    }
}