using System.ComponentModel.DataAnnotations;

namespace GestionRepuestoAPI.Modelos.Dtos
{
    public class UsuarioCrearDto
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string nombreUsuario { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string clave { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un correo electrónico válido.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]

        public string email { get; set; }




    }
}
