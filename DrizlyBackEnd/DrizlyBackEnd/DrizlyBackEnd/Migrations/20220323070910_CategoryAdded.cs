using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrizlyBackEnd.Migrations
{
    public partial class CategoryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LiquorFlavorId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LiquorFlavors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 85, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiquorFlavors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_LiquorFlavorId",
                table: "Products",
                column: "LiquorFlavorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_LiquorFlavors_LiquorFlavorId",
                table: "Products",
                column: "LiquorFlavorId",
                principalTable: "LiquorFlavors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_LiquorFlavors_LiquorFlavorId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "LiquorFlavors");

            migrationBuilder.DropIndex(
                name: "IX_Products_LiquorFlavorId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LiquorFlavorId",
                table: "Products");
        }
    }
}
