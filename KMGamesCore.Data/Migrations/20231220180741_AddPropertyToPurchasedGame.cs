using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KMGamesCore.Data.Migrations
{
    public partial class AddPropertyToPurchasedGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Purchased",
                table: "PurchasedGames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Purchased",
                table: "PurchasedGames");
        }
    }
}
