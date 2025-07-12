using GestionRepuestoAPI.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestionRepuestoAPI.Data
{
    public class AppDbContext : DbContext   
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Repuesto> Repuestos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<UsuarioRol> UsuariosRoles { get; set; }

        public DbSet<UsuarioPermiso> UsuariosPermisos { get; set; }

        public DbSet<Permiso> Permisos { get; set; }

        public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UsuarioPermiso>()
                .HasKey(up => new { up.idUsuario, up.idPermiso });

            modelBuilder.Entity<UsuarioPermiso>()
                .Property(up => up.idUsuario).HasColumnName("idUsuario");

            modelBuilder.Entity<UsuarioPermiso>()
                .Property(up => up.idPermiso).HasColumnName("idPermiso");

            modelBuilder.Entity<UsuarioRol>()
                .HasKey(ur => new { ur.idUsuario, ur.idRol });

            modelBuilder.Entity<UsuarioRol>()
                .Property(ur => ur.idUsuario).HasColumnName("idUsuario");

            modelBuilder.Entity<UsuarioRol>()
                .Property(ur => ur.idRol).HasColumnName("idRol");

        }

    }
}
