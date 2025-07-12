using System.ComponentModel.DataAnnotations;

namespace GestionRepuestoAPI.Modelos.Dtos
{
    public class UsuarioEditarDto
    {
        public int id { get; set; }
        public string nombreUsuario { get; set; }


        public string clave { get; set; }
        public string email { get; set; }

        public ICollection<UsuarioPermisoDto> usuarioPermisos { get; set; }
        public ICollection<UsuarioRolDto> usuarioRols { get; set; }

        public UsuarioEditarDto()
        {
            usuarioPermisos = new List<UsuarioPermisoDto>();
            usuarioRols = new List<UsuarioRolDto>();
        }
    }
}
