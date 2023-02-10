using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeAppDAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeletioninDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsWithAmount_Recipes_RecipeID",
                table: "IngredientsWithAmount");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsWithAmount_Recipes_RecipeID",
                table: "IngredientsWithAmount",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientsWithAmount_Recipes_RecipeID",
                table: "IngredientsWithAmount");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientsWithAmount_Recipes_RecipeID",
                table: "IngredientsWithAmount",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
