using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class LessonRelationCorrectionAndAdditions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lessonHomeworkLinks_lessonHomeworks_LessonHomeworkId",
                table: "lessonHomeworkLinks");

            migrationBuilder.DropTable(
                name: "LessonUnitHomeworkSubmission");

            migrationBuilder.DropTable(
                name: "lessonHomeworks");

            migrationBuilder.RenameColumn(
                name: "LessonHomeworkId",
                table: "lessonHomeworkLinks",
                newName: "LessonUnitStudentHomeworkId");

            migrationBuilder.RenameIndex(
                name: "IX_lessonHomeworkLinks_LessonHomeworkId",
                table: "lessonHomeworkLinks",
                newName: "IX_lessonHomeworkLinks_LessonUnitStudentHomeworkId");

            migrationBuilder.CreateTable(
                name: "lessonUnitStudentHomeworks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LessonUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessonUnitStudentHomeworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lessonUnitStudentHomeworks_lessonUnits_LessonUnitId",
                        column: x => x.LessonUnitId,
                        principalTable: "lessonUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_lessonUnitStudentHomeworks_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "lessonUnitStudentHomeworkMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonUnitStudentHomeworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessonUnitStudentHomeworkMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lessonUnitStudentHomeworkMaterials_lessonUnitStudentHomeworks_LessonUnitStudentHomeworkId",
                        column: x => x.LessonUnitStudentHomeworkId,
                        principalTable: "lessonUnitStudentHomeworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LessonUnitStudentHomeworkSubmission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonUnitStudentHomeworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_LessonUnitStudentHomeworkSubmission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonUnitStudentHomeworkSubmission_lessonUnitStudentHomeworks_LessonUnitStudentHomeworkId",
                        column: x => x.LessonUnitStudentHomeworkId,
                        principalTable: "lessonUnitStudentHomeworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LessonUnitStudentHomeworkSubmission_lessonUnits_LessonUnitId",
                        column: x => x.LessonUnitId,
                        principalTable: "lessonUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_lessonUnitStudentHomeworkMaterials_LessonUnitStudentHomeworkId",
                table: "lessonUnitStudentHomeworkMaterials",
                column: "LessonUnitStudentHomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_lessonUnitStudentHomeworks_LessonUnitId",
                table: "lessonUnitStudentHomeworks",
                column: "LessonUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_lessonUnitStudentHomeworks_StudentId",
                table: "lessonUnitStudentHomeworks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUnitStudentHomeworkSubmission_LessonUnitId",
                table: "LessonUnitStudentHomeworkSubmission",
                column: "LessonUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUnitStudentHomeworkSubmission_LessonUnitStudentHomeworkId",
                table: "LessonUnitStudentHomeworkSubmission",
                column: "LessonUnitStudentHomeworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_lessonHomeworkLinks_lessonUnitStudentHomeworks_LessonUnitStudentHomeworkId",
                table: "lessonHomeworkLinks",
                column: "LessonUnitStudentHomeworkId",
                principalTable: "lessonUnitStudentHomeworks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lessonHomeworkLinks_lessonUnitStudentHomeworks_LessonUnitStudentHomeworkId",
                table: "lessonHomeworkLinks");

            migrationBuilder.DropTable(
                name: "lessonUnitStudentHomeworkMaterials");

            migrationBuilder.DropTable(
                name: "LessonUnitStudentHomeworkSubmission");

            migrationBuilder.DropTable(
                name: "lessonUnitStudentHomeworks");

            migrationBuilder.RenameColumn(
                name: "LessonUnitStudentHomeworkId",
                table: "lessonHomeworkLinks",
                newName: "LessonHomeworkId");

            migrationBuilder.RenameIndex(
                name: "IX_lessonHomeworkLinks_LessonUnitStudentHomeworkId",
                table: "lessonHomeworkLinks",
                newName: "IX_lessonHomeworkLinks_LessonHomeworkId");

            migrationBuilder.CreateTable(
                name: "lessonHomeworks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessonHomeworks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LessonUnitHomeworkSubmission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonHomeworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LessonUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubmittedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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

            migrationBuilder.AddForeignKey(
                name: "FK_lessonHomeworkLinks_lessonHomeworks_LessonHomeworkId",
                table: "lessonHomeworkLinks",
                column: "LessonHomeworkId",
                principalTable: "lessonHomeworks",
                principalColumn: "Id");
        }
    }
}
