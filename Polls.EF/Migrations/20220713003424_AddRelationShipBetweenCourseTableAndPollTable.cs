using Microsoft.EntityFrameworkCore.Migrations;

namespace Polls.EF.Migrations
{
    public partial class AddRelationShipBetweenCourseTableAndPollTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poll_Session_SessionId",
                table: "Poll");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Poll",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Poll_SessionId",
                table: "Poll",
                newName: "IX_Poll_CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Poll_Course_CourseId",
                table: "Poll",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poll_Course_CourseId",
                table: "Poll");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Poll",
                newName: "SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Poll_CourseId",
                table: "Poll",
                newName: "IX_Poll_SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Poll_Session_SessionId",
                table: "Poll",
                column: "SessionId",
                principalTable: "Session",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
