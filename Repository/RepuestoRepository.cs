using GestionRepuestoAPI.Data;
using GestionRepuestoAPI.Modelos;
using GestionRepuestoAPI.Repository.Interfaces;

namespace GestionRepuestoAPI.Repository
{
    public class RepuestoRepository : IRepuestoRepository
    {
        private readonly AppDbContext _dbContext;

        public RepuestoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public bool ActualizarRepuesto(Repuesto repuesto)
        {
            if (repuesto == null)
            {
                return false;
            }
            _dbContext.Repuestos.Update(repuesto);
            return GuardarCambios();
        }

        public bool CrearRepuesto(Repuesto repuesto)
        {
            if (repuesto == null)
            {
                return false;
            }
            _dbContext.Repuestos.Add(repuesto);
            return GuardarCambios();
        }

        public bool EliminarRepuesto(int id)
        {
            var repuesto = ObtenerRepuesto(id);
            if (repuesto == null)
            {
                return false;
            }
            _dbContext.Repuestos.Remove(repuesto);
            return GuardarCambios();
        }

        public bool ExisteRepuesto(int id)
        {
            return _dbContext.Repuestos.Any(r => r.id == id);
        }

        public bool GuardarCambios()
        {
            return _dbContext.SaveChanges() >= 0; 
        }

        public Repuesto ObtenerRepuesto(int id)
        {
            return _dbContext.Repuestos.FirstOrDefault(r => r.id == id);
        }

        public ICollection<Repuesto> ObtenerRepuestos()
        {
            return _dbContext.Repuestos.OrderBy(r => r.nombre).ToList();
        }
    }
}
