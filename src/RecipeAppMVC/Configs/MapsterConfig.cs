using Mapster;
using RecipeAppDAL.Models;
using RecipeAppMVC.Models.Recipe;
using System;

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
        }
    }
}
