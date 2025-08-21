using Microsoft.AspNetCore.Mvc;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ContextFacultadDerecho _context;
        private readonly IConfiguration _configuration;

        public AuthController(ContextFacultadDerecho context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Login login)
        {
            if (login == null
                || string.IsNullOrEmpty(login.Correo)
                || string.IsNullOrEmpty(login.Contrasena))
            {
                return BadRequest("Petición inválida");
            }

            var usuario = _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefault(u => u.Correo == login.Correo && u.Contrasena == login.Contrasena);

            if (usuario == null)
                return Unauthorized("Correo o contraseña incorrectos");

            var claims = new List<Claim>
            {
                new Claim("Correo", usuario.Correo),
                new Claim("Rol", usuario.Rol.Nombre), // Nombre del rol desde la FK
                new Claim("Id", usuario.Id.ToString())
            };

            var secretKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(
                secretKey,
                SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler()
                .WriteToken(tokenOptions);

            return Ok(new
            {
                Token = tokenString,
            });
        }
    }
}
