using Microsoft.EntityFrameworkCore.Migrations;

namespace DrizlyBackEnd.Migrations
{
    public partial class BlackListTableUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannedAppUserName",
                table: "BlackLists");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "BlackLists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BannedAppİd",
                table: "BlackLists",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlackLists_AppUserId",
                table: "BlackLists",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlackLists_AspNetUsers_AppUserId",
                table: "BlackLists",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlackLists_AspNetUsers_AppUserId",
                table: "BlackLists");

            migrationBuilder.DropIndex(
                name: "IX_BlackLists_AppUserId",
                table: "BlackLists");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "BlackLists");

            migrationBuilder.DropColumn(
                name: "BannedAppİd",
                table: "BlackLists");

            migrationBuilder.AddColumn<string>(
                name: "BannedAppUserName",
                table: "BlackLists",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
