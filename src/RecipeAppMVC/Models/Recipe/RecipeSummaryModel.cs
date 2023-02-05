namespace RecipeAppMVC.Models.Recipe
{
    public class RecipeSummaryModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public RecipeReviewsSummaryModel? ReviewsSummary { get; set; }
    }
}
