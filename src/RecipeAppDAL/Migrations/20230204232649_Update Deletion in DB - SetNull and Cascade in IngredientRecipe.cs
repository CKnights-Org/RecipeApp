using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAppDAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeletioninDBSetNullandCascadeinIngredientRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsWithAmount_Ingredients_IngredientID",
                table: "IngredientsWithAmount");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsWithAmount_Recipes_RecipeID",
                table: "IngredientsWithAmount");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsWithAmount_Ingredients_IngredientID",
                table: "IngredientsWithAmount",
                column: "IngredientID",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsWithAmount_Recipes_RecipeID",
                table: "IngredientsWithAmount",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsWithAmount_Ingredients_IngredientID",
                table: "IngredientsWithAmount");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsWithAmount_Recipes_RecipeID",
                table: "IngredientsWithAmount");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsWithAmount_Ingredients_IngredientID",
                table: "IngredientsWithAmount",
                column: "IngredientID",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsWithAmount_Recipes_RecipeID",
                table: "IngredientsWithAmount",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
