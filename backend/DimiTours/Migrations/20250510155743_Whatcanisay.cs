using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DimiTours.Migrations
{
    /// <inheritdoc />
    public partial class Whatcanisay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityQuestion",
                table: "ActivityQuestion");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ActivityQuestion",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityQuestion",
                table: "ActivityQuestion",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityQuestion_ActivityId",
                table: "ActivityQuestion",
                column: "ActivityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ActivityQuestion",
                table: "ActivityQuestion");

            migrationBuilder.DropIndex(
                name: "IX_ActivityQuestion_ActivityId",
                table: "ActivityQuestion");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ActivityQuestion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ActivityQuestion",
                table: "ActivityQuestion",
                columns: new[] { "ActivityId", "QuestionId" });
        }
    }
}
