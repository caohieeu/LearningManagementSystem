using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningManagementSystem.Migrations
{
    public partial class updateField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Lesssions_LessionId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentLessions_Lesssions_LessionId",
                table: "DocumentLessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesssions_Titles_TitleId",
                table: "Lesssions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lesssions",
                table: "Lesssions");

            migrationBuilder.RenameTable(
                name: "Lesssions",
                newName: "Lession");

            migrationBuilder.RenameColumn(
                name: "LessionId",
                table: "Classes",
                newName: "TitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Classes_LessionId",
                table: "Classes",
                newName: "IX_Classes_TitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Lesssions_TitleId",
                table: "Lession",
                newName: "IX_Lession_TitleId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Titles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lession",
                table: "Lession",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FileDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDetails", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Titles_TitleId",
                table: "Classes",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentLessions_Lession_LessionId",
                table: "DocumentLessions",
                column: "LessionId",
                principalTable: "Lession",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Titles_TitleId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentLessions_Lession_LessionId",
                table: "DocumentLessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Lession_Titles_TitleId",
                table: "Lession");

            migrationBuilder.DropTable(
                name: "FileDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lession",
                table: "Lession");

            migrationBuilder.RenameTable(
                name: "Lession",
                newName: "Lesssions");

            migrationBuilder.RenameColumn(
                name: "TitleId",
                table: "Classes",
                newName: "LessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Classes_TitleId",
                table: "Classes",
                newName: "IX_Classes_LessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Lession_TitleId",
                table: "Lesssions",
                newName: "IX_Lesssions_TitleId");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Titles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lesssions",
                table: "Lesssions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Lesssions_LessionId",
                table: "Classes",
                column: "LessionId",
                principalTable: "Lesssions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentLessions_Lesssions_LessionId",
                table: "DocumentLessions",
                column: "LessionId",
                principalTable: "Lesssions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesssions_Titles_TitleId",
                table: "Lesssions",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
