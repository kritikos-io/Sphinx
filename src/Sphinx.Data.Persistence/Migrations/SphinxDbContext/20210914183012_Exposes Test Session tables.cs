using Microsoft.EntityFrameworkCore.Migrations;

namespace Kritikos.Sphinx.Data.Persistence.Migrations
{
    public partial class ExposesTestSessiontables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestSession_StimulusGroups_AId",
                table: "TestSession");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSession_StimulusGroups_BId",
                table: "TestSession");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSession_StimulusGroups_CId",
                table: "TestSession");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSession_StimulusGroups_DId",
                table: "TestSession");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSession_StimulusGroups_EId",
                table: "TestSession");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessionQuestion_Stimuli_CorrectAnswerId",
                table: "TestSessionQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessionQuestion_Stimuli_False1Id",
                table: "TestSessionQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessionQuestion_Stimuli_False2Id",
                table: "TestSessionQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessionQuestion_Stimuli_False3Id",
                table: "TestSessionQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessionQuestion_Stimuli_UnderTestId",
                table: "TestSessionQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessionQuestion_TestSession_SessionId",
                table: "TestSessionQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestSessionQuestion",
                table: "TestSessionQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestSession",
                table: "TestSession");

            migrationBuilder.RenameTable(
                name: "TestSessionQuestion",
                newName: "SessionQuestions");

            migrationBuilder.RenameTable(
                name: "TestSession",
                newName: "TestSessions");

            migrationBuilder.RenameIndex(
                name: "IX_TestSessionQuestion_UnderTestId",
                table: "SessionQuestions",
                newName: "IX_SessionQuestions_UnderTestId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSessionQuestion_SessionId",
                table: "SessionQuestions",
                newName: "IX_SessionQuestions_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSessionQuestion_False3Id",
                table: "SessionQuestions",
                newName: "IX_SessionQuestions_False3Id");

            migrationBuilder.RenameIndex(
                name: "IX_TestSessionQuestion_False2Id",
                table: "SessionQuestions",
                newName: "IX_SessionQuestions_False2Id");

            migrationBuilder.RenameIndex(
                name: "IX_TestSessionQuestion_False1Id",
                table: "SessionQuestions",
                newName: "IX_SessionQuestions_False1Id");

            migrationBuilder.RenameIndex(
                name: "IX_TestSessionQuestion_CorrectAnswerId",
                table: "SessionQuestions",
                newName: "IX_SessionQuestions_CorrectAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSession_EId",
                table: "TestSessions",
                newName: "IX_TestSessions_EId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSession_DId",
                table: "TestSessions",
                newName: "IX_TestSessions_DId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSession_CId",
                table: "TestSessions",
                newName: "IX_TestSessions_CId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSession_BId",
                table: "TestSessions",
                newName: "IX_TestSessions_BId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSession_AId",
                table: "TestSessions",
                newName: "IX_TestSessions_AId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SessionQuestions",
                table: "SessionQuestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestSessions",
                table: "TestSessions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestions_Stimuli_CorrectAnswerId",
                table: "SessionQuestions",
                column: "CorrectAnswerId",
                principalTable: "Stimuli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestions_Stimuli_False1Id",
                table: "SessionQuestions",
                column: "False1Id",
                principalTable: "Stimuli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestions_Stimuli_False2Id",
                table: "SessionQuestions",
                column: "False2Id",
                principalTable: "Stimuli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestions_Stimuli_False3Id",
                table: "SessionQuestions",
                column: "False3Id",
                principalTable: "Stimuli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestions_Stimuli_UnderTestId",
                table: "SessionQuestions",
                column: "UnderTestId",
                principalTable: "Stimuli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SessionQuestions_TestSessions_SessionId",
                table: "SessionQuestions",
                column: "SessionId",
                principalTable: "TestSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessions_StimulusGroups_AId",
                table: "TestSessions",
                column: "AId",
                principalTable: "StimulusGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessions_StimulusGroups_BId",
                table: "TestSessions",
                column: "BId",
                principalTable: "StimulusGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessions_StimulusGroups_CId",
                table: "TestSessions",
                column: "CId",
                principalTable: "StimulusGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessions_StimulusGroups_DId",
                table: "TestSessions",
                column: "DId",
                principalTable: "StimulusGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessions_StimulusGroups_EId",
                table: "TestSessions",
                column: "EId",
                principalTable: "StimulusGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestions_Stimuli_CorrectAnswerId",
                table: "SessionQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestions_Stimuli_False1Id",
                table: "SessionQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestions_Stimuli_False2Id",
                table: "SessionQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestions_Stimuli_False3Id",
                table: "SessionQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestions_Stimuli_UnderTestId",
                table: "SessionQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SessionQuestions_TestSessions_SessionId",
                table: "SessionQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessions_StimulusGroups_AId",
                table: "TestSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessions_StimulusGroups_BId",
                table: "TestSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessions_StimulusGroups_CId",
                table: "TestSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessions_StimulusGroups_DId",
                table: "TestSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestSessions_StimulusGroups_EId",
                table: "TestSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestSessions",
                table: "TestSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SessionQuestions",
                table: "SessionQuestions");

            migrationBuilder.RenameTable(
                name: "TestSessions",
                newName: "TestSession");

            migrationBuilder.RenameTable(
                name: "SessionQuestions",
                newName: "TestSessionQuestion");

            migrationBuilder.RenameIndex(
                name: "IX_TestSessions_EId",
                table: "TestSession",
                newName: "IX_TestSession_EId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSessions_DId",
                table: "TestSession",
                newName: "IX_TestSession_DId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSessions_CId",
                table: "TestSession",
                newName: "IX_TestSession_CId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSessions_BId",
                table: "TestSession",
                newName: "IX_TestSession_BId");

            migrationBuilder.RenameIndex(
                name: "IX_TestSessions_AId",
                table: "TestSession",
                newName: "IX_TestSession_AId");

            migrationBuilder.RenameIndex(
                name: "IX_SessionQuestions_UnderTestId",
                table: "TestSessionQuestion",
                newName: "IX_TestSessionQuestion_UnderTestId");

            migrationBuilder.RenameIndex(
                name: "IX_SessionQuestions_SessionId",
                table: "TestSessionQuestion",
                newName: "IX_TestSessionQuestion_SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_SessionQuestions_False3Id",
                table: "TestSessionQuestion",
                newName: "IX_TestSessionQuestion_False3Id");

            migrationBuilder.RenameIndex(
                name: "IX_SessionQuestions_False2Id",
                table: "TestSessionQuestion",
                newName: "IX_TestSessionQuestion_False2Id");

            migrationBuilder.RenameIndex(
                name: "IX_SessionQuestions_False1Id",
                table: "TestSessionQuestion",
                newName: "IX_TestSessionQuestion_False1Id");

            migrationBuilder.RenameIndex(
                name: "IX_SessionQuestions_CorrectAnswerId",
                table: "TestSessionQuestion",
                newName: "IX_TestSessionQuestion_CorrectAnswerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestSession",
                table: "TestSession",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestSessionQuestion",
                table: "TestSessionQuestion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestSession_StimulusGroups_AId",
                table: "TestSession",
                column: "AId",
                principalTable: "StimulusGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSession_StimulusGroups_BId",
                table: "TestSession",
                column: "BId",
                principalTable: "StimulusGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSession_StimulusGroups_CId",
                table: "TestSession",
                column: "CId",
                principalTable: "StimulusGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSession_StimulusGroups_DId",
                table: "TestSession",
                column: "DId",
                principalTable: "StimulusGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSession_StimulusGroups_EId",
                table: "TestSession",
                column: "EId",
                principalTable: "StimulusGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessionQuestion_Stimuli_CorrectAnswerId",
                table: "TestSessionQuestion",
                column: "CorrectAnswerId",
                principalTable: "Stimuli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessionQuestion_Stimuli_False1Id",
                table: "TestSessionQuestion",
                column: "False1Id",
                principalTable: "Stimuli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessionQuestion_Stimuli_False2Id",
                table: "TestSessionQuestion",
                column: "False2Id",
                principalTable: "Stimuli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessionQuestion_Stimuli_False3Id",
                table: "TestSessionQuestion",
                column: "False3Id",
                principalTable: "Stimuli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessionQuestion_Stimuli_UnderTestId",
                table: "TestSessionQuestion",
                column: "UnderTestId",
                principalTable: "Stimuli",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestSessionQuestion_TestSession_SessionId",
                table: "TestSessionQuestion",
                column: "SessionId",
                principalTable: "TestSession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
