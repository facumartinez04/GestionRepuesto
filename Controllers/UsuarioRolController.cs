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
    public class UsuarioRolController : ControllerBase
    {
        private readonly IUsuarioRolRepository _usuarioRolRepository;
        private readonly RespuestaAPI _respuestaAPI;

        public UsuarioRolController(IUsuarioRolRepository usuarioRolRepository)
        {
            _usuarioRolRepository = usuarioRolRepository;
            _respuestaAPI = new RespuestaAPI();
        }

        // Obtener roles asignados a un usuario
        [HttpGet("{usuarioId:int}")]
        public IActionResult ObtenerRolesDeUsuario(int usuarioId)
        {
            var roles = _usuarioRolRepository.ObtenerRolesDeUsuario(usuarioId);
            _respuestaAPI.Result = roles;
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            return Ok(_respuestaAPI);
        }

        // Asignar rol a usuario
        [HttpPost("asignar")]
        public IActionResult AsignarRol([FromBody] UsuarioRolDto dto)
        {
            if (!_usuarioRolRepository.AsignarRol(dto.idUsuario, dto.idRol))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "No se pudo asignar el rol (ya existe o error interno)";
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuestaAPI);
            }

            _respuestaAPI.Message = "Rol asignado correctamente.";
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            return Ok(_respuestaAPI);
        }

        // Remover rol de usuario
        [HttpDelete("remover")]
        public IActionResult RemoverRol([FromBody] UsuarioRolDto dto)
        {
            if (!_usuarioRolRepository.RemoverRol(dto.idUsuario, dto.idRol))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.Message = "No se pudo remover el rol (no existe o error interno)";
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_respuestaAPI);
            }

            _respuestaAPI.Message = "Rol removido correctamente.";
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            return Ok(_respuestaAPI);
        }
    }
}
