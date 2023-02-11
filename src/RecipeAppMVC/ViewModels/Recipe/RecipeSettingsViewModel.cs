using System.ComponentModel.DataAnnotations;

namespace RecipeAppMVC.ViewModels.Recipe
{
    public class RecipeSettingsViewModel
    {
        [Range(3, 20)]
        public int NumberOfIngredients { get; set; } = 5;
        // TODO
        public string Visibility { get; set; } = "Public / Private";

        public bool Aggreement { get; set; }
    }
}
