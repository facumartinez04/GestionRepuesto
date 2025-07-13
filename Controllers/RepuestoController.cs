using AutoMapper;
using GestionRepuestoAPI.Modelos;
using GestionRepuestoAPI.Modelos.Dtos;
using GestionRepuestoAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestionRepuestoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepuestoController : ControllerBase
    {
        private readonly IRepuestoRepository _repuestoRepository;
        private readonly IMapper _mapper;
        protected RespuestaAPI _respuestaAPI;

        public RepuestoController(IRepuestoRepository repuestoRepository, IMapper mapper)
        {
            _repuestoRepository = repuestoRepository;
            _mapper = mapper;
            _respuestaAPI = new RespuestaAPI();
        }

        [Authorize(Policy = "ListarRepuesto")]
        [HttpGet]
        public IActionResult ObtenerRepuestos()
        {
            var repuestos = _repuestoRepository.ObtenerRepuestos();
            var repuestosDto = _mapper.Map<ICollection<RepuestoLeerDto>>(repuestos);

            _respuestaAPI.Result = repuestosDto;
            _respuestaAPI.StatusCode = HttpStatusCode.OK;

            return Ok(_respuestaAPI);
        }

        [Authorize(Policy = "ListarRepuesto")]
        [HttpGet("{id:int}", Name = "ObtenerRepuesto")]
        public IActionResult ObtenerRepuesto(int id)
        {
            if (!_repuestoRepository.ExisteRepuesto(id))
            {
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "Repuesto no encontrado.";
                return NotFound(_respuestaAPI);
            }

            var repuesto = _repuestoRepository.ObtenerRepuesto(id);
            var repuestoDto = _mapper.Map<RepuestoLeerDto>(repuesto);

            _respuestaAPI.Result = repuestoDto;
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            return Ok(_respuestaAPI);
        }

        [Authorize(Policy = "CrearRepuesto")]
        [HttpPost]
        public IActionResult CrearRepuesto([FromBody] RepuestoCrearDto repuestoCrearDto)
        {
            if (repuestoCrearDto == null)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.Message = "Datos inválidos.";
                return BadRequest(_respuestaAPI);
            }

            var repuesto = _mapper.Map<Repuesto>(repuestoCrearDto);

            if (!_repuestoRepository.CrearRepuesto(repuesto))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.Message = "Error al crear el repuesto.";
                return StatusCode(StatusCodes.Status500InternalServerError, _respuestaAPI);
            }

            _respuestaAPI.Result = repuesto;
            _respuestaAPI.StatusCode = HttpStatusCode.Created;
            _respuestaAPI.Message = "Repuesto creado correctamente.";

            return CreatedAtRoute("ObtenerRepuesto", new { id = repuesto.id }, _respuestaAPI);
        }

        [Authorize(Policy = "EditarRepuesto")]
        [HttpPut("{id:int}")]
        public IActionResult ActualizarRepuesto(int id, [FromBody] RepuestoCrearDto repuestoActualizarDto)
        {
            if (repuestoActualizarDto == null || id <= 0)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.Message = "Datos inválidos.";
                return BadRequest(_respuestaAPI);
            }

            if (!_repuestoRepository.ExisteRepuesto(id))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                _respuestaAPI.Message = $"No se encontró ningún repuesto con ID = {id}.";
                return NotFound(_respuestaAPI);
            }

            var repuesto = _mapper.Map<Repuesto>(repuestoActualizarDto);
            repuesto.id = id;

            if (!_repuestoRepository.ActualizarRepuesto(repuesto))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.Message = "Error al actualizar el repuesto.";
                return StatusCode(StatusCodes.Status500InternalServerError, _respuestaAPI);
            }

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.Message = "Repuesto actualizado correctamente.";
            return Ok(_respuestaAPI);
        }


        [Authorize(Policy = "EliminarRepuesto")]
        [HttpDelete("{id:int}")]
        public IActionResult EliminarRepuesto(int id)
        {
            if (!_repuestoRepository.ExisteRepuesto(id))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                _respuestaAPI.Message = $"No se encontró ningún repuesto con ID = {id}.";
                return NotFound(_respuestaAPI);
            }

            var repuesto = _repuestoRepository.ObtenerRepuesto(id);
            if (!_repuestoRepository.EliminarRepuesto(repuesto.id))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.Message = "Error al eliminar el repuesto.";
                return StatusCode(StatusCodes.Status500InternalServerError, _respuestaAPI);
            }

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.Message = "Repuesto eliminado correctamente.";
            return Ok(_respuestaAPI);
        }


        [Authorize(Policy = "EditarRepuesto")]
        [HttpPatch("actualizar-stock/{id:int}")]
        public IActionResult ActualizarStock(int id, [FromBody] int nuevoStock)
        {
            var repuesto = _repuestoRepository.ObtenerRepuesto(id);
            if (repuesto == null)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                _respuestaAPI.Message = "Repuesto no encontrado.";
                return NotFound(_respuestaAPI);
            }

            if (nuevoStock < 0)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.Message = "El stock no puede ser negativo.";
                return BadRequest(_respuestaAPI);
            }

            repuesto.stock = Convert.ToInt32(nuevoStock);
            var resultado = _repuestoRepository.ActualizarRepuesto(repuesto);

            if (!resultado)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.Message = "Error al actualizar el stock.";
                return StatusCode(500, _respuestaAPI);
            }

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.Message = "Stock actualizado correctamente.";
            return Ok(_respuestaAPI);
        }


    }
}
