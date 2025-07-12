using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionRepuestoAPI.Migrations
{
    /// <inheritdoc />
    public partial class inicia22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPermisos_Permisos_Permisoid",
                table: "UsuariosPermisos");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPermisos_Usuarios_Usuarioid",
                table: "UsuariosPermisos");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosRoles_Roles_Rolid",
                table: "UsuariosRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosRoles_Usuarios_Usuarioid",
                table: "UsuariosRoles");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosRoles_Rolid",
                table: "UsuariosRoles");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosPermisos_Permisoid",
                table: "UsuariosPermisos");

            migrationBuilder.DropColumn(
                name: "Rolid",
                table: "UsuariosRoles");

            migrationBuilder.DropColumn(
                name: "Permisoid",
                table: "UsuariosPermisos");

            migrationBuilder.AlterColumn<int>(
                name: "Usuarioid",
                table: "UsuariosRoles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Usuarioid",
                table: "UsuariosPermisos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPermisos_Usuarios_Usuarioid",
                table: "UsuariosPermisos",
                column: "Usuarioid",
                principalTable: "Usuarios",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosRoles_Usuarios_Usuarioid",
                table: "UsuariosRoles",
                column: "Usuarioid",
                principalTable: "Usuarios",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosPermisos_Usuarios_Usuarioid",
                table: "UsuariosPermisos");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosRoles_Usuarios_Usuarioid",
                table: "UsuariosRoles");

            migrationBuilder.AlterColumn<int>(
                name: "Usuarioid",
                table: "UsuariosRoles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rolid",
                table: "UsuariosRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Usuarioid",
                table: "UsuariosPermisos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Permisoid",
                table: "UsuariosPermisos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_Rolid",
                table: "UsuariosRoles",
                column: "Rolid");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPermisos_Permisoid",
                table: "UsuariosPermisos",
                column: "Permisoid");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPermisos_Permisos_Permisoid",
                table: "UsuariosPermisos",
                column: "Permisoid",
                principalTable: "Permisos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosPermisos_Usuarios_Usuarioid",
                table: "UsuariosPermisos",
                column: "Usuarioid",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosRoles_Roles_Rolid",
                table: "UsuariosRoles",
                column: "Rolid",
                principalTable: "Roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosRoles_Usuarios_Usuarioid",
                table: "UsuariosRoles",
                column: "Usuarioid",
                principalTable: "Usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
