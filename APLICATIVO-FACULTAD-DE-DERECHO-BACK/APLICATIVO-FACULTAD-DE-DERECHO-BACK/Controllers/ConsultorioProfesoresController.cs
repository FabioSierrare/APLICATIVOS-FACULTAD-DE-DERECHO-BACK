using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultorioProfesoresController : Controller
    {
        private readonly IConsultorioProfesores _consultorioProfesores;

        public ConsultorioProfesoresController(IConsultorioProfesores consultorioProfesores)
        {
            _consultorioProfesores = consultorioProfesores;
        }

        [HttpGet("GetConsultorioProfesores")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetConsultorioProfesores()
            {
            var response = await _consultorioProfesores.GetConsultorioProfesores();
            return Ok(response);
        }

        [HttpPost("PostConsultorioProfesores")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostConsultorioProfesores([FromBody] ConsultorioProfesores consultorioProfesores)
        {
            try
            {
                var response = await _consultorioProfesores.PostConsultorioProfesores(consultorioProfesores);
                if (response)
                {
                    return Ok("Se guardo correctamente");
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


        [HttpPut("PutConsultorioProfesores/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutConsultorioProfesores([FromBody] ConsultorioProfesores consultorioProfesores)
        {
            try
            {
                var response = await _consultorioProfesores.PutConsultorioProfesores(consultorioProfesores);
                if (response)
                {
                    return Ok("Se Actualizo correctamente");
                }
                else
                {
                    return NotFound("No se encontro para la actualización");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteConsultorioProfesores/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCalendarios(int id)
        {
            try
            {
                var response = await _consultorioProfesores.DeleteConsultorioProfesores(id);
                if (response)
                {
                    return Ok("Se elimino correctamente");
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

        [HttpPut("UpdateConsultorioProfesores")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]   // ✔ Actualización exitosa
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  // ✔ Request inválido
        [ProducesResponseType(StatusCodes.Status404NotFound)]    // ✔ Recurso no existe
        [ProducesResponseType(StatusCodes.Status409Conflict)]    // ✔ Conflicto (duplicados)
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutConsultorioProfesoresCalendario(
         [FromBody] ConsultorioProfesoresCalendarioDto dto)
        {
            if (dto == null)
                return BadRequest();

            var result = await _consultorioProfesores
                .SincronizarCalendario(dto);

            return result ? Ok("Se actualizo correctamente") : BadRequest();
        }

    }
}
