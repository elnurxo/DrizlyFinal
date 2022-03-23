using Microsoft.EntityFrameworkCore.Migrations;

namespace DrizlyBackEnd.Migrations
{
    public partial class LiquorFlavorAddedintoProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LiquorColorId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LiquorColors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(maxLength: 85, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiquorColors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_LiquorColorId",
                table: "Products",
                column: "LiquorColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_LiquorColors_LiquorColorId",
                table: "Products",
                column: "LiquorColorId",
                principalTable: "LiquorColors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_LiquorColors_LiquorColorId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "LiquorColors");

            migrationBuilder.DropIndex(
                name: "IX_Products_LiquorColorId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LiquorColorId",
                table: "Products");
        }
    }
}
