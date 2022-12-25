using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShopDAL.Data;
using RecipeAppDAL.Models;
using RecipeAppMVC.Models.Recipe;
using RecipeAppMVC.ViewModels.Recipe;

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

        [HttpGet, Authorize]
        public IActionResult Create() => View(new CreateViewModel());

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConfirm(CreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var recipe = await _db.Recipes.FirstOrDefaultAsync(x => x.Id == id);
            if (recipe == null)
                return NotFound();

            return View(recipe.Adapt<EditViewModel>());
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirm(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            return RedirectToAction(nameof(Index));
        }
        
        [HttpGet, Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var recipe = await _db.Recipes.FirstOrDefaultAsync(x => x.Id == id);

            if (recipe == null)
                return NotFound();


            return View(recipe.Adapt<DeleteViewModel>());
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            if (id == 0)
                return BadRequest();
            var recipe = await _db.Recipes.FirstOrDefaultAsync(x => x.Id == id);
            if (recipe == null)
                return NotFound();
            _db.Recipes.Remove(recipe);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
