using Microsoft.EntityFrameworkCore.Migrations;

namespace DrizlyBackEnd.Migrations
{
    public partial class WineFoodPairingMultipleAlter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_WineFoodPairings_WineFoodPairingId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_WineFoodPairingId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WineFoodPairingId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductFoodPairing",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    WineFoodPairingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFoodPairing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFoodPairing_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductFoodPairing_WineFoodPairings_WineFoodPairingId",
                        column: x => x.WineFoodPairingId,
                        principalTable: "WineFoodPairings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductFoodPairing_ProductId",
                table: "ProductFoodPairing",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFoodPairing_WineFoodPairingId",
                table: "ProductFoodPairing",
                column: "WineFoodPairingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductFoodPairing");

            migrationBuilder.AddColumn<int>(
                name: "WineFoodPairingId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_WineFoodPairingId",
                table: "Products",
                column: "WineFoodPairingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_WineFoodPairings_WineFoodPairingId",
                table: "Products",
                column: "WineFoodPairingId",
                principalTable: "WineFoodPairings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
