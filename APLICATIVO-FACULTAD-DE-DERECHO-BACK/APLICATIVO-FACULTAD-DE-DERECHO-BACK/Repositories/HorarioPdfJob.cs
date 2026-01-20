using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using Microsoft.AspNetCore.Hosting;


public class HorarioPdfJob
{
    private readonly IHorario _horario;
    private readonly ContextFacultadDerecho _context;
    private readonly ICorreo _correo;
    private readonly IWebHostEnvironment _env;


    public HorarioPdfJob(
    IHorario horario,
    ContextFacultadDerecho context,
    ICorreo correo,
    IWebHostEnvironment env)
    {
        _horario = horario;
        _context = context;
        _correo = correo;
        _env = env;
    }

    public async Task Ejecutar()
    {
        List<List<DiaSemanaDto>> semanas;
        int indiceSemana;

        // 1️⃣ Obtener semanas
        semanas = await _horario.ObtenerSemanas();

        // 2️⃣ Obtener índice de semana actual
        try
        {
            indiceSemana = _horario.ObtenerIndiceSemanaActual(semanas);
        }
        catch
        {
            // ⚠️ Última semana → no se envía nada
            return;
        }

        // 3️⃣ Semana a enviar (siguiente)
        var semanaActual = semanas[indiceSemana + 1];
        semanaActual = await _horario.ObtenerTurnosYasesores(semanaActual);

        // 4️⃣ Validación extra (opcional)
        if (!semanaActual.Any())
            return;

        // 5️⃣ Datos de encabezado
        int semanaNumero = indiceSemana + 1;
        int anio = DateTime.Now.Year;
        string semestre = DateTime.Now.Month <= 6 ? "I" : "II";

        foreach (var dia in semanaActual)
        {
            Console.WriteLine(
                $"{dia.Dia} | Turnos: {dia.Turnos?.Count ?? -1} | Asesores: {dia.Asesor?.Count ?? -1}"
            );
        }


        // 6️⃣ Generar PDF
        var document = new TurnosSemanaPdf(
            _env,
            semanaActual,
            semanaNumero,
            anio,
            semestre,
            1
        );

        var pdfBytes = document.GeneratePdf();

        // 7️⃣ Rango de fechas
        var fechaInicio = semanaActual.First().Fecha.Date;
        var fechaFin = semanaActual.Last().Fecha.Date;

        // 8️⃣ Calendario activo
        var calendario = await _context.Calendarios
            .OrderByDescending(c => c.Id)
            .FirstAsync();

        // 9️⃣ Correos
        var correos = await (
            from u in _context.Usuarios
            join t in _context.Turnos
                on u.Id equals t.UsuarioId
            where t.CalendarioId == calendario.Id
               && t.Fecha.Date >= fechaInicio
               && t.Fecha.Date <= fechaFin
            select u.Correo
        ).Distinct().ToListAsync();

        if (!correos.Any())
            return;

        // 🔟 Enviar correo masivo
        var correoMasivo = new CorreoMasivo
        {
            Destinatarios = correos,
            Asunto = $"NOTIFICACION DE TURNO SEMESTRE {semestre} SEMANA N° {semanaNumero} - {anio}",
            Cuerpo = "Se adjunta el horario semanal.",
            Archivo = pdfBytes,
            NombreArchivo = $"Horario_Semana_{semanaNumero}_{anio}.pdf",
            TipoContenido = "application/pdf"
        };

        await _correo.EnviarMismoCorreoMasivo(correoMasivo);
    }
}
