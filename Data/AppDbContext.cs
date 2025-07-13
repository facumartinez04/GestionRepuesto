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
        public DbSet<RolPermiso> RolesPermisos { get; set; }



        public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para UsuarioPermiso
            modelBuilder.Entity<UsuarioPermiso>()
                .HasKey(up => new { up.idUsuario, up.idPermiso });


            modelBuilder.Entity<UsuarioPermiso>()
            .HasKey(up => new { up.idUsuario, up.idPermiso });

            modelBuilder.Entity<UsuarioRol>()
                .HasKey(ur => new { ur.idUsuario, ur.idRol });

            modelBuilder.Entity<UsuarioRol>()
                .HasKey(ur => new { ur.idUsuario, ur.idRol });


            modelBuilder.Entity<RolPermiso>()
              .HasKey(ur => new { ur.idRol, ur.idPermiso });

            modelBuilder.Entity<RolPermiso>()
                .HasKey(ur => new { ur.idRol, ur.idPermiso });

            modelBuilder.Entity<UsuarioRol>()
                .HasOne(ur => ur.usuario)
                .WithMany()
                .HasForeignKey(ur => ur.idUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsuarioRol>()
                .HasOne(ur => ur.rol)
                .WithMany()
                .HasForeignKey(ur => ur.idRol)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
