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

            return View(await ConvertRecipeToRecipeDetail(recipe, _db));
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
            {
                return View(model);
            }

            var recipe = new RecipeDetailModel()
            {
                Name = model.Name,
                Description = model.Description,
                Ingredients = model.Ingredients,
            };

            await _recipeService.CreateRecipeAsync(recipe);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _db.Recipes.FirstOrDefaultAsync(x => x.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(await ConvertRecipeToRecipeDetail(recipe, _db));
        }

        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RecipeDetailModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (viewModel.Id == 0)
            {
                return BadRequest();
            }

            var recipe = await _recipeService.UpdateRecipeAsync(viewModel);

            if (recipe == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Edit), new { id = recipe.Id });
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _db.Recipes.FirstOrDefaultAsync(x => x.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(await ConvertRecipeToRecipeDetail(recipe, _db));
        }

        [HttpPost, Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, RecipeDetailModel viewModel)
        {
            if (id == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var recipe = await _db.Recipes.FirstOrDefaultAsync(x => x.Id == id);
            
            if (recipe == null)
            {
                return NotFound();
            }

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

        private static async Task<RecipeDetailModel> ConvertRecipeToRecipeDetail(Recipe recipe, RecipeAppDBContext db)
        {
            var result = recipe.Adapt<RecipeDetailModel>();
            result.ReviewsSummary = recipe.Reviews.Adapt<RecipeReviewsSummaryModel>();
            result.Ingredients = recipe.IngredientRecipe.Adapt<List<IngredientViewModel>>();
            result.Reviews = recipe.Reviews.Adapt<List<ReviewModel>>();

            var ingredientsAfterAdapt = (await db.Ingredients.AsNoTracking().ToListAsync()).Adapt<List<IngredientModel>>();

            foreach (var ingredient in result.Ingredients)
            {
                ingredient.IngredientsSelection = ingredientsAfterAdapt.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }); ;
            }

            return result;
        }
    }
}
