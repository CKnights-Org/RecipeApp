using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecipeAppDAL.Data;
using RecipeAppDAL.Models;
using RecipeAppMVC.Models.Recipe;
using RecipeAppMVC.Services;
using RecipeAppMVC.ViewModels.Ingredient;
using RecipeAppMVC.ViewModels.Recipe;

namespace RecipeAppMVC.Controllers
{
    public class RecipeController : Controller
    {
        private readonly RecipeAppDBContext _db;
        private readonly IRecipeService _recipeService;

        public RecipeController(RecipeAppDBContext dBContext, IRecipeService recipeService)
        {
            _db = dBContext;
            _recipeService = recipeService;
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
        public async Task<IActionResult> RecipeSettings()
        {
            var viewModel = new RecipeSettingsViewModel();
            return View(viewModel);
        }

        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecipeSettings(RecipeSettingsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Aggreement != true)
            {
                ModelState.AddModelError("Aggreement", "Agreement needs to be checked");
                return View(model);
            }

            return RedirectToAction(nameof(Create), new { ingredients = model.NumberOfIngredients });
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Create(int ingredients = 5)
        {
            ingredients = ingredients > 0 ? ingredients : 5;

            var ingredientsAfterAdapt = (await _db.Ingredients.AsNoTracking().ToListAsync()).Adapt<List<IngredientModel>>();

            var emptyIngredient = new IngredientViewModel
            {
                IngredientsSelection = ingredientsAfterAdapt.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
            };

            var viewmodel = new CreateViewModel
            {
                Ingredients = new List<IngredientViewModel>()
            };

            for (int i = 0; i < ingredients; i++)
            {
                viewmodel.Ingredients.Add(emptyIngredient);
            }

            return View(viewmodel);
        }

        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var recipe = new RecipeDetailModel()
            {
                Name = model.Name,
                Description = model.Description,
                Ingredients = model.Ingredients.Adapt<List<IngredientModel>>()
            };

            await _recipeService.CreateRecipeAsync(recipe);

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
