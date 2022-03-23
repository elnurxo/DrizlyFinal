using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrizlyBackEnd.Migrations
{
    public partial class BaseEntityaddedINTOLiquorColorWineFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "WineFoodPairings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WineFoodPairings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "WineFoodPairings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "LiquorColors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LiquorColors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "LiquorColors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "WineFoodPairings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WineFoodPairings");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "WineFoodPairings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "LiquorColors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LiquorColors");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "LiquorColors");
        }
    }
}
