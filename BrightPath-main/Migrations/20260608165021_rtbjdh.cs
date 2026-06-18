using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrightPath.Migrations
{
    /// <inheritdoc />
    public partial class rtbjdh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Progress",
                table: "courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "lessonProgresses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isCopmleted = table.Column<bool>(type: "bit", nullable: false),
                    courseId = table.Column<int>(type: "int", nullable: false),
                    lessonId = table.Column<int>(type: "int", nullable: false),
                    courseId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessonProgresses", x => x.id);
                    table.ForeignKey(
                        name: "FK_lessonProgresses_AspNetUsers_studentId",
                        column: x => x.studentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_lessonProgresses_courses_courseId",
                        column: x => x.courseId,
                        principalTable: "courses",
                        principalColumn: "courseId");
                    table.ForeignKey(
                        name: "FK_lessonProgresses_courses_courseId1",
                        column: x => x.courseId1,
                        principalTable: "courses",
                        principalColumn: "courseId");
                    table.ForeignKey(
                        name: "FK_lessonProgresses_lessons_lessonId",
                        column: x => x.lessonId,
                        principalTable: "lessons",
                        principalColumn: "lessonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_lessonProgresses_courseId",
                table: "lessonProgresses",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_lessonProgresses_courseId1",
                table: "lessonProgresses",
                column: "courseId1");

            migrationBuilder.CreateIndex(
                name: "IX_lessonProgresses_lessonId",
                table: "lessonProgresses",
                column: "lessonId");

            migrationBuilder.CreateIndex(
                name: "IX_lessonProgresses_studentId",
                table: "lessonProgresses",
                column: "studentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lessonProgresses");

            migrationBuilder.DropColumn(
                name: "Progress",
                table: "courses");
        }
    }
}
