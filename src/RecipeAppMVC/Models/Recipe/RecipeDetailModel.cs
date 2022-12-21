namespace RecipeAppMVC.Models.Recipe
{
    public class RecipeDetailModel : RecipeSummaryModel
    {
        public List<IngredientModel> Ingredients { get; set; }

        public List<ReviewModel> Reviews { get; set; }
    }
}
