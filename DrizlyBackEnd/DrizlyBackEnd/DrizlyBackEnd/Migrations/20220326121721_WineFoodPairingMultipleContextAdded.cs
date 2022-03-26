using Microsoft.EntityFrameworkCore.Migrations;

namespace DrizlyBackEnd.Migrations
{
    public partial class WineFoodPairingMultipleContextAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFoodPairing_Products_ProductId",
                table: "ProductFoodPairing");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFoodPairing_WineFoodPairings_WineFoodPairingId",
                table: "ProductFoodPairing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductFoodPairing",
                table: "ProductFoodPairing");

            migrationBuilder.RenameTable(
                name: "ProductFoodPairing",
                newName: "ProductFoodPairings");

            migrationBuilder.RenameIndex(
                name: "IX_ProductFoodPairing_WineFoodPairingId",
                table: "ProductFoodPairings",
                newName: "IX_ProductFoodPairings_WineFoodPairingId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductFoodPairing_ProductId",
                table: "ProductFoodPairings",
                newName: "IX_ProductFoodPairings_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductFoodPairings",
                table: "ProductFoodPairings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFoodPairings_Products_ProductId",
                table: "ProductFoodPairings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFoodPairings_WineFoodPairings_WineFoodPairingId",
                table: "ProductFoodPairings",
                column: "WineFoodPairingId",
                principalTable: "WineFoodPairings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFoodPairings_Products_ProductId",
                table: "ProductFoodPairings");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFoodPairings_WineFoodPairings_WineFoodPairingId",
                table: "ProductFoodPairings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductFoodPairings",
                table: "ProductFoodPairings");

            migrationBuilder.RenameTable(
                name: "ProductFoodPairings",
                newName: "ProductFoodPairing");

            migrationBuilder.RenameIndex(
                name: "IX_ProductFoodPairings_WineFoodPairingId",
                table: "ProductFoodPairing",
                newName: "IX_ProductFoodPairing_WineFoodPairingId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductFoodPairings_ProductId",
                table: "ProductFoodPairing",
                newName: "IX_ProductFoodPairing_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductFoodPairing",
                table: "ProductFoodPairing",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFoodPairing_Products_ProductId",
                table: "ProductFoodPairing",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFoodPairing_WineFoodPairings_WineFoodPairingId",
                table: "ProductFoodPairing",
                column: "WineFoodPairingId",
                principalTable: "WineFoodPairings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
