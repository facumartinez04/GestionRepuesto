using System.ComponentModel.DataAnnotations.Schema;

namespace GestionRepuestoAPI.Modelos
{
    public class UsuarioRol
    {

        public int idUsuario { get; set; }
        public int idRol { get; set; }

        [NotMapped]

        public Rol rol { get; set; }

        [NotMapped]

        public Usuario usuario { get; set; }
    }
}
