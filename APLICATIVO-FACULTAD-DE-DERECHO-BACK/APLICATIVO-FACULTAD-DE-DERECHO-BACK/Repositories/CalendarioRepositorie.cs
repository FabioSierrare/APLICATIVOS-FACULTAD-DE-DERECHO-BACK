using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories
{
    public class CalendarioRepositorie : ICalendarios
    {
        private readonly ContextFacultadDerecho context;

        public CalendarioRepositorie(ContextFacultadDerecho context)
        {
            this.context = context;
        }

         
        public async Task<List<Calendarios>> GetCalendarios()
        {
            var data = await context.Calendarios.ToListAsync();
            return data;
        }

        public async Task<bool> PostCalendarios(Calendarios calendarios)
        {
            await context.Calendarios.AddAsync(calendarios);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> PutCalendarios(Calendarios calendarios)
        {
            context.Calendarios.Update(calendarios);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteCalendarios(int id)
        {
            var calendario = await context.Calendarios.FindAsync(id);
            if (calendario == null)
                return false;
            context.Calendarios.Remove(calendario);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> PostCalendarioCompleto(
    Calendarios calendario,
    List<LimitesTurnosConsultorio> limiteTurnos,
    List<ConfiguracionDias> configuracionesDias)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                // 1️⃣ Guardar calendario primero
                await context.Calendarios.AddAsync(calendario);
                await context.SaveAsync(); // Necesario para obtener calendario.Id

                // 2️⃣ Guardar limites de turnos (independiente)
                // 2️⃣ Guardar limites de turnos
                foreach (var limiteTurno in limiteTurnos)
                {
                    limiteTurno.CalendarioId = calendario.Id; // <- Asignar FK
                    await context.LimitesTurnosConsultorio.AddAsync(limiteTurno);
                }
                await context.SaveAsync();


                // 3️⃣ Guardar configuración de días (usando calendario.Id)
                foreach (var config in configuracionesDias)
                {
                    config.CalendarioId = calendario.Id; // Asignar FK
                    await context.ConfiguracionDias.AddAsync(config);
                }
                await context.SaveAsync();

                // ✅ Confirmar transacción
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                // ❌ Revertir cambios si algo falla
                await transaction.RollbackAsync();
                Console.WriteLine($"Error guardando datos: {ex.Message}");
                return false;
            }
        }


        public async Task<Calendarios> ObtenerCalendarioParaFechaAsync(DateTime fecha)
        {
            // 1) Preferir calendario marcado como "Activo" que contenga la fecha
            var cal = await context.Calendarios
                .Where(c => c.Estado == "Activo" && fecha.Date >= c.FechaInicio && fecha.Date <= c.FechaFin)
                .OrderByDescending(c => c.FechaInicio) // si hay 2, elegir el más reciente por inicio
                .FirstOrDefaultAsync();

            if (cal != null) return cal;

            // 2) Si no existe marcado como activo, buscar cualquier calendario que contenga la fecha
            cal = await context.Calendarios
                .Where(c => fecha.Date >= c.FechaInicio && fecha.Date <= c.FechaFin)
                .OrderByDescending(c => c.FechaInicio)
                .FirstOrDefaultAsync();

            if (cal != null) return cal;

            // 3) Fallback: último creado (solo si no hay ninguno que cubra la fecha)
            return await context.Calendarios
                .OrderByDescending(c => c.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<DisponibilidadDTO>> ObtenerDisponibilidadMesAsync(
    int usuarioId,
    int consultorioId,
    int mes,
    int anio)
        {
            var primerDia = new DateTime(anio, mes, 1);
            var ultimoDia = primerDia.AddMonths(1).AddDays(-1);

            // Obtener el calendario que cubra (preferiblemente) el primer dia del mes
            var calendario = await ObtenerCalendarioParaFechaAsync(primerDia);

            // Si no hay calendario (improbable), devolvemos días marcados como bloqueados o manejamos a gusto
            if (calendario == null)
            {
                // Puedes lanzar excepción o devolver lista con Bloqueado = true
                return Enumerable.Range(1, DateTime.DaysInMonth(anio, mes))
                    .Select(d => new DisponibilidadDTO
                    {
                        Fecha = new DateTime(anio, mes, d),
                        DiaSemana = new DateTime(anio, mes, d).ToString("dddd", new CultureInfo("es-CO")),
                        TurnosOcupadosAM = 0,
                        TurnosOcupadosPM = 0,
                        LimiteDiaAM = 0,
                        LimiteDiaPM = 0,
                        LimiteConsultorio = 0,
                        TurnosEstudianteAM = 0,
                        TurnosEstudiantePM = 0,
                        Bloqueado = true
                    }).ToList();
            }

            // Traer configuración de días para ese calendario
            var configuraciones = await context.ConfiguracionDias
                .Where(cd => cd.CalendarioId == calendario.Id)
                .ToListAsync();

            // Límite del consultorio en ese calendario
            var limiteConsultorio = await context.LimitesTurnosConsultorio
                .FirstOrDefaultAsync(l => l.CalendarioId == calendario.Id && l.ConsultorioId == consultorioId);

            // Traer todos los turnos del consultorio en el rango del mes (para reducir consultas)
            var turnosDelMes = await context.Turnos
                .Where(t => t.CalendarioId == calendario.Id
                            && t.ConsultorioId == consultorioId
                            && t.Fecha.Date >= primerDia.Date
                            && t.Fecha.Date <= ultimoDia.Date)
                .ToListAsync();

            var dias = new List<DisponibilidadDTO>();
            for (var dia = primerDia; dia <= ultimoDia; dia = dia.AddDays(1))
            {
                var diaSemana = dia.ToString("dddd", new CultureInfo("es-CO"));
                var configDia = configuraciones.FirstOrDefault(c => c.DiaSemana.Equals(diaSemana, StringComparison.OrdinalIgnoreCase));

                // Contadores globales
                var ocupadosAM = turnosDelMes.Count(t => t.Fecha.Date == dia.Date && t.Jornada == "AM");
                var ocupadosPM = turnosDelMes.Count(t => t.Fecha.Date == dia.Date && t.Jornada == "PM");

                // Contadores del estudiante
                var estudianteAM = turnosDelMes.Count(t => t.Fecha.Date == dia.Date && t.Jornada == "AM" && t.UsuarioId == usuarioId);
                var estudiantePM = turnosDelMes.Count(t => t.Fecha.Date == dia.Date && t.Jornada == "PM" && t.UsuarioId == usuarioId);

                // Límites
                var maxAM = configDia?.MaxTurnosAM ?? 0;
                var maxPM = configDia?.MaxTurnosPM ?? 0;
                var maxConsultorio = limiteConsultorio?.LimiteTurnos ?? int.MaxValue;

                // Bloqueado si:
                // - la jornada AM/PM alcanza su máximo (según reglas)
                // - o el total del día supera el límite del consultorio
                // - o la fecha está fuera del calendario (fecha < FechaInicio o > FechaFin)
                var fueraDelCalendario = dia.Date < calendario.FechaInicio.Date || dia.Date > calendario.FechaFin.Date;

                var bloqueadoJornadas = (ocupadosAM >= maxAM && ocupadosPM >= maxPM) // ambas completas
                                       || (ocupadosAM + ocupadosPM >= maxConsultorio);

                var bloqueado = fueraDelCalendario || bloqueadoJornadas;

                dias.Add(new DisponibilidadDTO
                {
                    Fecha = dia,
                    DiaSemana = diaSemana,
                    TurnosOcupadosAM = ocupadosAM,
                    TurnosOcupadosPM = ocupadosPM,
                    LimiteDiaAM = maxAM,
                    LimiteDiaPM = maxPM,
                    LimiteConsultorio = maxConsultorio == int.MaxValue ? 0 : maxConsultorio,
                    TurnosEstudianteAM = estudianteAM,
                    TurnosEstudiantePM = estudiantePM,
                    Bloqueado = bloqueado
                });
            }

            return dias;
        }


    }
}
