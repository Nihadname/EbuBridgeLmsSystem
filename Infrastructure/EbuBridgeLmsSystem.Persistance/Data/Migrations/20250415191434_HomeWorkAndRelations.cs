using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class HomeWorkAndRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lessonHomeworks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessonHomeworks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "lessonHomeworkLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonHomeworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessonHomeworkLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lessonHomeworkLinks_lessonHomeworks_LessonHomeworkId",
                        column: x => x.LessonHomeworkId,
                        principalTable: "lessonHomeworks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_lessonHomeworkLinks_lessonUnits_LessonUnitId",
                        column: x => x.LessonUnitId,
                        principalTable: "lessonUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonUnitHomeworkSubmission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonHomeworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonUnitHomeworkSubmission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonUnitHomeworkSubmission_lessonHomeworks_LessonHomeworkId",
                        column: x => x.LessonHomeworkId,
                        principalTable: "lessonHomeworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonUnitHomeworkSubmission_lessonUnits_LessonUnitId",
                        column: x => x.LessonUnitId,
                        principalTable: "lessonUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LessonUnitHomeworkSubmission_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lessonHomeworkLinks_LessonHomeworkId",
                table: "lessonHomeworkLinks",
                column: "LessonHomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_lessonHomeworkLinks_LessonUnitId",
                table: "lessonHomeworkLinks",
                column: "LessonUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUnitHomeworkSubmission_LessonHomeworkId",
                table: "LessonUnitHomeworkSubmission",
                column: "LessonHomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUnitHomeworkSubmission_LessonUnitId",
                table: "LessonUnitHomeworkSubmission",
                column: "LessonUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUnitHomeworkSubmission_StudentId",
                table: "LessonUnitHomeworkSubmission",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lessonHomeworkLinks");

            migrationBuilder.DropTable(
                name: "LessonUnitHomeworkSubmission");

            migrationBuilder.DropTable(
                name: "lessonHomeworks");
        }
    }
}
