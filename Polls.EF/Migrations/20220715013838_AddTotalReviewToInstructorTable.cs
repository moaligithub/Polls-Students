using Microsoft.EntityFrameworkCore.Migrations;

namespace Polls.EF.Migrations
{
    public partial class AddTotalReviewToInstructorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Course");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Question",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);

            migrationBuilder.AddColumn<float>(
                name: "TotalReview",
                table: "Instructor",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalReview",
                table: "Instructor");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Question",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Course",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
