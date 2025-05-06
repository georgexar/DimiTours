using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DimiTours.Migrations
{
    /// <inheritdoc />
    public partial class NewMigr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "User",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
