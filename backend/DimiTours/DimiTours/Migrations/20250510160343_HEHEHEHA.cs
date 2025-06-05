using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DimiTours.Migrations
{
    /// <inheritdoc />
    public partial class HEHEHEHA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityQuestion_Question_QuestionId1",
                table: "ActivityQuestion");

            migrationBuilder.DropIndex(
                name: "IX_ActivityQuestion_QuestionId1",
                table: "ActivityQuestion");

            migrationBuilder.DropColumn(
                name: "QuestionId1",
                table: "ActivityQuestion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionId1",
                table: "ActivityQuestion",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActivityQuestion_QuestionId1",
                table: "ActivityQuestion",
                column: "QuestionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityQuestion_Question_QuestionId1",
                table: "ActivityQuestion",
                column: "QuestionId1",
                principalTable: "Question",
                principalColumn: "Id");
        }
    }
}
