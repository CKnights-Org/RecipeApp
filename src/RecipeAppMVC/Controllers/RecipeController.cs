using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Create()
        {
            return View(new CreateViewModel
            {
                IngredientsSelection = (await _db.Ingredients.AsNoTracking().ToListAsync()).Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
            });
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConfirm(CreateViewModel viewModel)
        {
            if (viewModel.NewIngredient != null)
            {
                var output = new CreateViewModel
                {
                    Ingredients = viewModel.Ingredients,
                    Name = viewModel.Name,
                    Description = viewModel.Description
                };
                viewModel.NewIngredient.Ingredient = await _db.Ingredients.FirstOrDefaultAsync(x => x.Id == viewModel.NewIngredient.IngredientID);
                output.Ingredients.Add(viewModel.NewIngredient);
                
                //i dont like it aswell, but for now it works
                output.IngredientsSelection = (await _db.Ingredients.AsNoTracking().ToListAsync()).Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                });
                return View(output);
            }
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

            var vm = await recipe.BuildAdapter().AdaptToTypeAsync<DeleteViewModel>();
            return View(vm);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id, DeleteViewModel vm)
        {
            if (id == 0)
                return BadRequest();
            var recipe = await _db.Recipes.FirstOrDefaultAsync(x => x.Id == id);
            if (recipe == null)
                return NotFound();
            // var reviews = _db.Reviews.Where(x => x.Recipe == recipe);
            // _db.Reviews.RemoveRange(reviews);
            var ingredientRecipes = recipe.IngredientRecipe;
            _db.RemoveRange(ingredientRecipes);
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
