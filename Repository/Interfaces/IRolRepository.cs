using GestionRepuestoAPI.Modelos;

namespace GestionRepuestoAPI.Repository.Interfaces
{
    public interface  IRolRepository
    {
        ICollection<Rol> ObtenerRoles();
        Rol ObtenerRol(int id);
        bool CrearRol(Rol rol);
        bool ActualizarRol(Rol rol);
        bool EliminarRol(int id);
        bool ExisteRol(int id);
        bool GuardarCambios();
    }
}
