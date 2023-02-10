using RecipeAppMVC.ViewModels.Ingredient;

namespace RecipeAppMVC.Models.Recipe
{
    public class RecipeDetailModel : RecipeSummaryModel
    {
        public IList<IngredientViewModel> Ingredients { get; set; } = new List<IngredientViewModel>();

        public List<ReviewModel>? Reviews { get; set; }
    }
}
