using Mapster;
using Microsoft.AspNetCore.Mvc;
using PizzaShopDAL.Data;
using RecipeAppDAL.Models;
using RecipeAppMVC.Models.Recipe;

namespace RecipeAppMVC.Controllers
{
    public class RecipeController : Controller
    {
        private readonly RecipeAppDBContext _db;

        public RecipeController(RecipeAppDBContext dBContext)
        {
            _db = dBContext;
        }

        public IActionResult Index()
        {
            var recipeList = _db.Recipes
                .Select(a => ConvertRecipeToRecipeSummaryModel(a))
                .ToList();

            return View(recipeList);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var recipe = await _db.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(ConvertRecipeToRecipeDetail(recipe));
        }

        private static RecipeSummaryModel ConvertRecipeToRecipeSummaryModel(Recipe recipe)
        {
            var result = recipe.Adapt<RecipeSummaryModel>();
            result.ReviewsSummary = recipe.Reviews.Adapt<RecipeReviewsSummaryModel>();
            return result;
        }

        private static RecipeDetailModel ConvertRecipeToRecipeDetail(Recipe recipe)
        {
            var result = recipe.Adapt<RecipeDetailModel>();
            result.ReviewsSummary = recipe.Reviews.Adapt<RecipeReviewsSummaryModel>();
            result.Ingredients = recipe.IngredientRecipe.Adapt<List<IngredientModel>>();
            result.Reviews = recipe.Reviews.Adapt<List<ReviewModel>>();
            return result;
        }
    }
}
