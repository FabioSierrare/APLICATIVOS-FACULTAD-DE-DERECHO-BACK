using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using System.Diagnostics;


namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class HorarioController : Controller
    {
        private readonly IHorario _horario;
        private readonly ContextFacultadDerecho _context;

        public HorarioController(IHorario horario, ContextFacultadDerecho context)
        {
            _horario = horario;
            _context = context;
        }

        //[HttpGet("semana/pdf")]
        //public async Task<IActionResult> GenerarPdfSemanaActual()
        //{
        //    // 1️⃣ Obtener semanas
        //    var semanas = await _horario.ObtenerSemanas();

        //    // 2️⃣ Obtener índice semana actual
        //    int indiceSemana;
        //    try
        //    {
        //        indiceSemana = _horario.ObtenerIndiceSemanaActual(semanas);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //    // 3️⃣ Obtener semana actual
        //    var semanaActual = semanas[indiceSemana + 1];

        //    // 4️⃣ Cargar turnos y asesores
        //    semanaActual = await _horario.ObtenerTurnosYasesores(semanaActual);

        //    // 5️⃣ Datos de encabezado
        //    int semanaNumero = indiceSemana + 1;
        //    int anio = DateTime.Now.Year;
        //    string semestre = DateTime.Now.Month <= 6 ? "I" : "II";
        //    var calendario = await _context.Calendarios
        //    .OrderByDescending(c => c.Id)
        //    .FirstOrDefaultAsync();

        //    if (calendario == null)
        //        return BadRequest("No existe un calendario activo.");

        //    var turnos = await _context.Turnos
        //        .Where(t => t.CalendarioId == calendario.Id)
        //        .OrderBy(t => t.Fecha)
        //        .ThenBy(t => t.Jornada)
        //        .ToListAsync();

        //    var ultimo = await _horario.ObtenerUltimoNumeroTurnoAntesDe(
        //        semanaActual.First().Fecha
        //    );

        //    int ultimoturno = 1;

        //    if (ultimo != null)
        //    {
        //        var index = turnos.FindIndex(t => t.Id == ultimo.Id);
        //        ultimoturno = index >= 0 ? index + 2 : 1;
        //    }

        //    var FechaInicioSemana = semanaActual.First().Fecha;
        //    var FechaFinSemana = semanaActual.Last().Fecha;
        //    // 6️⃣ Crear documento PDF
        //    var document = new TurnosSemanaPdf(
        //        semanaActual,
        //        semanaNumero,
        //        anio,
        //        semestre,
        //        ultimoturno
        //    );

        //    var correos = await (from u in _context.Usuarios
        //                         join t in _context.Turnos.Where(t => t.Fecha.Date >= FechaInicioSemana.Date && t.Fecha.Date <= FechaFinSemana.Date && t.CalendarioId == _context.Calendarios.OrderByDescending(cal => cal.FechaInicio).FirstOrDefault().Id) on u.Id equals t.UsuarioId
        //                         select u.Correo
        //                         ).Distinct().ToListAsync();

        //    // 7️⃣ Generar PDF en memoria
        //    var pdfBytes = document.GeneratePdf();

        //    // 8️⃣ Retornar archivo
        //    return File(
        //        pdfBytes,
        //        "application/pdf",
        //        $"Turnos_Semana_{semanaNumero}_{anio}.pdf"
        //    );
        //}

        
    }
}
