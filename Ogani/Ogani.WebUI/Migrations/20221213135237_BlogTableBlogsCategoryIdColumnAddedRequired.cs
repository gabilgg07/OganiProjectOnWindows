using Microsoft.EntityFrameworkCore.Migrations;

namespace Ogani.WebUI.Migrations
{
    public partial class BlogTableBlogsCategoryIdColumnAddedRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogsCategories_BlogsCategoryId",
                table: "Blogs");

            migrationBuilder.AlterColumn<int>(
                name: "BlogsCategoryId",
                table: "Blogs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogsCategories_BlogsCategoryId",
                table: "Blogs",
                column: "BlogsCategoryId",
                principalTable: "BlogsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogsCategories_BlogsCategoryId",
                table: "Blogs");

            migrationBuilder.AlterColumn<int>(
                name: "BlogsCategoryId",
                table: "Blogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogsCategories_BlogsCategoryId",
                table: "Blogs",
                column: "BlogsCategoryId",
                principalTable: "BlogsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
