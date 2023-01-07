using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ogani.WebUI.Migrations
{
    public partial class AddedBaseEntityToAllEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Subscribes",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Subscribes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Subscribes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "ProductUnits",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProductUnits",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ProductUnits",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "ProductImages",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProductImages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ProductImages",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "ProductImages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "ContactPosts",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ContactPosts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "ContactPosts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "BlogTags",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BlogTags",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "BlogTags",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "BlogTagBlogs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BlogTagBlogs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "BlogTagBlogs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "BlogTagBlogs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Blogs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "BlogCategories",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BlogCategories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "BlogCategories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Authors",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Authors",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedByUserId",
                table: "Authors",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AuditLogs",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribes_CreatedByUserId",
                table: "Subscribes",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribes_DeletedByUserId",
                table: "Subscribes",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnits_CreatedByUserId",
                table: "ProductUnits",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnits_DeletedByUserId",
                table: "ProductUnits",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedByUserId",
                table: "Products",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DeletedByUserId",
                table: "Products",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_CreatedByUserId",
                table: "ProductImages",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_DeletedByUserId",
                table: "ProductImages",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPosts_CreatedByUserId",
                table: "ContactPosts",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPosts_DeletedByUserId",
                table: "ContactPosts",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedByUserId",
                table: "Categories",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DeletedByUserId",
                table: "Categories",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTags_CreatedByUserId",
                table: "BlogTags",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTags_DeletedByUserId",
                table: "BlogTags",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTagBlogs_CreatedByUserId",
                table: "BlogTagBlogs",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTagBlogs_DeletedByUserId",
                table: "BlogTagBlogs",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CreatedByUserId",
                table: "Blogs",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_DeletedByUserId",
                table: "Blogs",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_CreatedByUserId",
                table: "BlogCategories",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogCategories_DeletedByUserId",
                table: "BlogCategories",
                column: "DeletedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_CreatedByUserId",
                table: "Authors",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_DeletedByUserId",
                table: "Authors",
                column: "DeletedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Users_CreatedByUserId",
                table: "Authors",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Users_DeletedByUserId",
                table: "Authors",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogCategories_Users_CreatedByUserId",
                table: "BlogCategories",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogCategories_Users_DeletedByUserId",
                table: "BlogCategories",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_CreatedByUserId",
                table: "Blogs",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_DeletedByUserId",
                table: "Blogs",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTagBlogs_Users_CreatedByUserId",
                table: "BlogTagBlogs",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTagBlogs_Users_DeletedByUserId",
                table: "BlogTagBlogs",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTags_Users_CreatedByUserId",
                table: "BlogTags",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTags_Users_DeletedByUserId",
                table: "BlogTags",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_CreatedByUserId",
                table: "Categories",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_DeletedByUserId",
                table: "Categories",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPosts_Users_CreatedByUserId",
                table: "ContactPosts",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPosts_Users_DeletedByUserId",
                table: "ContactPosts",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Users_CreatedByUserId",
                table: "ProductImages",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Users_DeletedByUserId",
                table: "ProductImages",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatedByUserId",
                table: "Products",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_DeletedByUserId",
                table: "Products",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUnits_Users_CreatedByUserId",
                table: "ProductUnits",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUnits_Users_DeletedByUserId",
                table: "ProductUnits",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscribes_Users_CreatedByUserId",
                table: "Subscribes",
                column: "CreatedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscribes_Users_DeletedByUserId",
                table: "Subscribes",
                column: "DeletedByUserId",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Users_CreatedByUserId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Users_DeletedByUserId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogCategories_Users_CreatedByUserId",
                table: "BlogCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogCategories_Users_DeletedByUserId",
                table: "BlogCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_CreatedByUserId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_DeletedByUserId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTagBlogs_Users_CreatedByUserId",
                table: "BlogTagBlogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTagBlogs_Users_DeletedByUserId",
                table: "BlogTagBlogs");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTags_Users_CreatedByUserId",
                table: "BlogTags");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTags_Users_DeletedByUserId",
                table: "BlogTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_CreatedByUserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_DeletedByUserId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactPosts_Users_CreatedByUserId",
                table: "ContactPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactPosts_Users_DeletedByUserId",
                table: "ContactPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Users_CreatedByUserId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Users_DeletedByUserId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_CreatedByUserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_DeletedByUserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductUnits_Users_CreatedByUserId",
                table: "ProductUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductUnits_Users_DeletedByUserId",
                table: "ProductUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscribes_Users_CreatedByUserId",
                table: "Subscribes");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscribes_Users_DeletedByUserId",
                table: "Subscribes");

            migrationBuilder.DropIndex(
                name: "IX_Subscribes_CreatedByUserId",
                table: "Subscribes");

            migrationBuilder.DropIndex(
                name: "IX_Subscribes_DeletedByUserId",
                table: "Subscribes");

            migrationBuilder.DropIndex(
                name: "IX_ProductUnits_CreatedByUserId",
                table: "ProductUnits");

            migrationBuilder.DropIndex(
                name: "IX_ProductUnits_DeletedByUserId",
                table: "ProductUnits");

            migrationBuilder.DropIndex(
                name: "IX_Products_CreatedByUserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DeletedByUserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_CreatedByUserId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_DeletedByUserId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ContactPosts_CreatedByUserId",
                table: "ContactPosts");

            migrationBuilder.DropIndex(
                name: "IX_ContactPosts_DeletedByUserId",
                table: "ContactPosts");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CreatedByUserId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_DeletedByUserId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_BlogTags_CreatedByUserId",
                table: "BlogTags");

            migrationBuilder.DropIndex(
                name: "IX_BlogTags_DeletedByUserId",
                table: "BlogTags");

            migrationBuilder.DropIndex(
                name: "IX_BlogTagBlogs_CreatedByUserId",
                table: "BlogTagBlogs");

            migrationBuilder.DropIndex(
                name: "IX_BlogTagBlogs_DeletedByUserId",
                table: "BlogTagBlogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_CreatedByUserId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_DeletedByUserId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_BlogCategories_CreatedByUserId",
                table: "BlogCategories");

            migrationBuilder.DropIndex(
                name: "IX_BlogCategories_DeletedByUserId",
                table: "BlogCategories");

            migrationBuilder.DropIndex(
                name: "IX_Authors_CreatedByUserId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_DeletedByUserId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Subscribes");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Subscribes");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ProductUnits");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProductUnits");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ProductUnits");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ContactPosts");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ContactPosts");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "ContactPosts");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "BlogTags");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BlogTags");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "BlogTags");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "BlogTagBlogs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BlogTagBlogs");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "BlogTagBlogs");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "BlogTagBlogs");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "BlogCategories");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "DeletedByUserId",
                table: "Authors");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Subscribes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "AuditLogs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
