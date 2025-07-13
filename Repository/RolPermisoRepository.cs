using GestionRepuestoAPI.Data;
using GestionRepuestoAPI.Modelos;
using GestionRepuestoAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionRepuestoAPI.Repository
{
    public class RolPermisoRepository : IRolPermisoRepository
    {
        private readonly AppDbContext _dbContext;

        public RolPermisoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<RolPermiso> ObtenerPermisosDelRol(int rolId)
        {
            return _dbContext.RolesPermisos
                .AsNoTracking()
                .Where(rp => rp.idRol == rolId)
                .ToList();
        }

        public bool AsignarPermisoARol(int rolId, int permisoId)
        {
            bool yaTrackeado = _dbContext.ChangeTracker.Entries<RolPermiso>()
                .Any(e => e.Entity.idRol == rolId && e.Entity.idPermiso == permisoId);

            if (yaTrackeado)
                return false;

            bool yaExiste = _dbContext.RolesPermisos
                .AsNoTracking()
                .Any(rp => rp.idRol == rolId && rp.idPermiso == permisoId);

            if (yaExiste)
                return false;

            var nuevo = new RolPermiso
            {
                idRol = rolId,
                idPermiso = permisoId
            };

            _dbContext.RolesPermisos.Add(nuevo);
            return true;
        }

        public bool RemoverPermisoDelRol(int rolId, int permisoId)
        {
            var rolPermiso = _dbContext.RolesPermisos
                .FirstOrDefault(rp => rp.idRol == rolId && rp.idPermiso == permisoId);

            if (rolPermiso == null) return false;

            _dbContext.RolesPermisos.Remove(rolPermiso);
            return true;
        }

        public void RemoverTodosLosPermisosDelRol(int rolId)
        {
            var permisos = _dbContext.RolesPermisos
                .AsNoTracking()
                .Where(rp => rp.idRol == rolId)
                .ToList();

            _dbContext.RolesPermisos.RemoveRange(permisos);
        }

        public bool GuardarCambios()
        {
            return _dbContext.SaveChanges() >= 0;
        }
    }
}
