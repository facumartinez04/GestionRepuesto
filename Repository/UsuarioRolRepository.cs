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
                .Where(ur => ur.idUsuario == usuarioId)
                .ToList();
        }


        public bool AsignarRol(int usuarioId, int rolId)
        {
            if (!_dbContext.Roles.Any(r => r.id == rolId))
                return false;

            var yaAsignado = _dbContext.UsuariosRoles
                .Any(ur => ur.idUsuario == usuarioId && ur.idRol == rolId);

            if (yaAsignado) return false;

            var usuarioRol = new UsuarioRol
            {
                idUsuario = usuarioId,
                idRol = rolId
            };

            _dbContext.UsuariosRoles.Add(usuarioRol);
            return GuardarCambios();
        }


        public bool RemoverRol(int usuarioId, int rolId)
        {
            var usuarioRol = _dbContext.UsuariosRoles
                .FirstOrDefault(ur => ur.idUsuario == usuarioId && ur.idRol == rolId);

            if (usuarioRol == null) return false;

            _dbContext.UsuariosRoles.Remove(usuarioRol);
            return GuardarCambios();
        }

        public bool GuardarCambios()
        {
            return _dbContext.SaveChanges() >= 0;
        }
    }
}
