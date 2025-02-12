using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_users_UserId",
                table: "addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_notes_users_UserId",
                table: "notes");

            migrationBuilder.DropForeignKey(
                name: "FK_parents_users_UserId",
                table: "parents");

            migrationBuilder.DropForeignKey(
                name: "FK_reports_users_ReportedUserId",
                table: "reports");

            migrationBuilder.DropForeignKey(
                name: "FK_reports_users_UserId",
                table: "reports");

            migrationBuilder.DropForeignKey(
                name: "FK_students_users_UserId",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_teachers_users_UserId",
                table: "teachers");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropIndex(
                name: "IX_teachers_UserId",
                table: "teachers");

            migrationBuilder.DropIndex(
                name: "IX_students_UserId",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_reports_ReportedUserId",
                table: "reports");

            migrationBuilder.DropIndex(
                name: "IX_reports_UserId",
                table: "reports");

            migrationBuilder.DropIndex(
                name: "IX_parents_UserId",
                table: "parents");

            migrationBuilder.DropIndex(
                name: "IX_notes_UserId",
                table: "notes");

            migrationBuilder.DropIndex(
                name: "IX_addresses_UserId",
                table: "addresses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "ReportedUserId",
                table: "reports");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "reports");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "parents");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "addresses");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "teachers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "students",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "reports",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReportedAppUserId",
                table: "reports",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "parents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "notes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "BlockedUntil",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmailVerificationCodeValid",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstTimeLogined",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReportedHighly",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "AspNetUsers",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fullName",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "addresses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_teachers_AppUserId",
                table: "teachers",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_students_AppUserId",
                table: "students",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_reports_AppUserId",
                table: "reports",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_reports_ReportedAppUserId",
                table: "reports",
                column: "ReportedAppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_parents_AppUserId",
                table: "parents",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_notes_AppUserId",
                table: "notes",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CreatedTime",
                table: "AspNetUsers",
                column: "CreatedTime");

            migrationBuilder.AddCheckConstraint(
                name: "CK_User_MinimumAge",
                table: "AspNetUsers",
                sql: "DATEDIFF(YEAR, BirthDate, GETDATE()) >= 15");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_AppUserId",
                table: "addresses",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_AspNetUsers_AppUserId",
                table: "addresses",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_notes_AspNetUsers_AppUserId",
                table: "notes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_parents_AspNetUsers_AppUserId",
                table: "parents",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reports_AspNetUsers_AppUserId",
                table: "reports",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_reports_AspNetUsers_ReportedAppUserId",
                table: "reports",
                column: "ReportedAppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_students_AspNetUsers_AppUserId",
                table: "students",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_AspNetUsers_AppUserId",
                table: "teachers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_AspNetUsers_AppUserId",
                table: "addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_notes_AspNetUsers_AppUserId",
                table: "notes");

            migrationBuilder.DropForeignKey(
                name: "FK_parents_AspNetUsers_AppUserId",
                table: "parents");

            migrationBuilder.DropForeignKey(
                name: "FK_reports_AspNetUsers_AppUserId",
                table: "reports");

            migrationBuilder.DropForeignKey(
                name: "FK_reports_AspNetUsers_ReportedAppUserId",
                table: "reports");

            migrationBuilder.DropForeignKey(
                name: "FK_students_AspNetUsers_AppUserId",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "FK_teachers_AspNetUsers_AppUserId",
                table: "teachers");

            migrationBuilder.DropIndex(
                name: "IX_teachers_AppUserId",
                table: "teachers");

            migrationBuilder.DropIndex(
                name: "IX_students_AppUserId",
                table: "students");

            migrationBuilder.DropIndex(
                name: "IX_reports_AppUserId",
                table: "reports");

            migrationBuilder.DropIndex(
                name: "IX_reports_ReportedAppUserId",
                table: "reports");

            migrationBuilder.DropIndex(
                name: "IX_parents_AppUserId",
                table: "parents");

            migrationBuilder.DropIndex(
                name: "IX_notes_AppUserId",
                table: "notes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CreatedTime",
                table: "AspNetUsers");

            migrationBuilder.DropCheckConstraint(
                name: "CK_User_MinimumAge",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_addresses_AppUserId",
                table: "addresses");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "teachers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "students");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "reports");

            migrationBuilder.DropColumn(
                name: "ReportedAppUserId",
                table: "reports");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "parents");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "notes");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BlockedUntil",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ExpiredDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsEmailVerificationCodeValid",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsFirstTimeLogined",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsReportedHighly",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "fullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "addresses");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "teachers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReportedUserId",
                table: "reports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "reports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "parents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "notes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "addresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlockedUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsEmailVerificationCodeValid = table.Column<bool>(type: "bit", nullable: false),
                    IsFirstTimeLogined = table.Column<bool>(type: "bit", nullable: false),
                    IsReportedHighly = table.Column<bool>(type: "bit", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VerificationCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    fullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.CheckConstraint("CK_User_MinimumAge", "DATEDIFF(YEAR, BirthDate, GETDATE()) >= 15");
                    table.ForeignKey(
                        name: "FK_users_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_teachers_UserId",
                table: "teachers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_students_UserId",
                table: "students",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reports_ReportedUserId",
                table: "reports",
                column: "ReportedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_reports_UserId",
                table: "reports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_parents_UserId",
                table: "parents",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_notes_UserId",
                table: "notes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_UserId",
                table: "addresses",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_AppUserId",
                table: "users",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_users_CreatedTime",
                table: "users",
                column: "CreatedTime");

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_users_UserId",
                table: "addresses",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_notes_users_UserId",
                table: "notes",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_parents_users_UserId",
                table: "parents",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reports_users_ReportedUserId",
                table: "reports",
                column: "ReportedUserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reports_users_UserId",
                table: "reports",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_students_users_UserId",
                table: "students",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_teachers_users_UserId",
                table: "teachers",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
