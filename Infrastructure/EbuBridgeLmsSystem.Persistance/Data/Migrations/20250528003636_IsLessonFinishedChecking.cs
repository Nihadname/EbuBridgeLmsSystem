using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsLessonFinishedChecking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isLessonFinished",
                table: "lessonUnitAssignments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isLessonFinished",
                table: "lessonUnitAssignments");
        }
    }
}
