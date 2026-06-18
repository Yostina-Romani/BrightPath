using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrightPath.Migrations
{
    /// <inheritdoc />
    public partial class ryrryr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "courseTitle",
                table: "courses");

            migrationBuilder.RenameColumn(
                name: "lessonUrl",
                table: "lessons",
                newName: "lessonVideoUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lessonVideoUrl",
                table: "lessons",
                newName: "lessonUrl");

            migrationBuilder.AddColumn<string>(
                name: "courseTitle",
                table: "courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
