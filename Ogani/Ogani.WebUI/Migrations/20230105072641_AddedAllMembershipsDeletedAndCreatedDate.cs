using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ogani.WebUI.Migrations
{
    public partial class AddedAllMembershipsDeletedAndCreatedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "Membership",
                table: "UserTokens",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                schema: "Membership",
                table: "UserTokens",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "Membership",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                schema: "Membership",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "Membership",
                table: "UserLogins",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                schema: "Membership",
                table: "UserLogins",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "Membership",
                table: "UserClaims",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                schema: "Membership",
                table: "UserClaims",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "Membership",
                table: "Roles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                schema: "Membership",
                table: "Roles",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "Membership",
                table: "RoleClaims",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                schema: "Membership",
                table: "RoleClaims",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "Membership",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                schema: "Membership",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "Membership",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                schema: "Membership",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "Membership",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                schema: "Membership",
                table: "UserLogins");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "Membership",
                table: "UserClaims");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                schema: "Membership",
                table: "UserClaims");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "Membership",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                schema: "Membership",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "Membership",
                table: "RoleClaims");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                schema: "Membership",
                table: "RoleClaims");
        }
    }
}
