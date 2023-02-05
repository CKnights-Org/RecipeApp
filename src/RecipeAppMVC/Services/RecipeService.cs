using Mapster;
using Microsoft.EntityFrameworkCore;
using RecipeAppDAL.Data;
using RecipeAppDAL.Models;
using RecipeAppMVC.Models.Recipe;

namespace RecipeAppMVC.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipeAppDBContext _db;

        public RecipeService(RecipeAppDBContext dBContext)
        {
            _db = dBContext;
        }

        public async Task CreateRecipeAsync(RecipeDetailModel model)
        {
            var recipe = new Recipe
            {
                Name = model.Name,
                Description = model.Description,
                IngredientRecipe = new(),
            };
            
            _db.Recipes.Add(recipe);

            foreach (var ingredient in model.Ingredients)
            {
                if (ingredient.Ingredient == null)
                {
                    continue;
                }

                recipe.IngredientRecipe
                    .Add(new IngredientRecipe
                    {
                        Recipe = recipe,
                        IngredientID = ingredient.Ingredient.Id,
                        Amount = ingredient.Ingredient.Amount,
                        TypeOfAmount = ingredient.Ingredient.TypeOfAmount,
                    });
            }

            await _db.SaveChangesAsync();
        }

        public async Task<Recipe?> UpdateRecipeAsync(RecipeDetailModel model)
        {
            var recipe =  await _db.Recipes
                .Where(a => a.Id == model.Id)
                .FirstOrDefaultAsync();

            if (recipe == null)
            {
                return null;
            }

            // var newRecipe = model.Adapt<Recipe>();
            var ingredientList = new List<IngredientRecipe>();

            foreach (var ingredient in model.Ingredients)
            {
                if (ingredient.Ingredient == null)
                {
                    continue;
                }

                ingredientList
                    .Add(new IngredientRecipe
                    {
                        Recipe = recipe,
                        IngredientID = ingredient.Ingredient.Id,
                        Amount = ingredient.Ingredient.Amount,
                        TypeOfAmount = ingredient.Ingredient.TypeOfAmount,
                    });
            }

            _db.RemoveRange(recipe.IngredientRecipe);
            recipe.IngredientRecipe = ingredientList;
            // _db.Entry(recipe).CurrentValues.SetValues(newRecipe);
            await _db.SaveChangesAsync();
            return recipe;
        }
    }
}
