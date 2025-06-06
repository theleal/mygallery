using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIGallery.Migrations
{
    /// <inheritdoc />
    public partial class quarto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "idFileBackBlaze",
                table: "Obras",
                type: "varchar(200)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idFileBackBlaze",
                table: "Obras");
        }
    }
}
