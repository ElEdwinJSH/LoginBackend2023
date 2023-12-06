using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginBackend2023.Migrations
{
    public partial class Cambio2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "JuegosFavoritos",
                newName: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "JuegosFavoritos",
                newName: "UsuarioId");
        }
    }
}
