using Microsoft.EntityFrameworkCore.Migrations;

namespace Polls.EF.Migrations
{
    public partial class removeStudentNamefromInstructorReviewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "InstructorReview");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "InstructorReview",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
