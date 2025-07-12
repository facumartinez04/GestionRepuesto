using GestionRepuestoAPI.Data;
using GestionRepuestoAPI.Modelos;
using GestionRepuestoAPI.Repository.Interfaces;

namespace GestionRepuestoAPI.Repository
{
    public class PermisoRepository : IPermisoRepository
    {
        private readonly AppDbContext _dbContext;

        public PermisoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CrearPermiso(Permiso permiso)
        {
            _dbContext.Permisos.Add(permiso);
            return GuardarCambios();
        }

        public bool ActualizarPermiso(Permiso permiso)
        {
            _dbContext.Permisos.Update(permiso);
            return GuardarCambios();
        }

        public bool EliminarPermiso(int id)
        {
            var permiso = ObtenerPermiso(id);
            if (permiso == null) return false;

            _dbContext.Permisos.Remove(permiso);
            return GuardarCambios();
        }

        public Permiso ObtenerPermiso(int id)
        {
            return _dbContext.Permisos.FirstOrDefault(p => p.id == id);
        }

        public ICollection<Permiso> ObtenerPermisos()
        {
            return _dbContext.Permisos.OrderBy(p => p.nombrePermiso).ToList();
        }

        public bool ExistePermiso(int id)
        {
            return _dbContext.Permisos.Any(p => p.id == id);
        }

        public bool GuardarCambios()
        {
            return _dbContext.SaveChanges() >= 0;
        }
    }
}
