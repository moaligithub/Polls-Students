using Microsoft.EntityFrameworkCore.Migrations;

namespace Polls.EF.Migrations
{
    public partial class EditDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Points",
                table: "QuestionPoll");

            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "QuestionPoll",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "QuestionPoll");

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "QuestionPoll",
                type: "int",
                maxLength: 5,
                nullable: false,
                defaultValue: 0);
        }
    }
}
