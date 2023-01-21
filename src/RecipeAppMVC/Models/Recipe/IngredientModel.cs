namespace RecipeAppMVC.Models.Recipe
{
    public class IngredientModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int Amount { get; set; }

        public string? TypeOfAmount { get; set; }
    }
}
