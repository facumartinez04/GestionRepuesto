using AutoMapper;
using GestionRepuestoAPI.Helpers;
using GestionRepuestoAPI.Modelos;
using GestionRepuestoAPI.Modelos.Dtos;
using GestionRepuestoAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GestionRepuestoAPI.Controllers
{
    [Authorize(Policy = "ModuloUsuarios")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioPermisoRepository _usuarioPermisoRepository;
        private readonly IUsuarioRolRepository _usuarioRolRepository;

        private readonly IMapper _mapper;
        protected RespuestaAPI _respuestaAPI;

        public UsuarioController(IUsuarioRepository usuarioRepository,IUsuarioRolRepository usuarioRolRepository, IUsuarioPermisoRepository usuarioPermisoRepository, IMapper mapper)
        {
            _usuarioPermisoRepository = usuarioPermisoRepository;
            _usuarioRolRepository = usuarioRolRepository;
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

            if (_usuarioRepository.ExisteUsuario(usuarioCrearDto.nombreUsuario))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.Message = "El usuario ya está registrado.";
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
        public IActionResult ActualizarUsuario(int id, [FromBody] UsuarioEditarDto usuarioDto)
        {
            if (usuarioDto == null || id <= 0)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.Message = "Datos inválidos.";
                return BadRequest(_respuestaAPI);
            }

            var username = User.Identity?.Name;


         

            var usuarioExistente = _usuarioRepository.ObtenerUsuario(id);
            if (usuarioExistente == null)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                _respuestaAPI.Message = "Usuario no encontrado.";
                return NotFound(_respuestaAPI);
            }




            if (usuarioDto.nombreUsuario == username && SeguridadHelper.VerificarPassword(usuarioDto.clave, usuarioExistente.clave) == true)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.Message = "No podes autoeditarte.";
                return BadRequest(_respuestaAPI);
            }


            if (string.IsNullOrWhiteSpace(usuarioDto.clave))
            {
                usuarioDto.clave = usuarioExistente.clave;
            }

            else
            {
                usuarioDto.clave = BCrypt.Net.BCrypt.HashPassword(usuarioDto.clave);
            }

            _mapper.Map(usuarioDto, usuarioExistente);


            _usuarioRolRepository.RemoverTodosLosRoles(id);

            _usuarioPermisoRepository.RemoverTodosLosPermisos(id);


            _usuarioRolRepository.GuardarCambios();
            _usuarioPermisoRepository.GuardarCambios();



            if (usuarioDto.usuarioRols != null && usuarioDto.usuarioRols.Any())
            {
                foreach (var rol in usuarioDto.usuarioRols)
                {


                    _usuarioRolRepository.AsignarRol(usuarioExistente.id, rol.idRol);
                }



            }

      

            if (usuarioDto.usuarioPermisos != null && usuarioDto.usuarioPermisos.Any())
            {

                foreach (var permiso in usuarioDto.usuarioPermisos)
                {
                    _usuarioPermisoRepository.AsignarPermiso(usuarioExistente.id, permiso.idPermiso);
                }
            }



            if (!_usuarioRepository.GuardarCambios())
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


            var username = User.Identity?.Name;

            var usuarioget = _usuarioRepository.ObtenerUsuario(id);


            if (usuarioget.nombreUsuario == username)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.Message = "No podes autoeliminarte.";
                return BadRequest(_respuestaAPI);
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
