using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStore.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class concerttitleindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Musicales");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Genre",
                newSchema: "Musicales");

            migrationBuilder.RenameTable(
                name: "Concert",
                newName: "Concert",
                newSchema: "Musicales");

            migrationBuilder.CreateIndex(
                name: "IX_Concert_Title",
                schema: "Musicales",
                table: "Concert",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Concert_Title",
                schema: "Musicales",
                table: "Concert");

            migrationBuilder.RenameTable(
                name: "Genre",
                schema: "Musicales",
                newName: "Genre");

            migrationBuilder.RenameTable(
                name: "Concert",
                schema: "Musicales",
                newName: "Concert");
        }
    }
}
