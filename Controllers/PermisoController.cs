using AutoMapper;
using GestionRepuestoAPI.Modelos;
using GestionRepuestoAPI.Modelos.Dtos;
using GestionRepuestoAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestionRepuestoAPI.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class PermisoController : ControllerBase
    {
        private readonly IPermisoRepository _permisoRepository;
        private readonly IMapper _mapper;
        protected RespuestaAPI _respuestaAPI;

        public PermisoController(IPermisoRepository permisoRepository, IMapper mapper)
        {
            _permisoRepository = permisoRepository;
            _mapper = mapper;
            _respuestaAPI = new RespuestaAPI();
        }

        [HttpGet]
        public IActionResult ObtenerPermisos()
        {
            var lista = _permisoRepository.ObtenerPermisos();
            var dto = _mapper.Map<List<PermisoLeerDto>>(lista);

            _respuestaAPI.Result = dto;
            _respuestaAPI.StatusCode = HttpStatusCode.OK;

            return Ok(_respuestaAPI);
        }

        [HttpGet("{id}", Name = "ObtenerPermiso")]
        public IActionResult ObtenerPermiso(int id)
        {
            var permiso = _permisoRepository.ObtenerPermiso(id);
            if (permiso == null)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                _respuestaAPI.Message = "Permiso no encontrado.";
                return NotFound(_respuestaAPI);
            }

            var dto = _mapper.Map<PermisoLeerDto>(permiso);
            _respuestaAPI.Result = dto;
            _respuestaAPI.StatusCode = HttpStatusCode.OK;

            return Ok(_respuestaAPI);
        }

        [HttpPost]
        public IActionResult CrearPermiso([FromBody] PermisoCrearDto permisoDto)
        {
            if (permisoDto == null)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.Message = "Datos inválidos.";
                return BadRequest(_respuestaAPI);
            }

            var permiso = _mapper.Map<Permiso>(permisoDto);
            _permisoRepository.CrearPermiso(permiso);
            var dto = _mapper.Map<PermisoLeerDto>(permiso);

            _respuestaAPI.Result = dto;
            _respuestaAPI.StatusCode = HttpStatusCode.Created;

            return CreatedAtRoute("ObtenerPermiso", new { id = dto.id }, _respuestaAPI);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarPermiso(int id, [FromBody] PermisoCrearDto permisoDto)
        {
            var permiso = _permisoRepository.ObtenerPermiso(id);
            if (permiso == null)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                _respuestaAPI.Message = "Permiso no encontrado.";
                return NotFound(_respuestaAPI);
            }

            _mapper.Map(permisoDto, permiso);
            _permisoRepository.ActualizarPermiso(permiso);

            _respuestaAPI.StatusCode = HttpStatusCode.NoContent;
            return Ok(_respuestaAPI);
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarPermiso(int id)
        {
            var eliminado = _permisoRepository.EliminarPermiso(id);
            if (!eliminado)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                _respuestaAPI.Message = "No se encontró el permiso a eliminar.";
                return NotFound(_respuestaAPI);
            }

            _respuestaAPI.StatusCode = HttpStatusCode.NoContent;
            return Ok(_respuestaAPI);
        }
    }
}
