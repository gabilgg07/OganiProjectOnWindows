using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ogani.WebUI.Migrations
{
    public partial class BlogCategoryTableNameModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "BlogsCategoryId",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "BlogCategoryId",
                table: "Blogs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BlogCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_BlogCategoryId",
                table: "Blogs",
                column: "BlogCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BlogCategories_BlogCategoryId",
                table: "Blogs",
                column: "BlogCategoryId",
                principalTable: "BlogCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BlogCategories_BlogCategoryId",
                table: "Blogs");

            migrationBuilder.DropTable(
                name: "BlogCategories");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_BlogCategoryId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "BlogCategoryId",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "BlogsCategoryId",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BlogsCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
