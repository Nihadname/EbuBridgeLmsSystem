using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class newRelationsForQuizAndArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_quizQuestions_lessonQuizzes_LessonQuizId",
                table: "quizQuestions");

            migrationBuilder.DropTable(
                name: "quizOptions");

            migrationBuilder.DropTable(
                name: "quizResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_quizQuestions",
                table: "quizQuestions");

            migrationBuilder.RenameTable(
                name: "quizQuestions",
                newName: "QuizQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_quizQuestions_LessonQuizId",
                table: "QuizQuestions",
                newName: "IX_QuizQuestions_LessonQuizId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuizQuestions",
                table: "QuizQuestions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizOption",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    QuizQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizOption_QuizQuestions_QuizQuestionId",
                        column: x => x.QuizQuestionId,
                        principalTable: "QuizQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AttemptedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPassed = table.Column<bool>(type: "bit", nullable: false),
                    LessonStudentTeacherLessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LessonStudentTeacherStudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizResult_lessonQuizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "lessonQuizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizResult_lessonsStudents_LessonStudentTeacherLessonId_LessonStudentTeacherStudentId",
                        columns: x => new { x.LessonStudentTeacherLessonId, x.LessonStudentTeacherStudentId },
                        principalTable: "lessonsStudents",
                        principalColumns: new[] { "LessonId", "StudentId" });
                    table.ForeignKey(
                        name: "FK_QuizResult_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quizzes_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserQuizResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Grade = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsFailed = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserQuizResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserQuizResult_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppUserQuizResult_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleQuizQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleQuizQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleQuizQuestion_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestionOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    ArticleQuizQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestionOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizQuestionOptions_ArticleQuizQuestion_ArticleQuizQuestionId",
                        column: x => x.ArticleQuizQuestionId,
                        principalTable: "ArticleQuizQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserQuizResult_AppUserId1",
                table: "AppUserQuizResult",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserQuizResult_QuizId",
                table: "AppUserQuizResult",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleQuizQuestion_QuizId",
                table: "ArticleQuizQuestion",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizOption_QuizQuestionId",
                table: "QuizOption",
                column: "QuizQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestionOptions_ArticleQuizQuestionId",
                table: "QuizQuestionOptions",
                column: "ArticleQuizQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizResult_CreatedTime",
                table: "QuizResult",
                column: "CreatedTime");

            migrationBuilder.CreateIndex(
                name: "IX_QuizResult_LessonStudentTeacherLessonId_LessonStudentTeacherStudentId",
                table: "QuizResult",
                columns: new[] { "LessonStudentTeacherLessonId", "LessonStudentTeacherStudentId" });

            migrationBuilder.CreateIndex(
                name: "IX_QuizResult_QuizId",
                table: "QuizResult",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizResult_StudentId",
                table: "QuizResult",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_ArticleId",
                table: "Quizzes",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestions_lessonQuizzes_LessonQuizId",
                table: "QuizQuestions",
                column: "LessonQuizId",
                principalTable: "lessonQuizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizQuestions_lessonQuizzes_LessonQuizId",
                table: "QuizQuestions");

            migrationBuilder.DropTable(
                name: "AppUserQuizResult");

            migrationBuilder.DropTable(
                name: "QuizOption");

            migrationBuilder.DropTable(
                name: "QuizQuestionOptions");

            migrationBuilder.DropTable(
                name: "QuizResult");

            migrationBuilder.DropTable(
                name: "ArticleQuizQuestion");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuizQuestions",
                table: "QuizQuestions");

            migrationBuilder.RenameTable(
                name: "QuizQuestions",
                newName: "quizQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_QuizQuestions_LessonQuizId",
                table: "quizQuestions",
                newName: "IX_quizQuestions_LessonQuizId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_quizQuestions",
                table: "quizQuestions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "quizOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_quizOptions_quizQuestions_QuizQuestionId",
                        column: x => x.QuizQuestionId,
                        principalTable: "quizQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "quizResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttemptedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsPassed = table.Column<bool>(type: "bit", nullable: false),
                    LessonStudentTeacherLessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LessonStudentTeacherStudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Score = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_quizResults_lessonQuizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "lessonQuizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_quizResults_lessonsStudents_LessonStudentTeacherLessonId_LessonStudentTeacherStudentId",
                        columns: x => new { x.LessonStudentTeacherLessonId, x.LessonStudentTeacherStudentId },
                        principalTable: "lessonsStudents",
                        principalColumns: new[] { "LessonId", "StudentId" });
                    table.ForeignKey(
                        name: "FK_quizResults_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_quizOptions_QuizQuestionId",
                table: "quizOptions",
                column: "QuizQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_quizResults_CreatedTime",
                table: "quizResults",
                column: "CreatedTime");

            migrationBuilder.CreateIndex(
                name: "IX_quizResults_LessonStudentTeacherLessonId_LessonStudentTeacherStudentId",
                table: "quizResults",
                columns: new[] { "LessonStudentTeacherLessonId", "LessonStudentTeacherStudentId" });

            migrationBuilder.CreateIndex(
                name: "IX_quizResults_QuizId",
                table: "quizResults",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_quizResults_StudentId",
                table: "quizResults",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_quizQuestions_lessonQuizzes_LessonQuizId",
                table: "quizQuestions",
                column: "LessonQuizId",
                principalTable: "lessonQuizzes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
