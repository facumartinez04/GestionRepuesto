using GestionRepuestoAPI.Modelos;

namespace GestionRepuestoAPI.Repository.Interfaces
{
    public interface IPermisoRepository
    {
        ICollection<Permiso> ObtenerPermisos();
        Permiso ObtenerPermiso(int id);
        bool CrearPermiso(Permiso permiso);
        bool ActualizarPermiso(Permiso permiso);
        bool EliminarPermiso(int id);
        bool ExistePermiso(int id);
        bool GuardarCambios();
    }
}
