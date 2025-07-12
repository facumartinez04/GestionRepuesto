using System.Data;

namespace GestionRepuestoAPI.Modelos.Dtos
{
    public class LoginRespuestaDto
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }

        public string Usuario { get; set; }

        public ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();

        public ICollection<Rol> Roles { get; set; } = new List<Rol>();

        public LoginRespuestaDto()
        {
            Roles = new List<Rol>();
        }

    }
}
