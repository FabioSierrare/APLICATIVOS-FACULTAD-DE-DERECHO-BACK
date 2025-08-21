using Microsoft.AspNetCore.Mvc;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultoriosController : Controller
    {
        public readonly IConsultorios _consultorios;

        public ConsultoriosController(IConsultorios consultorios)
        {
            _consultorios = consultorios;
        }

        [HttpGet("GetConsultorios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetConsultorios()
        {
            var response = await _consultorios.GetConsultorios();
            return Ok(response);
        }

        [HttpPost("PostConsultorios")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostConsultorios([FromBody] Consultorios consultorios)
        {
            try
            {
                var response = await _consultorios.PostConsultorios(consultorios);
                if (response)
                {
                    return Ok("Se guardo correctamente el consultorio");
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutConsultorios/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutConsultorios([FromBody] Consultorios consultorios)
        {
            try
            {
                var response = await _consultorios.PutConsultorios(consultorios);
                if (response)
                {
                    return Ok("Se actualizo correctamente el consultorio");
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteConsultorios/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteConsultorios(int id)
        {
            try
            {
                var response = await _consultorios.DeleteConsultorios(id);
                if (response)
                {
                    return Ok("Se elimino correctamente el consultorio");
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
