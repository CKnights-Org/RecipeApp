﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using System.Linq;
using RecipeAppDAL.Models;
using RecipeAppDAL.Data;
using Microsoft.Extensions.Options;

namespace PizzaShopDAL.Data
{
    public class RecipeAppDBContext : DbContext
    {
        /* Toto nieje spravna cesta, ako to robit, avsak pre demonstraciu postacujuca */
        // private const string DatabaseName = "PizzaShop";
        // private const string ConnectionString = $"Server=(localdb)\\mssqllocaldb;Integrated Security=True;MultipleActiveResultSets=True;Database={DatabaseName};Trusted_Connection=True;";
        public string? DbPath { get; }

        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<Ingredient> Ingredients { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;

        protected DbSet<IngredientRecipe> IngredientsWithAmount { get; set; } = null!;

        public RecipeAppDBContext(DbContextOptions<RecipeAppDBContext> options) : base(options)
        {
        }

        // https://docs.microsoft.com/en-us/ef/core/modeling/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>()
                .HasMany(x => x.IngredientRecipe)
                .WithOne(x => x.Recipe);

            modelBuilder.Entity<Ingredient>()
                .HasMany(x => x.IngredientRecipe)
                .WithOne(x => x.Ingredient);

            modelBuilder.Entity<IngredientRecipe>()
                .HasOne(x => x.Recipe)
                .WithMany(x => x.IngredientRecipe);
            modelBuilder.Entity<IngredientRecipe>()
                .HasOne(x => x.Ingredient)
                .WithMany(x => x.IngredientRecipe);

            modelBuilder.Entity<Recipe>()
                .HasMany(x => x.Reviews)
                .WithOne(x => x.Recipe);


            // foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            // {
            //     relationship.DeleteBehavior = DeleteBehavior.NoAction;
            // }

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }

    }
}
