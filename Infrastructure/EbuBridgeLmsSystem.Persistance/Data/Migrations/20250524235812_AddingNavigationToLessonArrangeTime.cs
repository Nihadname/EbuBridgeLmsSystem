using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingNavigationToLessonArrangeTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "lessonUnitAssignments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_lessonUnitAssignments_TeacherId",
                table: "lessonUnitAssignments",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_lessonUnitAssignments_teachers_TeacherId",
                table: "lessonUnitAssignments",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lessonUnitAssignments_teachers_TeacherId",
                table: "lessonUnitAssignments");

            migrationBuilder.DropIndex(
                name: "IX_lessonUnitAssignments_TeacherId",
                table: "lessonUnitAssignments");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "lessonUnitAssignments");
        }
    }
}
