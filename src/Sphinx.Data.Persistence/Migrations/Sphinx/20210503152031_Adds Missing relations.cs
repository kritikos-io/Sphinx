// <auto-generated/>
using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kritikos.Sphinx.Data.Persistence.Migrations.Sphinx
{
    public partial class AddsMissingrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestions_Stimuli_PrimaryId",
                table: "SessionQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestions_Stimuli_SecondaryId",
                table: "SessionQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_Stimuli_DataSets_DataSetId",
                table: "Stimuli");

            migrationBuilder.DropIndex(
                name: "IX_SessionQuestions_SecondaryId",
                table: "SessionQuestions");

            migrationBuilder.DropColumn(
                name: "isSignificant",
                table: "Stimuli");

            migrationBuilder.RenameColumn(
                name: "SecondaryId",
                table: "SessionQuestions",
                newName: "SecondaryStimulusId");

            migrationBuilder.RenameColumn(
                name: "PrimaryId",
                table: "SessionQuestions",
                newName: "PrimaryStimulusId");

            migrationBuilder.RenameIndex(
                name: "IX_SessionQuestions_PrimaryId",
                table: "SessionQuestions",
                newName: "IX_SessionQuestions_PrimaryStimulusId");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "TestSessions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "TestSessions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "TestSessions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "TestSessions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "DataSetId",
                table: "Stimuli",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Stimuli",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "Stimuli",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Stimuli",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "MediaType",
                table: "Stimuli",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Stimuli",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Stimuli",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "SessionQuestions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "SessionQuestions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "SessionQuestions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "SessionQuestions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "DataSets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "DataSets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "DataSets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "DataSets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SignificantMatch",
                columns: table => new
                {
                    PrimaryStimulusId = table.Column<long>(type: "bigint", nullable: false),
                    SecondaryStimulusId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignificantMatch", x => new { x.PrimaryStimulusId, x.SecondaryStimulusId });
                    table.ForeignKey(
                        name: "FK_SignificantMatch_Stimuli_PrimaryStimulusId",
                        column: x => x.PrimaryStimulusId,
                        principalTable: "Stimuli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SignificantMatch_Stimuli_SecondaryStimulusId",
                        column: x => x.SecondaryStimulusId,
                        principalTable: "Stimuli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionQuestions_SecondaryStimulusId",
                table: "SessionQuestions",
                column: "SecondaryStimulusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SignificantMatch_SecondaryStimulusId",
                table: "SignificantMatch",
                column: "SecondaryStimulusId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestions_Stimuli_PrimaryStimulusId",
                table: "SessionQuestions",
                column: "PrimaryStimulusId",
                principalTable: "Stimuli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestions_Stimuli_SecondaryStimulusId",
                table: "SessionQuestions",
                column: "SecondaryStimulusId",
                principalTable: "Stimuli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stimuli_DataSets_DataSetId",
                table: "Stimuli",
                column: "DataSetId",
                principalTable: "DataSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestions_Stimuli_PrimaryStimulusId",
                table: "SessionQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestions_Stimuli_SecondaryStimulusId",
                table: "SessionQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_Stimuli_DataSets_DataSetId",
                table: "Stimuli");

            migrationBuilder.DropTable(
                name: "SignificantMatch");

            migrationBuilder.DropIndex(
                name: "IX_SessionQuestions_SecondaryStimulusId",
                table: "SessionQuestions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TestSessions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TestSessions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TestSessions");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TestSessions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Stimuli");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Stimuli");

            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "Stimuli");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Stimuli");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Stimuli");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SessionQuestions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SessionQuestions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "SessionQuestions");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SessionQuestions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DataSets");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DataSets");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "DataSets");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DataSets");

            migrationBuilder.RenameColumn(
                name: "SecondaryStimulusId",
                table: "SessionQuestions",
                newName: "SecondaryId");

            migrationBuilder.RenameColumn(
                name: "PrimaryStimulusId",
                table: "SessionQuestions",
                newName: "PrimaryId");

            migrationBuilder.RenameIndex(
                name: "IX_SessionQuestions_PrimaryStimulusId",
                table: "SessionQuestions",
                newName: "IX_SessionQuestions_PrimaryId");

            migrationBuilder.AlterColumn<Guid>(
                name: "DataSetId",
                table: "Stimuli",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Stimuli",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isSignificant",
                table: "Stimuli",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_SessionQuestions_SecondaryId",
                table: "SessionQuestions",
                column: "SecondaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestions_Stimuli_PrimaryId",
                table: "SessionQuestions",
                column: "PrimaryId",
                principalTable: "Stimuli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestions_Stimuli_SecondaryId",
                table: "SessionQuestions",
                column: "SecondaryId",
                principalTable: "Stimuli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stimuli_DataSets_DataSetId",
                table: "Stimuli",
                column: "DataSetId",
                principalTable: "DataSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}