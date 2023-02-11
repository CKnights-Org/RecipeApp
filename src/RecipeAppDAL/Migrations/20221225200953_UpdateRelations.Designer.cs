﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipeAppDAL.Data;

#nullable disable

namespace RecipeAppDAL.Migrations
{
    [DbContext(typeof(RecipeAppDBContext))]
    [Migration("20221225200953_UpdateRelations")]
    partial class UpdateRelations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("RecipeAppDAL.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Milk"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Sugar"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Cheese"
                        });
                });

            modelBuilder.Entity("RecipeAppDAL.Models.IngredientRecipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IngredientID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RecipeID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TypeOfAmount")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IngredientID");

                    b.HasIndex("RecipeID");

                    b.ToTable("IngredientsWithAmount");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 100,
                            IngredientID = 1,
                            RecipeID = 1,
                            TypeOfAmount = "ml"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 1,
                            IngredientID = 2,
                            RecipeID = 1,
                            TypeOfAmount = "teaspoon"
                        },
                        new
                        {
                            Id = 3,
                            Amount = 100,
                            IngredientID = 3,
                            RecipeID = 2,
                            TypeOfAmount = "g"
                        });
                });

            modelBuilder.Entity("RecipeAppDAL.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Test Description",
                            Name = "Test Recipe"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Test Second Description",
                            Name = "Second Recipe"
                        });
                });

            modelBuilder.Entity("RecipeAppDAL.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RecipeID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReviewerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RecipeID");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Rating = 5,
                            RecipeID = 1,
                            ReviewerName = "Jeffery"
                        },
                        new
                        {
                            Id = 2,
                            Rating = 4,
                            RecipeID = 1,
                            ReviewerName = "Michael"
                        },
                        new
                        {
                            Id = 3,
                            Rating = 5,
                            RecipeID = 1,
                            ReviewerName = "Julia"
                        },
                        new
                        {
                            Id = 4,
                            Rating = 4,
                            RecipeID = 2,
                            ReviewerName = "Michael"
                        });
                });

            modelBuilder.Entity("RecipeAppDAL.Models.IngredientRecipe", b =>
                {
                    b.HasOne("RecipeAppDAL.Models.Ingredient", "Ingredient")
                        .WithMany("IngredientRecipe")
                        .HasForeignKey("IngredientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RecipeAppDAL.Models.Recipe", "Recipe")
                        .WithMany("IngredientRecipe")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeAppDAL.Models.Review", b =>
                {
                    b.HasOne("RecipeAppDAL.Models.Recipe", "Recipe")
                        .WithMany("Reviews")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("RecipeAppDAL.Models.Ingredient", b =>
                {
                    b.Navigation("IngredientRecipe");
                });

            modelBuilder.Entity("RecipeAppDAL.Models.Recipe", b =>
                {
                    b.Navigation("IngredientRecipe");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
