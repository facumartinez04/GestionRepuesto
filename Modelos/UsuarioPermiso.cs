using System.ComponentModel.DataAnnotations.Schema;

namespace GestionRepuestoAPI.Modelos
{
    public class UsuarioPermiso
    {
        public int idUsuario { get; set; }

        public int idPermiso { get; set; }

        [NotMapped]
        public Usuario Usuario { get; set; }
        [NotMapped]

        public Permiso Permiso { get; set; }
    }
}
