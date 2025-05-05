using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EbuBridgeLmsSystem.Persistance.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewTeacherDetailAsPropFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherDetailApprovalOutBox_Email",
                table: "courseStudentApprovalOutBoxes");

            migrationBuilder.DropColumn(
                name: "TeacherDetailApprovalOutBox_FullName",
                table: "courseStudentApprovalOutBoxes");

            migrationBuilder.DropColumn(
                name: "TeacherDetailApprovalOutBox_PhoneNumber",
                table: "courseStudentApprovalOutBoxes");

            migrationBuilder.DropColumn(
                name: "TeacherDetailApprovalOutBox_Subject",
                table: "courseStudentApprovalOutBoxes");

            migrationBuilder.AddColumn<string>(
                name: "TeacherDetailApprovalOutBox_Email",
                table: "LessonStudentStudentApprovalOutBoxes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherDetailApprovalOutBox_FullName",
                table: "LessonStudentStudentApprovalOutBoxes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherDetailApprovalOutBox_PhoneNumber",
                table: "LessonStudentStudentApprovalOutBoxes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherDetailApprovalOutBox_Subject",
                table: "LessonStudentStudentApprovalOutBoxes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherDetailApprovalOutBox_Email",
                table: "LessonStudentStudentApprovalOutBoxes");

            migrationBuilder.DropColumn(
                name: "TeacherDetailApprovalOutBox_FullName",
                table: "LessonStudentStudentApprovalOutBoxes");

            migrationBuilder.DropColumn(
                name: "TeacherDetailApprovalOutBox_PhoneNumber",
                table: "LessonStudentStudentApprovalOutBoxes");

            migrationBuilder.DropColumn(
                name: "TeacherDetailApprovalOutBox_Subject",
                table: "LessonStudentStudentApprovalOutBoxes");

            migrationBuilder.AddColumn<string>(
                name: "TeacherDetailApprovalOutBox_Email",
                table: "courseStudentApprovalOutBoxes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherDetailApprovalOutBox_FullName",
                table: "courseStudentApprovalOutBoxes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherDetailApprovalOutBox_PhoneNumber",
                table: "courseStudentApprovalOutBoxes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherDetailApprovalOutBox_Subject",
                table: "courseStudentApprovalOutBoxes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
