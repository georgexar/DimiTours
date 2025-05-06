using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DimiTours.Migrations
{
    /// <inheritdoc />
    public partial class MediaItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaItems",
                table: "Section");

            migrationBuilder.CreateTable(
                name: "MediaItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MediaType = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaItem_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaItem_SectionId",
                table: "MediaItem",
                column: "SectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaItem");

            migrationBuilder.AddColumn<string>(
                name: "MediaItems",
                table: "Section",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
