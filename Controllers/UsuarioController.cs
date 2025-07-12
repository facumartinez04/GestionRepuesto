using AutoMapper;
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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        protected RespuestaAPI _respuestaAPI;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _respuestaAPI = new RespuestaAPI();
        }

        [HttpGet]
        public IActionResult ObtenerUsuarios()
        {
            var lista = _usuarioRepository.ObtenerUsuarios();
            var listaDto = _mapper.Map<ICollection<UsuarioLeerDto>>(lista);

            _respuestaAPI.Result = listaDto;
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            return Ok(_respuestaAPI);
        }

        [HttpGet("{id:int}", Name = "ObtenerUsuario")]
        public IActionResult ObtenerUsuario(int id)
        {
            if (!_usuarioRepository.ExisteUsuario(id))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                _respuestaAPI.Message = "Usuario no encontrado.";
                return NotFound(_respuestaAPI);
            }

            var usuario = _usuarioRepository.ObtenerUsuario(id);
            var usuarioDto = _mapper.Map<UsuarioLeerDto>(usuario);

            _respuestaAPI.Result = usuarioDto;
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            return Ok(_respuestaAPI);
        }

        [HttpPost]
        public IActionResult CrearUsuario([FromBody] UsuarioCrearDto usuarioCrearDto)
        {
            if (usuarioCrearDto == null)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.Message = "Datos inválidos.";
                return BadRequest(_respuestaAPI);
            }

            if (_usuarioRepository.ExisteUsuario(usuarioCrearDto.email))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.Message = "El email ya está registrado.";
                return BadRequest(_respuestaAPI);
            }

            var usuario = _mapper.Map<Usuario>(usuarioCrearDto);
            if (!_usuarioRepository.CrearUsuario(usuario))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.Message = "Error al crear el usuario.";
                return StatusCode(StatusCodes.Status500InternalServerError, _respuestaAPI);
            }

            _respuestaAPI.Result = usuario;
            _respuestaAPI.StatusCode = HttpStatusCode.Created;
            _respuestaAPI.Message = "Usuario creado correctamente.";

            return CreatedAtRoute("ObtenerUsuario", new { id = usuario.id }, _respuestaAPI);
        }

        [HttpPut("{id:int}")]
        public IActionResult ActualizarUsuario(int id, [FromBody] UsuarioCrearDto usuarioDto)
        {
            if (usuarioDto == null || id <= 0)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.Message = "Datos inválidos.";
                return BadRequest(_respuestaAPI);
            }

            if (!_usuarioRepository.ExisteUsuario(id))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                _respuestaAPI.Message = "Usuario no encontrado.";
                return NotFound(_respuestaAPI);
            }

            var usuario = _mapper.Map<Usuario>(usuarioDto);
            usuario.id = id;

            if (!_usuarioRepository.ActualizarUsuario(usuario))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.Message = "Error al actualizar el usuario.";
                return StatusCode(StatusCodes.Status500InternalServerError, _respuestaAPI);
            }

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.Message = "Usuario actualizado correctamente.";
            return Ok(_respuestaAPI);
        }

        [HttpDelete("{id:int}")]
        public IActionResult EliminarUsuario(int id)
        {
            if (!_usuarioRepository.ExisteUsuario(id))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                _respuestaAPI.Message = "Usuario no encontrado.";
                return NotFound(_respuestaAPI);
            }

            if (!_usuarioRepository.EliminarUsuario(id))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.InternalServerError;
                _respuestaAPI.Message = "Error al eliminar el usuario.";
                return StatusCode(StatusCodes.Status500InternalServerError, _respuestaAPI);
            }

            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            _respuestaAPI.Message = "Usuario eliminado correctamente.";
            return Ok(_respuestaAPI);
        }
    }
}
