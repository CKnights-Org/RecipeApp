using Mapster;
using RecipeAppDAL.Models;
using RecipeAppMVC.Models.Recipe;
using RecipeAppMVC.ViewModels.Ingredient;
using RecipeAppMVC.ViewModels.Recipe;
using System;
#pragma warning disable IDE0060
namespace RecipeAppMVC.Configs
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<Ingredient, IngredientModel>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Id, src => src.Id);

            TypeAdapterConfig<IngredientRecipe, IngredientModel>    
                .NewConfig()
                .TwoWays()
                .Map(dest => dest.Id, src => src.IngredientID)
                .Map(dest => dest.Name, src => src.Ingredient.Name)
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.TypeOfAmount, src => src.TypeOfAmount);

            TypeAdapterConfig<IngredientRecipe, IngredientViewModel>
                .NewConfig()
                .TwoWays()
                .Map(dest => dest.Ingredient.Id, src => src.IngredientID)
                .Map(dest => dest.Ingredient.Name, src => src.Ingredient.Name)
                .Map(dest => dest.Ingredient.Amount, src => src.Amount)
                .Map(dest => dest.Ingredient.TypeOfAmount, src => src.TypeOfAmount);

            TypeAdapterConfig<IngredientViewModel, IngredientModel>
                .NewConfig()
                .TwoWays()
                .Map(dest => dest.Id, src => src.Ingredient.Id)
                .Map(dest => dest.Name, src => src.Ingredient.Name)
                .Map(dest => dest.Amount, src => src.Ingredient.Amount)
                .Map(dest => dest.TypeOfAmount, src => src.Ingredient.TypeOfAmount);

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
