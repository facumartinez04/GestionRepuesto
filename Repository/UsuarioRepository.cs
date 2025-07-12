using GestionRepuestoAPI.Data;
using GestionRepuestoAPI.Helpers;
using GestionRepuestoAPI.Modelos;
using GestionRepuestoAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionRepuestoAPI.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _dbContext;

        public UsuarioRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CrearUsuario(Usuario usuario)
        {
            if (usuario == null) return false;

            usuario.clave = SeguridadHelper.HashPassword(usuario.clave);

            _dbContext.Usuarios.Add(usuario);
            return GuardarCambios();
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            if (usuario == null) return false;


            usuario.clave = SeguridadHelper.HashPassword(usuario.clave);

            return GuardarCambios();
        }

        public bool EliminarUsuario(int id)
        {
            var usuario = ObtenerUsuario(id);
            if (usuario == null) return false;

            _dbContext.Usuarios.Remove(usuario);
            return GuardarCambios();
        }

        public Usuario ObtenerUsuario(int id)
        {
            return _dbContext.Usuarios
                .FirstOrDefault(u => u.id == id);
        }

        public Usuario ObtenerUsuario(string nombreUsuario)
        {
            return _dbContext.Usuarios
                .FirstOrDefault(u => u.nombreUsuario == nombreUsuario);
        }

        public ICollection<Usuario> ObtenerUsuarios()
        {
            return _dbContext.Usuarios
                .OrderBy(u => u.nombreUsuario)
                .ToList();
        }

        public bool ExisteUsuario(int id)
        {
            return _dbContext.Usuarios.Any(u => u.id == id);
        }

        public bool ExisteUsuario(string nombreUsuario)
        {
            return _dbContext.Usuarios.Any(u => u.nombreUsuario == nombreUsuario);
        }

        public bool GuardarCambios()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public bool EditarRefreshToken(int id, string refreshToken, DateTime fechaExpiracion)
        {
            var usuario = ObtenerUsuario(id);
            if (usuario == null) return false;
            usuario.RefreshToken = refreshToken;
            usuario.RefreshTokenExpiryTime = fechaExpiracion;
            _dbContext.Usuarios.Update(usuario);
            return GuardarCambios();
        }
    }
}
