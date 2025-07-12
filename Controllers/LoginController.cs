using GestionRepuestoAPI.Helpers;
using GestionRepuestoAPI.Modelos;
using GestionRepuestoAPI.Modelos.Dtos;
using GestionRepuestoAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
            var claveActual = usuario.clave;



            if (usuario == null || SeguridadHelper.VerificarPassword(loginDto.clave, usuario.clave) == false)
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


            var rolesClaim = roles.Select(r => new Claim(ClaimTypes.Role, r.descripcion)).ToList();
            var permisosClaim = permisos.Select(p => new Claim("Permiso", p.dataKey)).ToList();



            var claims = new[]
            {
            new Claim(ClaimTypes.Name, usuario.nombreUsuario),
            new Claim("id", usuario.id.ToString()),
            new Claim("email", usuario.email),


                };
            claims = claims.Concat(rolesClaim).ToArray();
            claims = claims.Concat(permisosClaim).ToArray();


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

            _usuarioRepository.EditarRefreshToken(usuario.id, refreshToken, DateTime.Now.AddDays(7));





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

        [Authorize]
        [HttpPost("me")]
        public IActionResult ObtenerUsuarioActual()
        {
            var username = User.Identity?.Name;

            if (string.IsNullOrEmpty(username))
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.Unauthorized;
                _respuestaAPI.Message = "Usuario no autenticado.";
                return Unauthorized(_respuestaAPI);
            }

            var usuario = _usuarioRepository.ObtenerUsuario(username);

            if (usuario == null)
            {
                _respuestaAPI.Success = false;
                _respuestaAPI.StatusCode = HttpStatusCode.NotFound;
                _respuestaAPI.Message = "Usuario no encontrado.";
                return NotFound(_respuestaAPI);
            }

            var rolesUsuario = _usuarioRolRepository.ObtenerRolesDeUsuario(usuario.id)
                .Select(r => _rolRepository.ObtenerRol(r.idRol))
                .Where(r => r != null)
                .ToList();

            var permisosUsuario = _usuarioPermisoRepository.ObtenerPermisosDeUsuario(usuario.id)
                .Select(p => _permisoRepository.ObtenerPermiso(p.idPermiso))
                .Where(p => p != null)
                .ToList();

            var respuesta = new LoginRespuestaDto
            {
                Token = "",
                RefreshToken = "",
                Usuario = usuario.nombreUsuario,
                Roles = rolesUsuario,
                Permisos = permisosUsuario
            };

            _respuestaAPI.Result = respuesta;
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            return Ok(_respuestaAPI);
        }



        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenDto tokenDto)
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


            var rolesClaim = roles.Select(r => new Claim(ClaimTypes.Role, r.descripcion)).ToList();
            var permisosClaim = permisos.Select(p => new Claim("Permiso", p.dataKey)).ToList();

            var claims = new[]
            {
            new Claim(ClaimTypes.Name, usuario.nombreUsuario),
            new Claim("id", usuario.id.ToString()),
            new Claim("email", usuario.email),


                };
            claims = claims.Concat(rolesClaim).ToArray();
            claims = claims.Concat(permisosClaim).ToArray();




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
                RefreshToken = usuario.RefreshToken, // Se mantiene el mismo
                Usuario = usuario.nombreUsuario,
                Roles = roles,
                Permisos = permisos
            };

            _respuestaAPI.Result = respuesta;
            _respuestaAPI.StatusCode = HttpStatusCode.OK;
            return Ok(_respuestaAPI);
        }






    }
}
