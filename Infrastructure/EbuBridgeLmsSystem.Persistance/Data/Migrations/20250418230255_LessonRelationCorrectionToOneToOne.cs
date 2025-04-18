using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class LessonRelationCorrectionToOneToOne : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lessonUnitAttendances_lessonsStudents_LessonStudentLessonId_LessonStudentStudentId",
                table: "lessonUnitAttendances");

            migrationBuilder.DropIndex(
                name: "IX_lessonUnitAttendances_lessonUnitAssignmentId",
                table: "lessonUnitAttendances");

            migrationBuilder.DropColumn(
                name: "LessonStudentId",
                table: "lessonUnitAttendances");

            migrationBuilder.AlterColumn<Guid>(
                name: "LessonStudentStudentId",
                table: "lessonUnitAttendances",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "LessonStudentLessonId",
                table: "lessonUnitAttendances",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_lessonUnitAttendances_lessonUnitAssignmentId",
                table: "lessonUnitAttendances",
                column: "lessonUnitAssignmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_lessonUnitAttendances_lessonsStudents_LessonStudentLessonId_LessonStudentStudentId",
                table: "lessonUnitAttendances",
                columns: new[] { "LessonStudentLessonId", "LessonStudentStudentId" },
                principalTable: "lessonsStudents",
                principalColumns: new[] { "LessonId", "StudentId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lessonUnitAttendances_lessonsStudents_LessonStudentLessonId_LessonStudentStudentId",
                table: "lessonUnitAttendances");

            migrationBuilder.DropIndex(
                name: "IX_lessonUnitAttendances_lessonUnitAssignmentId",
                table: "lessonUnitAttendances");

            migrationBuilder.AlterColumn<Guid>(
                name: "LessonStudentStudentId",
                table: "lessonUnitAttendances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LessonStudentLessonId",
                table: "lessonUnitAttendances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LessonStudentId",
                table: "lessonUnitAttendances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_lessonUnitAttendances_lessonUnitAssignmentId",
                table: "lessonUnitAttendances",
                column: "lessonUnitAssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_lessonUnitAttendances_lessonsStudents_LessonStudentLessonId_LessonStudentStudentId",
                table: "lessonUnitAttendances",
                columns: new[] { "LessonStudentLessonId", "LessonStudentStudentId" },
                principalTable: "lessonsStudents",
                principalColumns: new[] { "LessonId", "StudentId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
