using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIGallery.Migrations
{
    /// <inheritdoc />
    public partial class Sexto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aperture",
                table: "WorkArts",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ISO",
                table: "WorkArts",
                type: "varchar(5)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Speed",
                table: "WorkArts",
                type: "varchar(10)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aperture",
                table: "WorkArts");

            migrationBuilder.DropColumn(
                name: "ISO",
                table: "WorkArts");

            migrationBuilder.DropColumn(
                name: "Speed",
                table: "WorkArts");
        }
    }
}
