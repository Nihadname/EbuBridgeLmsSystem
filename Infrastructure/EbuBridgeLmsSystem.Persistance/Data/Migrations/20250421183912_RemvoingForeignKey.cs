using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemvoingForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lessons_teachers_TeacherId",
                table: "lessons");

            migrationBuilder.DropIndex(
                name: "IX_lessons_TeacherId",
                table: "lessons");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "lessons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "lessons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_lessons_TeacherId",
                table: "lessons",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_lessons_teachers_TeacherId",
                table: "lessons",
                column: "TeacherId",
                principalTable: "teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
