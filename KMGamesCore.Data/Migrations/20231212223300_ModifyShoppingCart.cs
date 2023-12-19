using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KMGamesCore.Data.Migrations
{
    public partial class ModifyShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Games_GameId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_GameId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "ShoppingCarts");

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
                name: "IX_GamesInCart_GameId",
                table: "GamesInCart",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesInCart_ShoppingCartId",
                table: "GamesInCart",
                column: "ShoppingCartId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "GamesInCart");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_GameId",
                table: "ShoppingCarts",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Games_GameId",
                table: "ShoppingCarts",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
