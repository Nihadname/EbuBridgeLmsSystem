using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeletingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lessonUnitAttendances");

            migrationBuilder.DropColumn(
                name: "Attended",
                table: "lessonsStudents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Attended",
                table: "lessonsStudents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "lessonUnitAttendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonStudentLessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonUnitId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LessonStudentStudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttendanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false),
                    LessonStudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonUnitId = table.Column<int>(type: "int", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessonUnitAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lessonUnitAttendances_lessonUnits_LessonUnitId1",
                        column: x => x.LessonUnitId1,
                        principalTable: "lessonUnits",
                        principalColumn: "Id");
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
                name: "IX_lessonUnitAttendances_LessonUnitId1",
                table: "lessonUnitAttendances",
                column: "LessonUnitId1");
        }
    }
}
