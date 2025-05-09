using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionAndSaasStudentAdding2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscribtions_SaasStudents_SaasStudentId",
                table: "Subscribtions");

            migrationBuilder.DropIndex(
                name: "IX_Subscribtions_SaasStudentId",
                table: "Subscribtions");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Subscribtions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Subscribtions");

            migrationBuilder.DropColumn(
                name: "IsTrialAvailable",
                table: "Subscribtions");

            migrationBuilder.DropColumn(
                name: "SaasStudentId",
                table: "Subscribtions");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Subscribtions");

            migrationBuilder.DropColumn(
                name: "TrialDays",
                table: "Subscribtions");

            migrationBuilder.CreateTable(
                name: "SaasStudentSubscribtions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SaasStudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscribtionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsTrialAvailable = table.Column<bool>(type: "bit", nullable: false),
                    TrialDays = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaasStudentSubscribtions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaasStudentSubscribtions_SaasStudents_SaasStudentId",
                        column: x => x.SaasStudentId,
                        principalTable: "SaasStudents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaasStudentSubscribtions_Subscribtions_SubscribtionId",
                        column: x => x.SubscribtionId,
                        principalTable: "Subscribtions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaasStudentSubscribtions_SaasStudentId",
                table: "SaasStudentSubscribtions",
                column: "SaasStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SaasStudentSubscribtions_SubscribtionId",
                table: "SaasStudentSubscribtions",
                column: "SubscribtionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaasStudentSubscribtions");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "EndDate",
                table: "Subscribtions",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Subscribtions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTrialAvailable",
                table: "Subscribtions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "SaasStudentId",
                table: "Subscribtions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "StartDate",
                table: "Subscribtions",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "TrialDays",
                table: "Subscribtions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subscribtions_SaasStudentId",
                table: "Subscribtions",
                column: "SaasStudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscribtions_SaasStudents_SaasStudentId",
                table: "Subscribtions",
                column: "SaasStudentId",
                principalTable: "SaasStudents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
