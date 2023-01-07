using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ogani.WebUI.Migrations
{
    public partial class AllTablesAddedDeletedDateColumnAndBlogsCategoriesTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Subscribes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Subscribes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "ProductUnits",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "ProductImages",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ContactPosts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "ContactPosts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "BlogTags",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BlogsCategoryId",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Authors",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BlogsCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogsCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogsCategoryId",
                table: "Blogs",
                column: "BlogsCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogsCategories_BlogsCategoryId",
                table: "Blogs",
                column: "BlogsCategoryId",
                principalTable: "BlogsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogsCategories_BlogsCategoryId",
                table: "Blogs");

            migrationBuilder.DropTable(
                name: "BlogsCategories");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_BlogsCategoryId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Subscribes");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "ProductUnits");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "ContactPosts");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "BlogTags");

            migrationBuilder.DropColumn(
                name: "BlogsCategoryId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Authors");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Subscribes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "ContactPosts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
