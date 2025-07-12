using System.ComponentModel.DataAnnotations;

namespace GestionRepuestoAPI.Modelos
{
    public class Usuario
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string nombreUsuario { get; set; }

        public string clave { get; set; }


        public string email { get; set; }


        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; } = DateTime.MinValue;


        public ICollection<UsuarioPermiso>? usuarioPermisos { get; set; }
        public ICollection<UsuarioRol>? usuarioRols { get; set; }


        public Usuario()
        {
            usuarioPermisos = new List<UsuarioPermiso>();
            usuarioRols = new List<UsuarioRol>();
        }

    }
}
