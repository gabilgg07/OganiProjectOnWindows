using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ogani.WebUI.Migrations
{
    public partial class BlogTableBodyAndPublishedDateAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "WrotedDate",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "Blogs",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedDate",
                table: "Blogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "WrotedDate",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
