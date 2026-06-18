using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrightPath.Migrations
{
    /// <inheritdoc />
    public partial class yrtytrekl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Grade",
                newName: "GradeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GradeId",
                table: "Grade",
                newName: "id");
        }
    }
}
