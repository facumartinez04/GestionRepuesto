using GestionRepuestoAPI.Modelos;
using GestionRepuestoAPI.Modelos.Dtos;
using GestionRepuestoAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestionRepuestoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioPermisoController : ControllerBase
    {
        private readonly IUsuarioPermisoRepository _usuarioPermisoRepository;
        private readonly RespuestaAPI _respuestaAPI;

        public UsuarioPermisoController(IUsuarioPermisoRepository usuarioPermisoRepository)
        {
            _usuarioPermisoRepository = usuarioPermisoRepository;
            _respuestaAPI = new RespuestaAPI();
        }

        [HttpGet("{usuarioId:int}")]
        public IActionResult ObtenerPermisosDeUsuario(int usuarioId)
        {
            var permisos = _usuarioPermisoRepository.ObtenerPermisosDeUsuario(usuarioId);
            _respuestaAPI.Result = permisos;
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            return Ok(_respuestaAPI);
        }

        [HttpPost("asignar")]
        public IActionResult AsignarPermiso([FromBody] UsuarioPermisoDto dto)
        {
            if (!_usuarioPermisoRepository.AsignarPermiso(dto.idUsuario, dto.idPermiso))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "No se pudo asignar el permiso.";
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuestaAPI);
            }

            _respuestaAPI.Message = "Permiso asignado correctamente.";
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            return Ok(_respuestaAPI);
        }

        [HttpDelete("remover")]
        public IActionResult RemoverPermiso([FromBody] UsuarioPermisoDto dto)
        {
            if (!_usuarioPermisoRepository.RemoverPermiso(dto.idUsuario, dto.idPermiso))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "No se pudo remover el permiso.";
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuestaAPI);
            }

            _respuestaAPI.Message = "Permiso removido correctamente.";
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            return Ok(_respuestaAPI);
        }
    }
}
    