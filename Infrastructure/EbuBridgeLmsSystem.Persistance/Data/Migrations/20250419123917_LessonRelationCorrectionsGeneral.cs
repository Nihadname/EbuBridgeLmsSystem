using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class LessonRelationCorrectionsGeneral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lessonHomeworkLinks_lessonUnitStudentHomeworks_LessonUnitStudentHomeworkId",
                table: "lessonHomeworkLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_lessonHomeworkLinks_lessonUnits_LessonUnitId",
                table: "lessonHomeworkLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonUnitStudentHomeworkSubmission_lessonUnits_LessonUnitId",
                table: "LessonUnitStudentHomeworkSubmission");

            migrationBuilder.DropIndex(
                name: "IX_LessonUnitStudentHomeworkSubmission_LessonUnitId",
                table: "LessonUnitStudentHomeworkSubmission");

            migrationBuilder.DropIndex(
                name: "IX_lessonHomeworkLinks_LessonUnitId",
                table: "lessonHomeworkLinks");

            migrationBuilder.DropColumn(
                name: "LessonUnitId",
                table: "LessonUnitStudentHomeworkSubmission");

            migrationBuilder.DropColumn(
                name: "LessonSetTime",
                table: "lessonUnits");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "lessons");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "lessons");

            migrationBuilder.DropColumn(
                name: "MeetingLink",
                table: "lessons");

            migrationBuilder.DropColumn(
                name: "ScheduledDate",
                table: "lessons");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "lessons");

            migrationBuilder.DropColumn(
                name: "LessonUnitId",
                table: "lessonHomeworkLinks");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "EnrollmentEndDeadline",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "EnrollmentStartDate",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "difficultyLevel",
                table: "Courses",
                newName: "DifficultyLevel");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "lessonUnitAssignments",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "MeetingLink",
                table: "lessonUnitAssignments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LessonUnitStudentHomeworkId",
                table: "lessonHomeworkLinks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_lessonHomeworkLinks_lessonUnitStudentHomeworks_LessonUnitStudentHomeworkId",
                table: "lessonHomeworkLinks",
                column: "LessonUnitStudentHomeworkId",
                principalTable: "lessonUnitStudentHomeworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lessonHomeworkLinks_lessonUnitStudentHomeworks_LessonUnitStudentHomeworkId",
                table: "lessonHomeworkLinks");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "lessonUnitAssignments");

            migrationBuilder.DropColumn(
                name: "MeetingLink",
                table: "lessonUnitAssignments");

            migrationBuilder.RenameColumn(
                name: "DifficultyLevel",
                table: "Courses",
                newName: "difficultyLevel");

            migrationBuilder.AddColumn<Guid>(
                name: "LessonUnitId",
                table: "LessonUnitStudentHomeworkSubmission",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LessonSetTime",
                table: "lessonUnits",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "lessons",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "lessons",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "MeetingLink",
                table: "lessons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledDate",
                table: "lessons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "lessons",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AlterColumn<Guid>(
                name: "LessonUnitStudentHomeworkId",
                table: "lessonHomeworkLinks",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "LessonUnitId",
                table: "lessonHomeworkLinks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Courses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentEndDeadline",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollmentStartDate",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Courses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonUnitStudentHomeworkSubmission_LessonUnitId",
                table: "LessonUnitStudentHomeworkSubmission",
                column: "LessonUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_lessonHomeworkLinks_LessonUnitId",
                table: "lessonHomeworkLinks",
                column: "LessonUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_lessonHomeworkLinks_lessonUnitStudentHomeworks_LessonUnitStudentHomeworkId",
                table: "lessonHomeworkLinks",
                column: "LessonUnitStudentHomeworkId",
                principalTable: "lessonUnitStudentHomeworks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_lessonHomeworkLinks_lessonUnits_LessonUnitId",
                table: "lessonHomeworkLinks",
                column: "LessonUnitId",
                principalTable: "lessonUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonUnitStudentHomeworkSubmission_lessonUnits_LessonUnitId",
                table: "LessonUnitStudentHomeworkSubmission",
                column: "LessonUnitId",
                principalTable: "lessonUnits",
                principalColumn: "Id");
        }
    }
}
