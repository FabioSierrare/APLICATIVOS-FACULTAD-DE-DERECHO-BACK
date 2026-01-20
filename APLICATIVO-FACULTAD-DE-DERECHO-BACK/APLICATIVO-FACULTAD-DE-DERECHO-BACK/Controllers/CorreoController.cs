using Microsoft.AspNetCore.Mvc;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorreoController : ControllerBase
    {
        private readonly ICorreo _correoRepository;

        public CorreoController(ICorreo correoRepository)
        {
            _correoRepository = correoRepository;
        }

        // ✅ Enviar un solo correo
        [HttpPost("EnviarCorreo")]
        public async Task<IActionResult> EnviarCorreo([FromBody] Correo correo)
        {
            var enviado = await _correoRepository.EnviarCorreo(correo);
            if (enviado)
                return Ok("Correo enviado exitosamente");
            else
                return BadRequest("Error al enviar el correo");
        }

        // ✅ Enviar correos masivos (contenido diferente para cada usuario)
        [HttpPost("EnviarCorreosMasivos")]
        public async Task<IActionResult> EnviarCorreosMasivos([FromBody] List<Correo> correos)
        {
            var enviados = await _correoRepository.EnviarCorreosMasivos(correos);
            if (enviados)
                return Ok("Correos enviados exitosamente");
            else
                return BadRequest("Error al enviar los correos");
        }

        [HttpPost("EnviarCorreoMasivoMismoContenido")]
        public async Task<IActionResult> EnviarCorreoMasivoMismoContenido(
             [FromBody] CorreoMasivo correoMasivo)
        {
            var enviado = await _correoRepository.EnviarMismoCorreoMasivo(correoMasivo);

            if (enviado)
                return Ok("Correo masivo enviado exitosamente");
            else
                return BadRequest("Error al enviar el correo masivo");
        }

    }
}
