using Microsoft.EntityFrameworkCore.Migrations;

namespace Ogani.WebUI.Migrations
{
    public partial class AppInfoSosialNetworsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "AppInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "AppInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Linkedin",
                table: "AppInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pinteres",
                table: "AppInfos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "AppInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "AppInfos");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "AppInfos");

            migrationBuilder.DropColumn(
                name: "Linkedin",
                table: "AppInfos");

            migrationBuilder.DropColumn(
                name: "Pinteres",
                table: "AppInfos");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "AppInfos");
        }
    }
}
