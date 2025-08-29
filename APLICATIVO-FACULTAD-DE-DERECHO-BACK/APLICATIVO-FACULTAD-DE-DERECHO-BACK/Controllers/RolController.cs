using Microsoft.AspNetCore.Mvc;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using System.Diagnostics;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RolController : Controller
    {
        private readonly IRol _rolRepository;

        public RolController(IRol rolRepository)
        {
            _rolRepository = rolRepository;
        }

        [HttpGet("GetRol")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetRol()
        {
            var sw = Stopwatch.StartNew();
            var roles = await _rolRepository.GetRol();
            sw.Stop();
            Console.WriteLine($"GetRol executed in {sw.ElapsedMilliseconds} ms");
            return Ok(roles);
        }

        [HttpPost("PostRol")]
        public async Task<IActionResult> PostRol([FromBody] Rol rol)
        {
            try
            {
                var respuesta = await _rolRepository.PostRol(rol);
                if (respuesta)
                {
                    return Ok("Rol creado exitosamente");
                }
                else
                {
                    return BadRequest(respuesta);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutRol/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutRol([FromBody] Rol rol)
        {
            try
            {
                var respuesta = await _rolRepository.PutRol(rol);
                if (respuesta)
                {
                    return Ok("Rol actualizado exitosamente");
                }
                else
                {
                    return BadRequest("Rol no encontrado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteRol/{id}")]
        public async Task<IActionResult> DeleteRol(int id)
        {
            try
            {
                var respuesta = await _rolRepository.DeleteRol(id);
                if (respuesta)
                {
                    return Ok("Rol eliminado exitosamente");
                }
                else
                {
                    return BadRequest(respuesta);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
