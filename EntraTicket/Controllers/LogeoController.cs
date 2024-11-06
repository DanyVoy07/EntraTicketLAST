using EntraTicket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EntraTicket.Controllers
{
    [ApiController]
    [Route("Usuario")]
    public class LogeoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LogeoController(IConfiguration configuration) // Constructor con nombre corregido
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult IniciarSesion([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest(new { success = false, message = "Datos de inicio de sesión no proporcionados" });
            }

            var usuario = logeo.DB().FirstOrDefault(x => x.nombre == loginRequest.nombre && x.contraseña == loginRequest.contraseña);

            if (usuario == null)
            {
                return Unauthorized(new { success = false, message = "Credenciales incorrectas" });
            }

            var jwtToken = GenerarTokenJWT(usuario);

            return Ok(new
            {
                success = true,
                message = "Éxito",
                result = jwtToken
            });
        }

        private string GenerarTokenJWT(logeo usuario)
        {
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64),
                new Claim("id", usuario.idusuario.ToString()),
                new Claim("nombre", usuario.nombre),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    // Clase LoginRequest
    public class LoginRequest
    {
        public string nombre { get; set; }
        public string contraseña { get; set; }
    }

    // Clase Jwt (para configuración de JWT desde appsettings.json)
    public class Jwt
    {
        public string Subject { get; set; }
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
