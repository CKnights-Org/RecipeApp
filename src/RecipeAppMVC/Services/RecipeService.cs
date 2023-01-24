using Mapster;
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
                recipe.IngredientRecipe
                    .Add(new IngredientRecipe
                    {
                        Recipe = recipe,
                        IngredientID = ingredient.Id,
                        Amount = ingredient.Amount,
                        TypeOfAmount = ingredient.TypeOfAmount
                    });
            }

            await _db.SaveChangesAsync();
        }
    }
}
