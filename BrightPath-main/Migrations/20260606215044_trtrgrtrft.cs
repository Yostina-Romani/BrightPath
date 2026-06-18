using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrightPath.Migrations
{
    /// <inheritdoc />
    public partial class trtrgrtrft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_Grade_gradeid",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "id",
                table: "courses");

            migrationBuilder.RenameColumn(
                name: "gradeid",
                table: "courses",
                newName: "gradeId");

            migrationBuilder.RenameIndex(
                name: "IX_courses_gradeid",
                table: "courses",
                newName: "IX_courses_gradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_Grade_gradeId",
                table: "courses",
                column: "gradeId",
                principalTable: "Grade",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_Grade_gradeId",
                table: "courses");

            migrationBuilder.RenameColumn(
                name: "gradeId",
                table: "courses",
                newName: "gradeid");

            migrationBuilder.RenameIndex(
                name: "IX_courses_gradeId",
                table: "courses",
                newName: "IX_courses_gradeid");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_courses_Grade_gradeid",
                table: "courses",
                column: "gradeid",
                principalTable: "Grade",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
