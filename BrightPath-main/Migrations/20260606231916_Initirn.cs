using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrightPath.Migrations
{
    /// <inheritdoc />
    public partial class Initirn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Grade_GradeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_courses_Grade_gradeId",
                table: "courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Grade",
                table: "Grade");

            migrationBuilder.RenameTable(
                name: "Grade",
                newName: "grade");

            migrationBuilder.AddPrimaryKey(
                name: "PK_grade",
                table: "grade",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_grade_GradeId",
                table: "AspNetUsers",
                column: "GradeId",
                principalTable: "grade",
                principalColumn: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_grade_gradeId",
                table: "courses",
                column: "gradeId",
                principalTable: "grade",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_grade_GradeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_courses_grade_gradeId",
                table: "courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_grade",
                table: "grade");

            migrationBuilder.RenameTable(
                name: "grade",
                newName: "Grade");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Grade",
                table: "Grade",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Grade_GradeId",
                table: "AspNetUsers",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_Grade_gradeId",
                table: "courses",
                column: "gradeId",
                principalTable: "Grade",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
