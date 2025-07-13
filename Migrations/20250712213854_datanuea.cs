using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionRepuestoAPI.Migrations
{
    /// <inheritdoc />
    public partial class datanuea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_idRol",
                table: "UsuariosRoles",
                column: "idRol");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPermisos_idPermiso",
                table: "UsuariosPermisos",
                column: "idPermiso");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPermisos_Permisos_idPermiso",
                table: "UsuariosPermisos",
                column: "idPermiso",
                principalTable: "Permisos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPermisos_Usuarios_idUsuario",
                table: "UsuariosPermisos",
                column: "idUsuario",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosRoles_Roles_idRol",
                table: "UsuariosRoles",
                column: "idRol",
                principalTable: "Roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosRoles_Usuarios_idUsuario",
                table: "UsuariosRoles",
                column: "idUsuario",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPermisos_Permisos_idPermiso",
                table: "UsuariosPermisos");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPermisos_Usuarios_idUsuario",
                table: "UsuariosPermisos");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosRoles_Roles_idRol",
                table: "UsuariosRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosRoles_Usuarios_idUsuario",
                table: "UsuariosRoles");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosRoles_idRol",
                table: "UsuariosRoles");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosPermisos_idPermiso",
                table: "UsuariosPermisos");
        }
    }
}
