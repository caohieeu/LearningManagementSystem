using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningManagementSystem.Migrations
{
    public partial class updateDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionExamId",
                table: "AnswerExams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AnswerExams_QuestionExamId",
                table: "AnswerExams",
                column: "QuestionExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerExams_QuestionExams_QuestionExamId",
                table: "AnswerExams",
                column: "QuestionExamId",
                principalTable: "QuestionExams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerExams_QuestionExams_QuestionExamId",
                table: "AnswerExams");

            migrationBuilder.DropIndex(
                name: "IX_AnswerExams_QuestionExamId",
                table: "AnswerExams");

            migrationBuilder.DropColumn(
                name: "QuestionExamId",
                table: "AnswerExams");
        }
    }
}
