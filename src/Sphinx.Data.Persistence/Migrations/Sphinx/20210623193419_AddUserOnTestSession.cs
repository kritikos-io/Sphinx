// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Kritikos.Sphinx.Data.Persistence.Migrations.Sphinx
{
    public partial class AddUserOnTestSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "TestSessions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "TestSessions",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TestStages",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    TestSessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestStages_TestSessions_TestSessionId",
                        column: x => x.TestSessionId,
                        principalTable: "TestSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSessions",
                columns: table => new
                {
                    SphinxUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestSessionId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessions", x => new { x.SphinxUserId, x.TestSessionId });
                    table.ForeignKey(
                        name: "FK_UserSessions_AspNetUsers_SphinxUserId",
                        column: x => x.SphinxUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSessions_TestSessions_TestSessionId",
                        column: x => x.TestSessionId,
                        principalTable: "TestSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestSessions_UserId",
                table: "TestSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestStages_TestSessionId",
                table: "TestStages",
                column: "TestSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_TestSessionId",
                table: "UserSessions",
                column: "TestSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessions_AspNetUsers_UserId",
                table: "TestSessions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSessions_AspNetUsers_UserId",
                table: "TestSessions");

            migrationBuilder.DropTable(
                name: "TestStages");

            migrationBuilder.DropTable(
                name: "UserSessions");

            migrationBuilder.DropIndex(
                name: "IX_TestSessions_UserId",
                table: "TestSessions");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "TestSessions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TestSessions");
        }
    }
}
