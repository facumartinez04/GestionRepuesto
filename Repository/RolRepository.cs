using GestionRepuestoAPI.Data;
using GestionRepuestoAPI.Modelos;
using GestionRepuestoAPI.Repository.Interfaces;

namespace GestionRepuestoAPI.Repository
{
    public class RolRepository : IRolRepository
    {
        private readonly AppDbContext _db;

        public RolRepository(AppDbContext db)
        {
            _db = db;
        }

        public bool CrearRol(Rol rol)
        {
            if (rol == null) return false;
            _db.Roles.Add(rol);
            GuardarCambios();
            return true;
        }

        public bool ActualizarRol(Rol rol)
        {
            if (rol == null) return false;
            _db.Roles.Update(rol);
            return GuardarCambios();
        }

        public bool EliminarRol(int id)
        {
            var rol = ObtenerRol(id);
            if (rol == null) return false;
            _db.Roles.Remove(rol);
            GuardarCambios();
            return true;


        }

        public Rol ObtenerRol(int id)
        {
            return _db.Roles.FirstOrDefault(r => r.id == id);
        }

        public ICollection<Rol> ObtenerRoles()
        {
            return _db.Roles.OrderBy(r => r.descripcion).ToList();
        }

        public bool ExisteRol(int id)
        {
            return _db.Roles.Any(r => r.id == id);
        }

        public bool GuardarCambios()
        {
            return _db.SaveChanges() >= 0;
        }
    }
}
