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
        private readonly IRolPermisoRepository _rolPermisoRepository;
        private readonly IPermisoRepository _permisoRepository;
        private readonly IUsuarioRolRepository _usuarioRolRepository;

        private readonly IMapper _mapper;
        private readonly RespuestaAPI _respuesta;

        public RolController(IRolRepository rolRepository,IRolPermisoRepository rolPermisoRepository, IUsuarioRolRepository usuarioRolRepository, IPermisoRepository permisoRepository, IMapper mapper)
        {
            _usuarioRolRepository = usuarioRolRepository;
            _permisoRepository = permisoRepository;
            _rolPermisoRepository = rolPermisoRepository;

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

            if (string.IsNullOrWhiteSpace(dto.descripcion))
            {
                _respuesta.Success = false;
                _respuesta.Message = "La descripción del rol es obligatoria.";
                _respuesta.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuesta);
            }

            if (_rolRepository.ObtenerRoles().ToList().Any(r => r.descripcion.Equals(dto.descripcion, StringComparison.OrdinalIgnoreCase)))
            {
                _respuesta.Success = false;
                _respuesta.Message = "Ya existe un rol con esa descripción.";
                _respuesta.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuesta);
            }


            var rol = _mapper.Map<Rol>(dto);
            _rolRepository.CrearRol(rol);

            _respuesta.Result = _mapper.Map<RolDto>(rol);
            _respuesta.StatusCode = HttpStatusCode.Created;
            return CreatedAtAction(nameof(GetRol), new { id = rol.id }, _respuesta);
        }

        [HttpPut("{id:int}")]
        public IActionResult Actualizar(int id, [FromBody] RolPermisoEditarDto dto)
        {
            if (dto == null || id != dto.idRol)
                return BadRequest();

            if (!_rolRepository.ExisteRol(id))
                return NotFound();

            if (string.IsNullOrWhiteSpace(dto.descripcion))
            {
                _respuesta.Success = false;
                _respuesta.Message = "La descripción del rol es obligatoria.";
                _respuesta.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuesta);
            }

            if (dto.listaPermisos != null && dto.listaPermisos.Count > 0)
            {
                foreach (var permiso in dto.listaPermisos)
                {
                    if (!_permisoRepository.ExistePermiso(permiso.idPermiso))
                    {
                        _respuesta.Success = false;
                        _respuesta.Message = $"El permiso con ID {permiso.idPermiso} no existe.";
                        _respuesta.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_respuesta);
                    }
                }
            }

            var rolExistente = _rolRepository.ObtenerRol(id);
            if (rolExistente == null)
                return NotFound();

            rolExistente.descripcion = dto.descripcion;

            _rolPermisoRepository.RemoverTodosLosPermisosDelRol(id);
            _rolPermisoRepository.GuardarCambios(); 

            if (dto.listaPermisos != null && dto.listaPermisos.Count > 0)
            {
                foreach (var permiso in dto.listaPermisos)
                {
                    _rolPermisoRepository.AsignarPermisoARol(id, permiso.idPermiso);
                }
            }

            _rolRepository.ActualizarRol(rolExistente);
            _rolPermisoRepository.GuardarCambios();
            _rolRepository.GuardarCambios();

            _respuesta.StatusCode = HttpStatusCode.OK;
            _respuesta.Message = "Rol actualizado correctamente con sus permisos exactos.";
            return Ok(_respuesta);
        }


        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id)
        {
            if (_usuarioRolRepository.ObtenerUsuariosPorRol(id).Any())
            {
                _respuesta.Success = false;
                _respuesta.Message = "No se puede eliminar el rol porque tiene usuarios asignados.";
                _respuesta.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuesta);
            }
            if (!_rolRepository.ExisteRol(id)) return NotFound();

            _rolRepository.EliminarRol(id);
            _respuesta.StatusCode = HttpStatusCode.NoContent;
            return Ok(_respuesta);
        }
    }
}
