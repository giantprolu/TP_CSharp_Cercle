using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Geometrie.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AjoutCercle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cercles",
                table: "Cercles");

            migrationBuilder.RenameTable(
                name: "Cercles",
                newName: "cercle");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cercle",
                table: "cercle",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_cercle",
                table: "cercle");

            migrationBuilder.RenameTable(
                name: "cercle",
                newName: "Cercles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cercles",
                table: "Cercles",
                column: "Id");
        }
    }
}
