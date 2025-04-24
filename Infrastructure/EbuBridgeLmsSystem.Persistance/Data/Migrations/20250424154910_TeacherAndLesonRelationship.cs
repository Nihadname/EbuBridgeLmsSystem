using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class TeacherAndLesonRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lessonUnitAttendances_lessonsStudents_LessonStudentLessonId_LessonStudentStudentId",
                table: "lessonUnitAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_quizResults_lessonsStudents_LessonStudentLessonId_LessonStudentStudentId",
                table: "quizResults");

            migrationBuilder.DropTable(
                name: "TeacherStudent");

            migrationBuilder.RenameColumn(
                name: "LessonStudentStudentId",
                table: "quizResults",
                newName: "LessonStudentTeacherStudentId");

            migrationBuilder.RenameColumn(
                name: "LessonStudentLessonId",
                table: "quizResults",
                newName: "LessonStudentTeacherLessonId");

            migrationBuilder.RenameIndex(
                name: "IX_quizResults_LessonStudentLessonId_LessonStudentStudentId",
                table: "quizResults",
                newName: "IX_quizResults_LessonStudentTeacherLessonId_LessonStudentTeacherStudentId");

            migrationBuilder.RenameColumn(
                name: "LessonStudentStudentId",
                table: "lessonUnitAttendances",
                newName: "LessonStudentTeacherStudentId");

            migrationBuilder.RenameColumn(
                name: "LessonStudentLessonId",
                table: "lessonUnitAttendances",
                newName: "LessonStudentTeacherLessonId");

            migrationBuilder.RenameIndex(
                name: "IX_lessonUnitAttendances_LessonStudentLessonId_LessonStudentStudentId",
                table: "lessonUnitAttendances",
                newName: "IX_lessonUnitAttendances_LessonStudentTeacherLessonId_LessonStudentTeacherStudentId");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "lessonsStudents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_lessonsStudents_TeacherId",
                table: "lessonsStudents",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_lessonsStudents_teachers_TeacherId",
                table: "lessonsStudents",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_lessonUnitAttendances_lessonsStudents_LessonStudentTeacherLessonId_LessonStudentTeacherStudentId",
                table: "lessonUnitAttendances",
                columns: new[] { "LessonStudentTeacherLessonId", "LessonStudentTeacherStudentId" },
                principalTable: "lessonsStudents",
                principalColumns: new[] { "LessonId", "StudentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_quizResults_lessonsStudents_LessonStudentTeacherLessonId_LessonStudentTeacherStudentId",
                table: "quizResults",
                columns: new[] { "LessonStudentTeacherLessonId", "LessonStudentTeacherStudentId" },
                principalTable: "lessonsStudents",
                principalColumns: new[] { "LessonId", "StudentId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lessonsStudents_teachers_TeacherId",
                table: "lessonsStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_lessonUnitAttendances_lessonsStudents_LessonStudentTeacherLessonId_LessonStudentTeacherStudentId",
                table: "lessonUnitAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_quizResults_lessonsStudents_LessonStudentTeacherLessonId_LessonStudentTeacherStudentId",
                table: "quizResults");

            migrationBuilder.DropIndex(
                name: "IX_lessonsStudents_TeacherId",
                table: "lessonsStudents");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "lessonsStudents");

            migrationBuilder.RenameColumn(
                name: "LessonStudentTeacherStudentId",
                table: "quizResults",
                newName: "LessonStudentStudentId");

            migrationBuilder.RenameColumn(
                name: "LessonStudentTeacherLessonId",
                table: "quizResults",
                newName: "LessonStudentLessonId");

            migrationBuilder.RenameIndex(
                name: "IX_quizResults_LessonStudentTeacherLessonId_LessonStudentTeacherStudentId",
                table: "quizResults",
                newName: "IX_quizResults_LessonStudentLessonId_LessonStudentStudentId");

            migrationBuilder.RenameColumn(
                name: "LessonStudentTeacherStudentId",
                table: "lessonUnitAttendances",
                newName: "LessonStudentStudentId");

            migrationBuilder.RenameColumn(
                name: "LessonStudentTeacherLessonId",
                table: "lessonUnitAttendances",
                newName: "LessonStudentLessonId");

            migrationBuilder.RenameIndex(
                name: "IX_lessonUnitAttendances_LessonStudentTeacherLessonId_LessonStudentTeacherStudentId",
                table: "lessonUnitAttendances",
                newName: "IX_lessonUnitAttendances_LessonStudentLessonId_LessonStudentStudentId");

            migrationBuilder.CreateTable(
                name: "TeacherStudent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherStudent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherStudent_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherStudent_teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStudent_StudentId",
                table: "TeacherStudent",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStudent_TeacherId",
                table: "TeacherStudent",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_lessonUnitAttendances_lessonsStudents_LessonStudentLessonId_LessonStudentStudentId",
                table: "lessonUnitAttendances",
                columns: new[] { "LessonStudentLessonId", "LessonStudentStudentId" },
                principalTable: "lessonsStudents",
                principalColumns: new[] { "LessonId", "StudentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_quizResults_lessonsStudents_LessonStudentLessonId_LessonStudentStudentId",
                table: "quizResults",
                columns: new[] { "LessonStudentLessonId", "LessonStudentStudentId" },
                principalTable: "lessonsStudents",
                principalColumns: new[] { "LessonId", "StudentId" });
        }
    }
}
