using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class LessonRelationCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lessonUnitAttendances_lessonUnits_LessonUnitId",
                table: "lessonUnitAttendances");

            migrationBuilder.DropIndex(
                name: "IX_lessonUnitAttendances_LessonUnitId",
                table: "lessonUnitAttendances");

            migrationBuilder.DropColumn(
                name: "LessonUnitId",
                table: "lessonUnitAttendances");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LessonUnitId",
                table: "lessonUnitAttendances",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_lessonUnitAttendances_LessonUnitId",
                table: "lessonUnitAttendances",
                column: "LessonUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_lessonUnitAttendances_lessonUnits_LessonUnitId",
                table: "lessonUnitAttendances",
                column: "LessonUnitId",
                principalTable: "lessonUnits",
                principalColumn: "Id");
        }
    }
}
