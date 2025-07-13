namespace GestionRepuestoAPI.Modelos.Dtos
{
    public class RolPermisoEditarDto
    {
        public int idRol { get; set; }

        public string descripcion { get; set; }
        public List<RolPermisoDto>? listaPermisos { get; set; }
        public RolPermisoEditarDto()
        {
            listaPermisos = new List<RolPermisoDto>();
        }
    }
}
