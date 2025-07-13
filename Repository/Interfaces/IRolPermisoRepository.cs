using GestionRepuestoAPI.Modelos;

namespace GestionRepuestoAPI.Repository.Interfaces
{
    public interface IRolPermisoRepository
    {
        ICollection<RolPermiso> ObtenerPermisosDelRol(int rolId);
        bool AsignarPermisoARol(int rolId, int permisoId);
        bool RemoverPermisoDelRol(int rolId, int permisoId);
        void RemoverTodosLosPermisosDelRol(int rolId);
        bool GuardarCambios();


    }
}
