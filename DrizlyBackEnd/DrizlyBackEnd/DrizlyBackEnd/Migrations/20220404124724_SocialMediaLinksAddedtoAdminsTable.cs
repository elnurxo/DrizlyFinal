using Microsoft.EntityFrameworkCore.Migrations;

namespace DrizlyBackEnd.Migrations
{
    public partial class SocialMediaLinksAddedtoAdminsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacebookUrl",
                table: "AspNetUsers",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstagramUrl",
                table: "AspNetUsers",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterUrl",
                table: "AspNetUsers",
                maxLength: 120,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "InstagramUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TwitterUrl",
                table: "AspNetUsers");
        }
    }
}
