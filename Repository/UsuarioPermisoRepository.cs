using GestionRepuestoAPI.Data;
using GestionRepuestoAPI.Modelos;
using GestionRepuestoAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GestionRepuestoAPI.Repository
{
    public class UsuarioPermisoRepository : IUsuarioPermisoRepository
    {
        private readonly AppDbContext _dbContext;

        public UsuarioPermisoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<UsuarioPermiso> ObtenerPermisosDeUsuario(int usuarioId)
        {
            return _dbContext.UsuariosPermisos
                .AsNoTracking()
                .Where(up => up.idUsuario == usuarioId)
                .ToList();
        }

        public void RemoverTodosLosPermisos(int usuarioId)
        {
            var permisos = _dbContext.UsuariosPermisos
                .AsNoTracking() 
                .Where(up => up.idUsuario == usuarioId)
                .ToList();

            _dbContext.UsuariosPermisos.RemoveRange(permisos);
        }

        public bool AsignarPermiso(int usuarioId, int idPermiso)
        {
            if (_dbContext.ChangeTracker.Entries<UsuarioPermiso>()
                .Any(e => e.Entity.idUsuario == usuarioId && e.Entity.idPermiso == idPermiso))
            {
                return false;
            }

            if (_dbContext.UsuariosPermisos
                .AsNoTracking()
                .Any(up => up.idUsuario == usuarioId && up.idPermiso == idPermiso))
            {
                return false;
            }

            _dbContext.UsuariosPermisos.Add(new UsuarioPermiso
            {
                idUsuario = usuarioId,
                idPermiso = idPermiso
            });

            return true;
        }

        public bool GuardarCambios()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public bool RemoverPermiso(int usuarioId, int permisoId)
        {
            var usuarioPermiso = _dbContext.UsuariosPermisos
                .FirstOrDefault(up => up.idUsuario == usuarioId && up.idPermiso == permisoId);
            if (usuarioPermiso == null)
            {
                return false;
            }
            _dbContext.UsuariosPermisos.Remove(usuarioPermiso);
            return true;
        }
    }
}
