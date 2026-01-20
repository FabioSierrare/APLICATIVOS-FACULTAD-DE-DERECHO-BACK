using Microsoft.AspNetCore.Mvc;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using System.Diagnostics;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioConsultoriosController : Controller
    {
        public readonly IUsuariosConsultorios usuariosConsultorios;

        public UsuarioConsultoriosController(IUsuariosConsultorios usuariosConsultorios)
        {
            this.usuariosConsultorios = usuariosConsultorios;
        }

        [HttpGet("GetUsuarioConsultorio")]
        public async Task<IActionResult> GetUsuariosConsultorios()
        {
            var sw = Stopwatch.StartNew();
            var data = await usuariosConsultorios.GetUsuariosConsultorios();
            sw.Stop();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> PostUsuariosConsultorios([FromBody] UsuarioConsultorio usuarioConsultorio)
        {
            if (usuarioConsultorio == null)
                return BadRequest("Usuario Consultorio no puede ser nulo");
            var result = await usuariosConsultorios.PostUsuariosConsultorios(usuarioConsultorio);
            if (result)
                return Ok("Usuario Consultorio creado exitosamente");
            else
                return StatusCode(500, "Error al crear el Usuario Consultorio");
        }

        [HttpPut]

        [Route("api/UsuarioConsultorios")]
        public async Task<IActionResult> PutUsuariosConsultorios([FromBody] UsuarioConsultorio usuarioConsultorio)
        {
            if (usuarioConsultorio == null)
                return BadRequest("Usuario Consultorio no puede ser nulo");
            var result = await usuariosConsultorios.PutUsuariosConsultorios(usuarioConsultorio);
            if (result)
                return Ok("Usuario Consultorio actualizado exitosamente");
            else
                return StatusCode(500, "Error al actualizar el Usuario Consultorio");
        }

        [HttpDelete]
        [Route("api/UsuarioConsultorios/{id}")]
        public async Task<IActionResult> DeleteUsuariosConsultorios(int id)
        {
            if (id <= 0)
                return BadRequest("Id no puede ser menor o igual a cero");
            var result = await usuariosConsultorios.DeleteUsuariosConsultorios(id);
            if (result)
                return Ok("Usuario Consultorio eliminado exitosamente");
            else
                return NotFound("Usuario Consultorio no encontrado");
        }
    }
}
