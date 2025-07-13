using GestionRepuestoAPI.Modelos;

namespace GestionRepuestoAPI.Repository.Interfaces
{
    public interface IUsuarioRolRepository
    {
        ICollection<UsuarioRol> ObtenerRolesDeUsuario(int usuarioId);

        ICollection<UsuarioRol> ObtenerUsuariosPorRol(int rolId);
        bool AsignarRol(int usuarioId, int rolId);
        bool RemoverRol(int usuarioId, int rolId);

        void RemoverTodosLosRoles(int usuarioId);
        bool GuardarCambios();
    }
}
