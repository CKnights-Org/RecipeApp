using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RecipeAppDAL.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientsWithAmount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IngredientID = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeID = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    TypeOfAmount = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsWithAmount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientsWithAmount_Ingredients_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngredientsWithAmount_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReviewerName = table.Column<string>(type: "TEXT", nullable: false),
                    Rating = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Milk" },
                    { 2, "Sugar" },
                    { 3, "Cheese" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Test Description", "Test Recipe" },
                    { 2, "Test Second Description", "Second Recipe" }
                });

            migrationBuilder.InsertData(
                table: "IngredientsWithAmount",
                columns: new[] { "Id", "Amount", "IngredientID", "RecipeID", "TypeOfAmount" },
                values: new object[,]
                {
                    { 1, 100, 1, 1, "ml" },
                    { 2, 1, 2, 1, "teaspoon" },
                    { 3, 100, 3, 2, "g" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Rating", "RecipeID", "ReviewerName" },
                values: new object[,]
                {
                    { 1, 5, 1, "Jeffery" },
                    { 2, 4, 1, "Michael" },
                    { 3, 5, 1, "Julia" },
                    { 4, 4, 2, "Michael" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsWithAmount_IngredientID",
                table: "IngredientsWithAmount",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsWithAmount_RecipeID",
                table: "IngredientsWithAmount",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RecipeID",
                table: "Reviews",
                column: "RecipeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientsWithAmount");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
