using System.ComponentModel.DataAnnotations;

namespace RecipeAppMVC.Models.Recipe
{
    public class IngredientModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        
        [Required]
        public int Amount { get; set; }
        [Required]
        public string? TypeOfAmount { get; set; }
    }
}
