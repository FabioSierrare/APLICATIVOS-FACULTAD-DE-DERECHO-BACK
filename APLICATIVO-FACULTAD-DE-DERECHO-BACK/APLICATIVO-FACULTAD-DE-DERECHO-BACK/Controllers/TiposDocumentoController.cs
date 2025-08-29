using Microsoft.AspNetCore.Mvc;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using System.Diagnostics;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposDocumentoController : Controller
    {
        public readonly ITiposDocumento _tiposDocumento;

        public TiposDocumentoController(ITiposDocumento tiposDocumento)
        {
            _tiposDocumento = tiposDocumento;
        }

        [HttpGet("GetTiposDocumento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTiposDocumento()
        {
            var sw = Stopwatch.StartNew();
            var response = await _tiposDocumento.GetTiposDocumentos();
            sw.Stop();
            Console.WriteLine($"GetTiposDocumento executed in {sw.ElapsedMilliseconds} ms");
            return Ok(response);
        }

        [HttpPost("PostTiposDocumento")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostTiposDocumento([FromBody] TiposDocumento tiposDocumento)
        {
            try
            {
                var response = await _tiposDocumento.PostTiposDocumentos(tiposDocumento);
                if (response)
                {
                    return Ok("Se guardo correctamente el tipo de documento");
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

        [HttpPut("PutTiposDocumento/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutTiposDocumento([FromBody] TiposDocumento tiposDocumento)
        {
            try
            {
                var response = await _tiposDocumento.PutTiposDocumentos(tiposDocumento);
                if (response)
                {
                    return Ok("Se actualizo correctamente el tipo de documento");
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

        [HttpDelete("DeleteTiposDocumento/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTiposDocumento(int id)
        {
            try
            {
                var response = await _tiposDocumento.DeleteTiposDocumentos(id);
                if (response)
                {
                    return Ok("Se elimino correctamente el tipo de documento");
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
