using Microsoft.EntityFrameworkCore.Migrations;

namespace DrizlyBackEnd.Migrations
{
    public partial class WineFoodPairingAddedintoProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WineFoodPairingId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WineFoodPairings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 85, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WineFoodPairings", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_WineFoodPairings_WineFoodPairingId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "WineFoodPairings");

            migrationBuilder.DropIndex(
                name: "IX_Products_WineFoodPairingId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WineFoodPairingId",
                table: "Products");
        }
    }
}
