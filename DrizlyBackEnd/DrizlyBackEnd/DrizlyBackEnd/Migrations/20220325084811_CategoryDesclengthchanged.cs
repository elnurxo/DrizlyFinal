using Microsoft.EntityFrameworkCore.Migrations;

namespace DrizlyBackEnd.Migrations
{
    public partial class CategoryDesclengthchanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Desc",
                table: "Categories",
                maxLength: 2500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(800)",
                oldMaxLength: 800);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Desc",
                table: "Categories",
                type: "nvarchar(800)",
                maxLength: 800,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2500);
        }
    }
}
