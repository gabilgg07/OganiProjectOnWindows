using Microsoft.EntityFrameworkCore.Migrations;

namespace Ogani.WebUI.Migrations
{
    public partial class BlogCategoryIdSetRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogCategories_BlogCategoryId",
                table: "Blogs");

            migrationBuilder.AlterColumn<int>(
                name: "BlogCategoryId",
                table: "Blogs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogCategories_BlogCategoryId",
                table: "Blogs",
                column: "BlogCategoryId",
                principalTable: "BlogCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogCategories_BlogCategoryId",
                table: "Blogs");

            migrationBuilder.AlterColumn<int>(
                name: "BlogCategoryId",
                table: "Blogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogCategories_BlogCategoryId",
                table: "Blogs",
                column: "BlogCategoryId",
                principalTable: "BlogCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
