namespace RecipeAppMVC.Models.Recipe
{
    public class RecipeSummaryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }= null!;

        public string Description { get; set; }= null!;

        public RecipeReviewsSummaryModel ReviewsSummary { get; set; }= null!;
    }
}
