using GestionRepuestoAPI.Modelos;

namespace GestionRepuestoAPI.Repository.Interfaces
{
    public interface IUsuarioPermisoRepository
    {
        ICollection<UsuarioPermiso> ObtenerPermisosDeUsuario(int usuarioId);
        bool AsignarPermiso(int usuarioId, int permisoId);
        bool RemoverPermiso(int usuarioId, int permisoId);

        void RemoverTodosLosPermisos(int usuarioId);
        bool GuardarCambios();
    }
}
