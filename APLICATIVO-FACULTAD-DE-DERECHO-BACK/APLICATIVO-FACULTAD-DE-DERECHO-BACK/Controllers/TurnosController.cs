using Microsoft.AspNetCore.Mvc;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnosController : Controller
    {
        public readonly ITurnos _turnos;

        public TurnosController(ITurnos turnos)
        {
            _turnos = turnos;
        }

        [HttpGet("GetTurnos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTurnos()
        {
            var response = await _turnos.GetTurnos();
            return Ok(response);
        }

        [HttpPost("PostTurnos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostTurnos([FromBody] Turnos turnos)
        {
            try
            {
                var response = await _turnos.PostTurnos(turnos);
                if (response)
                {
                    return Ok("Se guardo correctamente el turno");
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

        [HttpPut("PutTurnos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutTurnos([FromBody] Turnos turnos)
        {
            try
            {
                var response = await _turnos.PutTurnos(turnos);
                if (response)
                {
                    return Ok("Se actualizo correctamente el turno");
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

        [HttpDelete("DeleteTurnos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTurnos(int id)
        {
            try
            {
                var response = await _turnos.DeleteTurnos(id);
                if (response)
                {
                    return Ok("Se elimino correctamente el turno");
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
