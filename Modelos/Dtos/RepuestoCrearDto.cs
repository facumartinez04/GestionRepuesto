namespace GestionRepuestoAPI.Modelos.Dtos
{
    public class RepuestoCrearDto
    {
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public decimal precio { get; set; }

        public string marca { get; set; } = string.Empty;

        public string modelo { get; set; } = string.Empty;

        public string categoria { get; set; } = string.Empty;
        public int stock { get; set; }

    }
}
