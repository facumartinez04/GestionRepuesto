using GestionRepuestoAPI.Helpers;
using GestionRepuestoAPI.Modelos;
using GestionRepuestoAPI.Modelos.Dtos;
using GestionRepuestoAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GestionRepuestoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioRolRepository _usuarioRolRepository;
        private readonly IRolRepository _rolRepository;

        private readonly IPermisoRepository _permisoRepository;

        private readonly IUsuarioPermisoRepository _usuarioPermisoRepository;

        private readonly IConfiguration _configuration;
        protected RespuestaAPI _respuestaAPI;

        public LoginController(IUsuarioRepository usuarioRepository,IUsuarioPermisoRepository usuarioPermisoRepository, IUsuarioRolRepository usuarioRolRepository,IRolRepository rolRepository, IPermisoRepository permisoRepository, IConfiguration configuration)
        {

            _rolRepository = rolRepository;
            _permisoRepository = permisoRepository;
            _usuarioPermisoRepository = usuarioPermisoRepository;
            _usuarioRolRepository = usuarioRolRepository;
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
            _respuestaAPI = new RespuestaAPI();
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var usuario = _usuarioRepository.ObtenerUsuario(loginDto.nombreUsuario);



            if (usuario == null || SeguridadHelper.VerificarPassword(loginDto.clave, usuario.clave))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.Unauthorized;
                _respuestaAPI.Message = "Credenciales inválidas.";
                return Unauthorized(_respuestaAPI);
            }


            var busquedaUsuarioRoles = _usuarioRolRepository.ObtenerRolesDeUsuario(usuario.id);


            var busquedaUsuarioPermisos = _usuarioPermisoRepository.ObtenerPermisosDeUsuario(usuario.id);


            List<Rol> roles = new List<Rol>();

            foreach (var item in busquedaUsuarioRoles)
            {
                var rol = _rolRepository.ObtenerRol(item.idRol);
                if (rol != null)
                {
                    roles.Add(rol);
                }
            }

            List<Permiso> permisos = new List<Permiso>();
            foreach (var item in busquedaUsuarioPermisos)
            {
                var permiso = _permisoRepository.ObtenerPermiso(item.idPermiso);
                if (permiso != null)
                {
                    permisos.Add(permiso);
                }
            }





            var claims = new[]
            {
        new Claim(ClaimTypes.Name, usuario.nombreUsuario),
        new Claim("id", usuario.id.ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = GenerarRefreshToken();
            usuario.RefreshToken = refreshToken;
            usuario.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            _usuarioRepository.ActualizarUsuario(usuario); 




            var respuesta = new LoginRespuestaDto
            {
                Token = tokenString,
                RefreshToken = refreshToken,
                Usuario = usuario.nombreUsuario,
                Roles = roles,
                Permisos = permisos

            };

            _respuestaAPI.Result = respuesta;
            _respuestaAPI.StatusCode = HttpStatusCode.OK;

            return Ok(_respuestaAPI);
        }



        private string GenerarRefreshToken()
        {
            var randomBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }


        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] LoginRespuestaDto tokenDto)
        {
            if (tokenDto == null || string.IsNullOrEmpty(tokenDto.Usuario) || string.IsNullOrEmpty(tokenDto.RefreshToken))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.BadRequest;
                _respuestaAPI.Message = "Datos inválidos.";
                return BadRequest(_respuestaAPI);
            }

            var usuario = _usuarioRepository.ObtenerUsuario(tokenDto.Usuario);

            if (usuario == null ||
                usuario.RefreshToken != tokenDto.RefreshToken ||
                usuario.RefreshTokenExpiryTime <= DateTime.Now)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.Unauthorized;
                _respuestaAPI.Message = "Refresh token inválido o expirado.";
                return Unauthorized(_respuestaAPI);
            }

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, usuario.nombreUsuario),
        new Claim("id", usuario.id.ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var nuevoToken = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(nuevoToken);

            var respuesta = new LoginRespuestaDto
            {
                Token = tokenString,
                RefreshToken = usuario.RefreshToken,
                Usuario = usuario.nombreUsuario
            };

            _respuestaAPI.Result = respuesta;
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            return Ok(_respuestaAPI);
        }

    }
}
