using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ogani.WebUI.Migrations
{
    public partial class AuditLogsTaableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedByUserId = table.Column<int>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: false),
                    DeletedByUserId = table.Column<int>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    QueryString = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    StatusCode = table.Column<int>(nullable: false),
                    RequestDate = table.Column<DateTime>(nullable: false),
                    ResponseDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_DeletedByUserId",
                        column: x => x.DeletedByUserId,
                        principalSchema: "Membership",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_CreatedByUserId",
                table: "AuditLogs",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_DeletedByUserId",
                table: "AuditLogs",
                column: "DeletedByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");
        }
    }
}
