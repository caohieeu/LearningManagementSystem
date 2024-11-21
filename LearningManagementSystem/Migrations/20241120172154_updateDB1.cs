using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningManagementSystem.Migrations
{
    public partial class updateDB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerExams_QuestionExams_QuestionExamId",
                table: "AnswerExams");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionExams_Examinations_ExaminationId",
                table: "QuestionExams");

            migrationBuilder.DropIndex(
                name: "IX_QuestionExams_ExaminationId",
                table: "QuestionExams");

            migrationBuilder.DropIndex(
                name: "IX_AnswerExams_QuestionExamId",
                table: "AnswerExams");

            migrationBuilder.DropColumn(
                name: "QuestionExamId",
                table: "AnswerExams");

            migrationBuilder.RenameColumn(
                name: "ExaminationId",
                table: "QuestionExams",
                newName: "Type");

            migrationBuilder.CreateTable(
                name: "ExaminationQuestions",
                columns: table => new
                {
                    ExaminationId = table.Column<int>(type: "int", nullable: false),
                    QuestionExamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationQuestions", x => new { x.ExaminationId, x.QuestionExamId });
                    table.ForeignKey(
                        name: "FK_ExaminationQuestions_Examinations_ExaminationId",
                        column: x => x.ExaminationId,
                        principalTable: "Examinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExaminationQuestions_QuestionExams_QuestionExamId",
                        column: x => x.QuestionExamId,
                        principalTable: "QuestionExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationQuestions_QuestionExamId",
                table: "ExaminationQuestions",
                column: "QuestionExamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationQuestions");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "QuestionExams",
                newName: "ExaminationId");

            migrationBuilder.AddColumn<int>(
                name: "QuestionExamId",
                table: "AnswerExams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionExams_ExaminationId",
                table: "QuestionExams",
                column: "ExaminationId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionExams_Examinations_ExaminationId",
                table: "QuestionExams",
                column: "ExaminationId",
                principalTable: "Examinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
