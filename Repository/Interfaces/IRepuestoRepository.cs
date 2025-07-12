using GestionRepuestoAPI.Modelos;

namespace GestionRepuestoAPI.Repository.Interfaces

{
    public interface IRepuestoRepository
    {
        ICollection<Repuesto> ObtenerRepuestos();
        Repuesto ObtenerRepuesto(int id);
        bool ExisteRepuesto(int id);
        bool CrearRepuesto(Repuesto repuesto);
        bool ActualizarRepuesto(Repuesto repuesto);
        bool EliminarRepuesto(int id);
        bool GuardarCambios();

    }
}
