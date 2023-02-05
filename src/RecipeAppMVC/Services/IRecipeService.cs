using RecipeAppDAL.Models;
using RecipeAppMVC.Models.Recipe;

namespace RecipeAppMVC.Services
{
    public interface IRecipeService
    {
        Task CreateRecipeAsync(RecipeDetailModel model);
        Task<Recipe?> UpdateRecipeAsync(RecipeDetailModel model);
    }
}
