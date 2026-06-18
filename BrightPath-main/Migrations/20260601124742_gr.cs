using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrightPath.Migrations
{
    /// <inheritdoc />
    public partial class gr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentCourses_students_studentId",
                table: "studentCourses");

            migrationBuilder.DropIndex(
                name: "IX_studentCourses_studentId",
                table: "studentCourses");

            migrationBuilder.AlterColumn<string>(
                name: "studentId",
                table: "studentCourses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "studentId1",
                table: "studentCourses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_studentCourses_studentId1",
                table: "studentCourses",
                column: "studentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_studentCourses_students_studentId1",
                table: "studentCourses",
                column: "studentId1",
                principalTable: "students",
                principalColumn: "studentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_studentCourses_students_studentId1",
                table: "studentCourses");

            migrationBuilder.DropIndex(
                name: "IX_studentCourses_studentId1",
                table: "studentCourses");

            migrationBuilder.DropColumn(
                name: "studentId1",
                table: "studentCourses");

            migrationBuilder.AlterColumn<int>(
                name: "studentId",
                table: "studentCourses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_studentCourses_studentId",
                table: "studentCourses",
                column: "studentId");

            migrationBuilder.AddForeignKey(
                name: "FK_studentCourses_students_studentId",
                table: "studentCourses",
                column: "studentId",
                principalTable: "students",
                principalColumn: "studentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
