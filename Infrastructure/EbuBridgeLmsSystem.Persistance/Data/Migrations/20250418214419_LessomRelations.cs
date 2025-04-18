using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class LessomRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lessonUnitAttendances_lessonUnits_LessonUnitId",
                table: "lessonUnitAttendances");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTime",
                table: "lessonUnitAttendances",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LessonUnitId",
                table: "lessonUnitAttendances",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "lessonUnitAttendances",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "lessonUnitAttendances",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "lessonUnitAssignmentId",
                table: "lessonUnitAttendances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "lessonUnitAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduledStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessonUnitAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lessonUnitAssignments_lessonUnits_LessonUnitId",
                        column: x => x.LessonUnitId,
                        principalTable: "lessonUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_lessonUnitAssignments_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lessonUnitAttendances_lessonUnitAssignmentId",
                table: "lessonUnitAttendances",
                column: "lessonUnitAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_lessonUnitAssignments_LessonUnitId",
                table: "lessonUnitAssignments",
                column: "LessonUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_lessonUnitAssignments_StudentId",
                table: "lessonUnitAssignments",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_lessonUnitAttendances_lessonUnitAssignments_lessonUnitAssignmentId",
                table: "lessonUnitAttendances",
                column: "lessonUnitAssignmentId",
                principalTable: "lessonUnitAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_lessonUnitAttendances_lessonUnits_LessonUnitId",
                table: "lessonUnitAttendances",
                column: "LessonUnitId",
                principalTable: "lessonUnits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lessonUnitAttendances_lessonUnitAssignments_lessonUnitAssignmentId",
                table: "lessonUnitAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_lessonUnitAttendances_lessonUnits_LessonUnitId",
                table: "lessonUnitAttendances");

            migrationBuilder.DropTable(
                name: "lessonUnitAssignments");

            migrationBuilder.DropIndex(
                name: "IX_lessonUnitAttendances_lessonUnitAssignmentId",
                table: "lessonUnitAttendances");

            migrationBuilder.DropColumn(
                name: "lessonUnitAssignmentId",
                table: "lessonUnitAttendances");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedTime",
                table: "lessonUnitAttendances",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<Guid>(
                name: "LessonUnitId",
                table: "lessonUnitAttendances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "lessonUnitAttendances",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "lessonUnitAttendances",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AddForeignKey(
                name: "FK_lessonUnitAttendances_lessonUnits_LessonUnitId",
                table: "lessonUnitAttendances",
                column: "LessonUnitId",
                principalTable: "lessonUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
