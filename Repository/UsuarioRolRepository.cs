using GestionRepuestoAPI.Data;
using GestionRepuestoAPI.Modelos;
using GestionRepuestoAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionRepuestoAPI.Repository
{
    public class UsuarioRolRepository : IUsuarioRolRepository
    {
        private readonly AppDbContext _dbContext;

        public UsuarioRolRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<UsuarioRol> ObtenerRolesDeUsuario(int usuarioId)
        {
            return _dbContext.UsuariosRoles
                .AsNoTracking() 
                .Where(ur => ur.idUsuario == usuarioId)
                .ToList();
        }

        public void RemoverTodosLosRoles(int usuarioId)
        {
            var roles = _dbContext.UsuariosRoles
                .AsNoTracking()
                .Where(ur => ur.idUsuario == usuarioId)
                .ToList();

            _dbContext.UsuariosRoles.RemoveRange(roles);
        }

        public bool AsignarRol(int usuarioId, int rolId)
        {
            bool yaTrackeado = _dbContext.ChangeTracker.Entries<UsuarioRol>()
                .Any(e => e.Entity.idUsuario == usuarioId && e.Entity.idRol == rolId);

            if (yaTrackeado)
                return false;

            bool yaExiste = _dbContext.UsuariosRoles
                .AsNoTracking()
                .Any(ur => ur.idUsuario == usuarioId && ur.idRol == rolId);

            if (yaExiste)
                return false;


            var usuarioRol = new UsuarioRol
            {
                idUsuario = usuarioId,
                idRol = rolId
            };

            _dbContext.UsuariosRoles.Add(usuarioRol);
            return true;
        }

        public bool RemoverRol(int usuarioId, int rolId)
        {
            var usuarioRol = _dbContext.UsuariosRoles
                .FirstOrDefault(ur => ur.idUsuario == usuarioId && ur.idRol == rolId);

            if (usuarioRol == null) return false;

            _dbContext.UsuariosRoles.Remove(usuarioRol);
            return true;
        }

        public bool GuardarCambios()
        {
            return _dbContext.SaveChanges() >= 0;
        }
    }
}
