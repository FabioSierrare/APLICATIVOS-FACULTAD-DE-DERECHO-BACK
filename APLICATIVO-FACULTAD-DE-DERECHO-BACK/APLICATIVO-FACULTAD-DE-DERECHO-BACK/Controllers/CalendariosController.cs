using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CalendariosController : Controller
    {
        private readonly ICalendarios _calendarios;

        public CalendariosController(ICalendarios calendarios)
        {
            _calendarios = calendarios;
        }

        [HttpGet("GetCalendarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCalendarios()
        {
            var response = await _calendarios.GetCalendarios();
            return Ok(response);
        }

        [HttpPost("PostCalendarios")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostCalendarios([FromBody] Calendarios calendarios)
        {
            try
            {
                var response = await _calendarios.PostCalendarios(calendarios);
                if (response)
                {
                    return Ok("Se guardo correctamente el calendario");
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


        [HttpPut("PutCalendarios/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutCalendarios([FromBody] Calendarios calendarios)
        {
            try
            {
                var response = await _calendarios.PutCalendarios(calendarios);
                if (response)
                {
                    return Ok("Se Actualizo correctamente el calendario");
                }
                else
                {
                    return NotFound("Calendario no encontrado");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCalendarios/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCalendarios(int id)
        {
            try
            {
                var response = await _calendarios.DeleteCalendarios(id);
                if (response)
                {
                    return Ok("Se elimino correctamente el calendario");
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

        [HttpPost("PostTodoForm")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GuardarCalendarioCompleto([FromBody] CalendarioRequest request)
        {
            try
            {
                var exito = await _calendarios.PostCalendarioCompleto(
                request.Calendarios,
                request.LimitesTurnosConsultorio,
                request.ConfiguracionDias
            );

                if (exito)
                    return Ok("Datos guardados correctamente.");
                else
                    return BadRequest("Error guardando datos.");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("mes")]
        public async Task<IActionResult> GetDisponibilidadMes(int usuarioId, int consultorioId, int mes, int anio)
        {
            var lista = await _calendarios.ObtenerDisponibilidadMesAsync(usuarioId, consultorioId, mes, anio);
            return Ok(lista);
        }
    }
}
