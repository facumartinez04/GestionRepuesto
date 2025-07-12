using GestionRepuestoAPI.Modelos;

namespace GestionRepuestoAPI.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        ICollection<Usuario> ObtenerUsuarios();
        Usuario ObtenerUsuario(int id);
        Usuario ObtenerUsuario(string nombreUsuario);

        bool EditarRefreshToken(int id, string refreshToken, DateTime fechaExpiracion);
        bool ExisteUsuario(int id);
        bool ExisteUsuario(string nombreUsuario);
        bool CrearUsuario(Usuario usuario);
        bool ActualizarUsuario(Usuario usuario);    
        bool EliminarUsuario(int id);
        bool GuardarCambios();
            
    }
}
