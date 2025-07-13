using GestionRepuestoAPI.Modelos;
using GestionRepuestoAPI.Modelos.Dtos;
using GestionRepuestoAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestionRepuestoAPI.Controllers
{

    [Authorize(Policy = "ModuloRoles")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolPermisoController : ControllerBase
    {
        private readonly IRolPermisoRepository _rolPermisoRepository;
        private readonly RespuestaAPI _respuestaAPI;

        public RolPermisoController(IRolPermisoRepository rolPermisoRepository)
        {
            _rolPermisoRepository = rolPermisoRepository;
            _respuestaAPI = new RespuestaAPI();
        }

        [HttpGet("{rolId:int}")]
        public IActionResult ObtenerPermisosDelRol(int rolId)
        {
            var permisos = _rolPermisoRepository.ObtenerPermisosDelRol(rolId);
            _respuestaAPI.Result = permisos;
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            return Ok(_respuestaAPI);
        }

        [HttpPost("asignar")]
        public IActionResult AsignarPermisoARol([FromBody] RolPermisoDto dto)
        {
            if (!_rolPermisoRepository.AsignarPermisoARol(dto.idRol, dto.idPermiso))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "No se pudo asignar el permiso al rol.";
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuestaAPI);
            }

            _respuestaAPI.Message = "Permiso asignado correctamente al rol.";
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            return Ok(_respuestaAPI);
        }

        [HttpDelete("remover")]
        public IActionResult RemoverPermisoDelRol([FromBody] RolPermisoDto dto)
        {
            if (!_rolPermisoRepository.RemoverPermisoDelRol(dto.idRol, dto.idPermiso))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "No se pudo remover el permiso del rol.";
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuestaAPI);
            }

            _respuestaAPI.Message = "Permiso removido correctamente del rol.";
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            return Ok(_respuestaAPI);
        }
    }
}
