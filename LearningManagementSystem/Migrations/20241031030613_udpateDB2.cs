using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningManagementSystem.Migrations
{
    public partial class udpateDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentLessions_Lession_LessionId",
                table: "DocumentLessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Lession_Classes_ClassId",
                table: "Lession");

            migrationBuilder.DropForeignKey(
                name: "FK_Lession_Titles_TitleId",
                table: "Lession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lession",
                table: "Lession");

            migrationBuilder.RenameTable(
                name: "Lession",
                newName: "Lessions");

            migrationBuilder.RenameIndex(
                name: "IX_Lession_TitleId",
                table: "Lessions",
                newName: "IX_Lessions_TitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Lession_ClassId",
                table: "Lessions",
                newName: "IX_Lessions_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessions",
                table: "Lessions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentLessions_Lessions_LessionId",
                table: "DocumentLessions",
                column: "LessionId",
                principalTable: "Lessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessions_Classes_ClassId",
                table: "Lessions",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessions_Titles_TitleId",
                table: "Lessions",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentLessions_Lessions_LessionId",
                table: "DocumentLessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessions_Classes_ClassId",
                table: "Lessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessions_Titles_TitleId",
                table: "Lessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessions",
                table: "Lessions");

            migrationBuilder.RenameTable(
                name: "Lessions",
                newName: "Lession");

            migrationBuilder.RenameIndex(
                name: "IX_Lessions_TitleId",
                table: "Lession",
                newName: "IX_Lession_TitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessions_ClassId",
                table: "Lession",
                newName: "IX_Lession_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lession",
                table: "Lession",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentLessions_Lession_LessionId",
                table: "DocumentLessions",
                column: "LessionId",
                principalTable: "Lession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lession_Classes_ClassId",
                table: "Lession",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lession_Titles_TitleId",
                table: "Lession",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
