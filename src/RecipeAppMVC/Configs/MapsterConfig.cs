using Mapster;
using RecipeAppDAL.Models;
using RecipeAppMVC.Models.Recipe;
using RecipeAppMVC.ViewModels.Recipe;
using System;
#pragma warning disable IDE0060
namespace RecipeAppMVC.Configs
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<IngredientRecipe, IngredientModel>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Ingredient.Name);

            TypeAdapterConfig<IEnumerable<Review>, RecipeReviewsSummaryModel>
                .NewConfig()
                .Map(dest => dest.Rating, src => src.Any() ? src.Sum(r => r.Rating) / src.Count() : 0)
                .Map(dest => dest.RatingCount, src => src.Count());

            TypeAdapterConfig.GlobalSettings.NewConfig<Recipe, DeleteViewModel>()
                .AfterMappingAsync((poco, dto) => {
                    dto.Ingredients = poco.IngredientRecipe;
                    return Task.CompletedTask;
                });
        }
    }
}
#pragma warning restore IDE0060
