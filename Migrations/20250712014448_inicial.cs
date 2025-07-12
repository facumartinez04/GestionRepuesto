using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionRepuestoAPI.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombrePermiso = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataKey = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Repuestos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stock = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repuestos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosPermisos",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    idPermiso = table.Column<int>(type: "int", nullable: false),
                    Usuarioid = table.Column<int>(type: "int", nullable: false),
                    Permisoid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosPermisos", x => new { x.idUsuario, x.idPermiso });
                    table.ForeignKey(
                        name: "FK_UsuariosPermisos_Permisos_Permisoid",
                        column: x => x.Permisoid,
                        principalTable: "Permisos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosPermisos_Usuarios_Usuarioid",
                        column: x => x.Usuarioid,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosRoles",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    idRol = table.Column<int>(type: "int", nullable: false),
                    Usuarioid = table.Column<int>(type: "int", nullable: false),
                    Rolid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosRoles", x => new { x.idUsuario, x.idRol });
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Roles_Rolid",
                        column: x => x.Rolid,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosRoles_Usuarios_Usuarioid",
                        column: x => x.Usuarioid,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPermisos_Permisoid",
                table: "UsuariosPermisos",
                column: "Permisoid");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPermisos_Usuarioid",
                table: "UsuariosPermisos",
                column: "Usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_Rolid",
                table: "UsuariosRoles",
                column: "Rolid");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRoles_Usuarioid",
                table: "UsuariosRoles",
                column: "Usuarioid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Repuestos");

            migrationBuilder.DropTable(
                name: "UsuariosPermisos");

            migrationBuilder.DropTable(
                name: "UsuariosRoles");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
