using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrightPath.Migrations
{
    /// <inheritdoc />
    public partial class hhreedf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "teacherName",
                table: "courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "teacherName",
                table: "courses");
        }
    }
}
