using AutoMapper;
using GestionRepuestoAPI.Modelos;
using GestionRepuestoAPI.Modelos.Dtos;
using GestionRepuestoAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestionRepuestoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolRepository _rolRepository;
        private readonly IMapper _mapper;
        private readonly RespuestaAPI _respuesta;

        public RolController(IRolRepository rolRepository, IMapper mapper)
        {
            _rolRepository = rolRepository;
            _mapper = mapper;
            _respuesta = new RespuestaAPI();
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var lista = _rolRepository.ObtenerRoles();
            var dto = _mapper.Map<ICollection<RolDto>>(lista);
            _respuesta.Result = dto;
            return Ok(_respuesta);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetRol(int id)
        {
            if (!_rolRepository.ExisteRol(id)) return NotFound();

            var rol = _rolRepository.ObtenerRol(id);
            var dto = _mapper.Map<RolDto>(rol);
            _respuesta.Result = dto;
            return Ok(_respuesta);
        }

        [HttpPost]
        public IActionResult Crear([FromBody] RolDto dto)
        {
            if (dto == null) return BadRequest();

            var rol = _mapper.Map<Rol>(dto);
            _rolRepository.CrearRol(rol);

            _respuesta.Result = _mapper.Map<RolDto>(rol);
            _respuesta.StatusCode = HttpStatusCode.Created;
            return CreatedAtAction(nameof(GetRol), new { id = rol.id }, _respuesta);
        }

        [HttpPut("{id:int}")]
        public IActionResult Actualizar(int id, [FromBody] RolDto dto)
        {
            if (dto == null || id != dto.id) return BadRequest();

            if (!_rolRepository.ExisteRol(id)) return NotFound();

            var rol = _mapper.Map<Rol>(dto);
            _rolRepository.ActualizarRol(rol);
            _respuesta.StatusCode = HttpStatusCode.NoContent;
            return Ok(_respuesta);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id)
        {
            if (!_rolRepository.ExisteRol(id)) return NotFound();

            _rolRepository.EliminarRol(id);
            _respuesta.StatusCode = HttpStatusCode.NoContent;
            return Ok(_respuesta);
        }
    }
}
