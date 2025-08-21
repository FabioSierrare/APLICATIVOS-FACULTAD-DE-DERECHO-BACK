using Microsoft.AspNetCore.Mvc;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ConfiguracionDiasController : Controller
    {
        public readonly IConfiguracionDias _configuracionDias;

        public ConfiguracionDiasController(IConfiguracionDias configuracionDias)
        {
            _configuracionDias = configuracionDias;
        }

        [HttpGet("GetConfiguracionDias")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetConfiguracionDias()
        {
            var response = await _configuracionDias.GetConfiguracionDias();
            return Ok(response);
        }

        [HttpPost("PostConfiguracionDias")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PostConfiguracionDias([FromBody] ConfiguracionDias configuracionDias)
        {
            try
            {
                var response = await _configuracionDias.PostConfiguracionDias(configuracionDias);
                if (response)
                {
                    return Ok("Se guardo correctamente la configuracion de dias");
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

        [HttpPut("PutConfiguracionDias/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutConfiguracionDias([FromBody] ConfiguracionDias configuracionDias)
        {
            try
            {
                var response = await _configuracionDias.PutConfiguracionDias(configuracionDias);
                if (response)
                {
                    return Ok("Se actualizo correctamente la configuracion de dias");
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

        [HttpDelete("DeleteConfiguracionDias/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteConfiguracionDias(int id)
        {
            try
            {
                var response = await _configuracionDias.DeleteConfiguracionDias(id);
                if (response)
                {
                    return Ok("Se elimino correctamente la configuracion de dias");
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
