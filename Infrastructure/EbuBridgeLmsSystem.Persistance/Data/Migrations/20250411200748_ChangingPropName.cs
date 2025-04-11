using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangingPropName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnrolled",
                table: "students");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnrolledInAnyCourse",
                table: "students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnrolledInAnyCourse",
                table: "students");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnrolled",
                table: "students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
