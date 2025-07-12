using GestionRepuestoAPI.Data;
using GestionRepuestoAPI.Modelos;
using GestionRepuestoAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                .Where(up => up.idUsuario == usuarioId)
                .ToList();
        }


        public bool AsignarPermiso(int usuarioId, int permisoId)
        {
            var existe = _dbContext.UsuariosPermisos
                .Any(up => up.idUsuario == usuarioId && up.idPermiso == permisoId);

            if (existe) return false;

            var usuarioPermiso = new UsuarioPermiso
            {
                idUsuario = usuarioId,
                idPermiso = permisoId
            };

            _dbContext.UsuariosPermisos.Add(usuarioPermiso);
            return GuardarCambios();
        }

        public bool RemoverPermiso(int usuarioId, int permisoId)
        {
            var usuarioPermiso = _dbContext.UsuariosPermisos
                .FirstOrDefault(up => up.idUsuario == usuarioId && up.idPermiso == permisoId);

            if (usuarioPermiso == null) return false;

            _dbContext.UsuariosPermisos.Remove(usuarioPermiso);
            return GuardarCambios();
        }

        public bool GuardarCambios()
        {
            return _dbContext.SaveChanges() >= 0;
        }
    }
}
