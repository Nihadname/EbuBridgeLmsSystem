using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChanginRelationsOfLessonUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lessonsMaterial");

            migrationBuilder.DropTable(
                name: "lessonsVideo");

            migrationBuilder.CreateTable(
                name: "lessonUnitMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessonUnitMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lessonUnitMaterials_lessonUnits_LessonUnitId",
                        column: x => x.LessonUnitId,
                        principalTable: "lessonUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lessonUnitVideos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonUnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessonUnitVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lessonUnitVideos_lessonUnits_LessonUnitId",
                        column: x => x.LessonUnitId,
                        principalTable: "lessonUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lessonUnitMaterials_CreatedTime",
                table: "lessonUnitMaterials",
                column: "CreatedTime");

            migrationBuilder.CreateIndex(
                name: "IX_lessonUnitMaterials_LessonUnitId",
                table: "lessonUnitMaterials",
                column: "LessonUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_lessonUnitVideos_CreatedTime",
                table: "lessonUnitVideos",
                column: "CreatedTime");

            migrationBuilder.CreateIndex(
                name: "IX_lessonUnitVideos_LessonUnitId",
                table: "lessonUnitVideos",
                column: "LessonUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lessonUnitMaterials");

            migrationBuilder.DropTable(
                name: "lessonUnitVideos");

            migrationBuilder.CreateTable(
                name: "lessonsMaterial",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Title = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessonsMaterial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lessonsMaterial_lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lessonsVideo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lessonsVideo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_lessonsVideo_lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lessonsMaterial_CreatedTime",
                table: "lessonsMaterial",
                column: "CreatedTime");

            migrationBuilder.CreateIndex(
                name: "IX_lessonsMaterial_LessonId",
                table: "lessonsMaterial",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_lessonsVideo_CreatedTime",
                table: "lessonsVideo",
                column: "CreatedTime");

            migrationBuilder.CreateIndex(
                name: "IX_lessonsVideo_LessonId",
                table: "lessonsVideo",
                column: "LessonId");
        }
    }
}
