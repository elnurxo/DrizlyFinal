using Microsoft.EntityFrameworkCore.Migrations;

namespace DrizlyBackEnd.Migrations
{
    public partial class AppUserCouponsTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserCoupons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(nullable: false),
                    Coupon = table.Column<string>(maxLength: 36, nullable: false),
                    IsUsed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserCoupons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserCoupons_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserCoupons_AppUserId",
                table: "AppUserCoupons",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserCoupons");
        }
    }
}
