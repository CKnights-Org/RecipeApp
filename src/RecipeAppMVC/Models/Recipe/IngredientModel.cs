namespace RecipeAppMVC.Models.Recipe
{
    public class IngredientModel
    {
        public string Name { get; set; }= null!;

        public int Amount { get; set; }

        public string TypeOfAmount { get; set; }= null!;
    }
}
