using Microsoft.EntityFrameworkCore.Migrations;

namespace DrizlyBackEnd.Migrations
{
    public partial class CouponCategoryTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CouponCategoryId",
                table: "AppUserCoupons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CouponCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    DiscountPercent = table.Column<int>(nullable: false),
                    SaleValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserCoupons_CouponCategoryId",
                table: "AppUserCoupons",
                column: "CouponCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserCoupons_CouponCategories_CouponCategoryId",
                table: "AppUserCoupons",
                column: "CouponCategoryId",
                principalTable: "CouponCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserCoupons_CouponCategories_CouponCategoryId",
                table: "AppUserCoupons");

            migrationBuilder.DropTable(
                name: "CouponCategories");

            migrationBuilder.DropIndex(
                name: "IX_AppUserCoupons_CouponCategoryId",
                table: "AppUserCoupons");

            migrationBuilder.DropColumn(
                name: "CouponCategoryId",
                table: "AppUserCoupons");
        }
    }
}
