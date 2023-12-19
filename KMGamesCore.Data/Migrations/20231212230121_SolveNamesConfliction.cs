using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KMGamesCore.Data.Migrations
{
    public partial class SolveNamesConfliction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameShoppingCart");

            migrationBuilder.DropTable(
                name: "GamesInCart");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameShoppingCart",
                columns: table => new
                {
                    GamesGameId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartsShoppingCartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameShoppingCart", x => new { x.GamesGameId, x.ShoppingCartsShoppingCartId });
                    table.ForeignKey(
                        name: "FK_GameShoppingCart_Games_GamesGameId",
                        column: x => x.GamesGameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameShoppingCart_ShoppingCarts_ShoppingCartsShoppingCartId",
                        column: x => x.ShoppingCartsShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "ShoppingCartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GamesInCart",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesInCart", x => new { x.GameId, x.ShoppingCartId });
                    table.ForeignKey(
                        name: "FK_GamesInCart_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamesInCart_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "ShoppingCartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameShoppingCart_ShoppingCartsShoppingCartId",
                table: "GameShoppingCart",
                column: "ShoppingCartsShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesInCart_ShoppingCartId",
                table: "GamesInCart",
                column: "ShoppingCartId");
        }
    }
}
