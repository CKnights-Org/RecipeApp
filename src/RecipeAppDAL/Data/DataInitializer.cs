using Microsoft.EntityFrameworkCore;
using RecipeAppDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeAppDAL.Data
{
    public static class DataInitializer
    {
        //Specifying IDs is mandatory if seeding db through OnModelCreating method
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var ingedients = new List<Ingredient>()
            {
                new Ingredient { Id = 1, Name = "Milk" },
                new Ingredient { Id = 2, Name = "Sugar" },
                new Ingredient { Id = 3, Name = "Cheese" },
            };

            foreach (var ingedient in ingedients)
            {
                modelBuilder.Entity<Ingredient>().HasData(ingedient);
            }

            var recipes = new List<Recipe>()
            {
                new Recipe()
                {
                    Id = 1,
                    Name = "Test Recipe",
                    Description = "Test Description"
                },
                new Recipe()
                {
                    Id = 2,
                    Name = "Second Recipe",
                    Description = "Test Second Description"
                }
            };

            foreach (var recipe in recipes)
            {
                modelBuilder.Entity<Recipe>().HasData(recipe);
            }

            var reviews = new List<Review>()
            {
                new Review()
                {
                    Id = 1,
                    ReviewerName = "Jeffery",
                    RecipeID = 1,
                    Rating = 5,
                },
                new Review()
                {
                    Id = 2,
                    ReviewerName = "Michael",
                    RecipeID = 1,
                    Rating = 4,
                },
                new Review()
                {
                    Id = 3,
                    ReviewerName = "Julia",
                    RecipeID = 1,
                    Rating = 5,
                },
                new Review()
                {
                    Id = 4,
                    ReviewerName = "Michael",
                    RecipeID = 2,
                    Rating = 4,
                },
            };

            foreach (var review in reviews)
            {
                modelBuilder.Entity<Review>().HasData(review);
            }

            var ingredientRecipes = new List<IngredientRecipe>()
            {
                new IngredientRecipe()
                {
                    Id = 1,
                    IngredientID = 1,
                    RecipeID = 1,
                    Amount = 100,
                    TypeOfAmount = "ml",
                },
                new IngredientRecipe()
                {
                    Id = 2,
                    IngredientID = 2,
                    RecipeID = 1,
                    Amount = 1,
                    TypeOfAmount = "teaspoon",
                },
                new IngredientRecipe()
                {
                    Id = 3,
                    IngredientID = 3,
                    RecipeID = 2,
                    Amount = 100,
                    TypeOfAmount = "g",
                },
            };

            foreach (var ir in ingredientRecipes)
            {
                modelBuilder.Entity<IngredientRecipe>().HasData(ir);
            }
        }
    }
}
