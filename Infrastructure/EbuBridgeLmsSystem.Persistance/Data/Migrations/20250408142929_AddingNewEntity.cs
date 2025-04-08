using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lessonUnitAttendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonStudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonStudentLessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonStudentStudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttendanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessonUnitAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lessonUnitAttendances_lessonUnits_LessonUnitId",
                        column: x => x.LessonUnitId,
                        principalTable: "lessonUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lessonUnitAttendances_lessonsStudents_LessonStudentLessonId_LessonStudentStudentId",
                        columns: x => new { x.LessonStudentLessonId, x.LessonStudentStudentId },
                        principalTable: "lessonsStudents",
                        principalColumns: new[] { "LessonId", "StudentId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lessonUnitAttendances_LessonStudentLessonId_LessonStudentStudentId",
                table: "lessonUnitAttendances",
                columns: new[] { "LessonStudentLessonId", "LessonStudentStudentId" });

            migrationBuilder.CreateIndex(
                name: "IX_lessonUnitAttendances_LessonUnitId",
                table: "lessonUnitAttendances",
                column: "LessonUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lessonUnitAttendances");
        }
    }
}
