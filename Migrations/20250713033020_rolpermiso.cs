using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionRepuestoAPI.Migrations
{
    /// <inheritdoc />
    public partial class rolpermiso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RolesPermisos",
                columns: table => new
                {
                    idRol = table.Column<int>(type: "int", nullable: false),
                    idPermiso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPermisos", x => new { x.idRol, x.idPermiso });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolesPermisos");
        }
    }
}
