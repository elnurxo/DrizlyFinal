using Microsoft.EntityFrameworkCore.Migrations;

namespace DrizlyBackEnd.Migrations
{
    public partial class SweetDryScaleTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SweetDryScaleId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "sweetDryScales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 85, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sweetDryScales", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SweetDryScaleId",
                table: "Products",
                column: "SweetDryScaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_sweetDryScales_SweetDryScaleId",
                table: "Products",
                column: "SweetDryScaleId",
                principalTable: "sweetDryScales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_sweetDryScales_SweetDryScaleId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "sweetDryScales");

            migrationBuilder.DropIndex(
                name: "IX_Products_SweetDryScaleId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SweetDryScaleId",
                table: "Products");
        }
    }
}
