using Microsoft.AspNetCore.Mvc;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class LimitesTurnosConsultorioController : Controller
    {
        private readonly ILimitesTurnosConsultorioRepositorie _limitesTurnosConsultorio;

        public LimitesTurnosConsultorioController(ILimitesTurnosConsultorioRepositorie limitesTurnosConsultorio)
        {
            _limitesTurnosConsultorio = limitesTurnosConsultorio;
        }

        [HttpGet("GetLimitesTurnosConsultorio")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetLimitesTurnosConsultorio()
        {
            var response = await _limitesTurnosConsultorio.GetLimitesTurnosConsultorio();
            return Ok(response);
        }

        [HttpPost("PostLimitesTurnosConsultorio")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostLimitesTurnosConsultorio([FromBody] LimitesTurnosConsultorio limitesTurnosConsultorio)
        {
            try
            {
                var response = await _limitesTurnosConsultorio.PostLimitesTurnosConsultorio(limitesTurnosConsultorio);
                if (response)
                {
                    return Ok("Se guardo correctamente el Limite de turnos");
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


        [HttpPut("PutLimitesTurnosConsultorio/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutCalendarios([FromBody] LimitesTurnosConsultorio limitesTurnosConsultorio)
        {
            try
            {
                var response = await _limitesTurnosConsultorio.PutLimitesTurnosConsultorio(limitesTurnosConsultorio);
                if (response)
                {
                    return Ok("Se Actualizo correctamente el Limite de turnos");
                }
                else
                {
                    return NotFound("Limite de turno no encontrado");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteLimitesTurnosConsultorio/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLimitesTurnosConsultorio(int id)
        {
            try
            {
                var response = await _limitesTurnosConsultorio.DeleteLimitesTurnosConsultorio(id);
                if (response)
                {
                    return Ok("Se elimino correctamente el Limite de turnos");
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
