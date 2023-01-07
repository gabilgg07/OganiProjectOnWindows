using Microsoft.EntityFrameworkCore.Migrations;

namespace Ogani.WebUI.Migrations
{
    public partial class AppInfoPinterestNameModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pinteres",
                table: "AppInfos");

            migrationBuilder.AddColumn<string>(
                name: "Pinterest",
                table: "AppInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pinterest",
                table: "AppInfos");

            migrationBuilder.AddColumn<string>(
                name: "Pinteres",
                table: "AppInfos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
