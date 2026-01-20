using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using Microsoft.EntityFrameworkCore;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using System.Globalization;
using System.Threading.Tasks;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories
{
    public class HorarioRepositorie : IHorario
    {
        private readonly ContextFacultadDerecho context;

        public HorarioRepositorie(ContextFacultadDerecho Context)
        {
            this.context = Context;
        }

        public async Task<List<List<DiaSemanaDto>>> ObtenerSemanas()
        {
            var calendario = await context.Calendarios
                .OrderByDescending(c => c.FechaInicio)
                .FirstOrDefaultAsync();

            var inicio = calendario.FechaInicio.Date;
            var fin = calendario.FechaFin.Date;

            var semanas = new List<List<DiaSemanaDto>>();
            var semanaActual = new List<DiaSemanaDto>();

            var fecha = inicio;

            var diasSemana = new[]
            {
        "Domingo",
        "Lunes",
        "Martes",
        "Miércoles",
        "Jueves",
        "Viernes",
        "Sábado"
    };

            while (fecha <= fin)
            {
                var diaNombre = diasSemana[(int)fecha.DayOfWeek];

                var esDiaLaboral =
                    diaNombre != "Sábado" &&
                    diaNombre != "Domingo" &&
                    diaNombre != calendario.DiaConciliacion;

                if (esDiaLaboral)
                {
                    semanaActual.Add(new DiaSemanaDto
                    {
                        Fecha = fecha,
                        Dia = diaNombre
                    });
                }

                // Cerrar semana al llegar a viernes o al final del calendario
                if (diaNombre == "Viernes" || fecha == fin)
                {
                    if (semanaActual.Count > 0)
                    {
                        semanas.Add(semanaActual);
                        semanaActual = new List<DiaSemanaDto>();
                    }
                }

                fecha = fecha.AddDays(1);
            }

            return semanas;
        }

        public int ObtenerIndiceSemanaActual(List<List<DiaSemanaDto>> semanas)
        {
            var hoy = DateTime.Today;

            for(int i = 0; i < semanas.Count; i++)
{
                var semana = semanas[i];
                var esLaUltimaSemana = semanas.Count - 1 == i;

                var inicio = semana.First().Fecha.Date;

                // Calcular el domingo de esa semana
                int diasHastaDomingo = DayOfWeek.Sunday - inicio.DayOfWeek;
                if (diasHastaDomingo < 0)
                    diasHastaDomingo += 7;

                var fin = inicio.AddDays(diasHastaDomingo);

                if (hoy >= inicio && hoy <= fin && !esLaUltimaSemana)
                    return i; // índice base 0
            }


            throw new Exception("Te encuentras en la ultima semana");
        }

        public async Task<List<DiaSemanaDto>> ObtenerTurnosYasesores(List<DiaSemanaDto> semana)
        {
            //Obtener los turnos y asesores para cada día de la semana

            //Obtener turnos

            foreach (var dia in semana)
            {
                var turnosDelDia = await (from u in context.Usuarios
                                          join t in context.Turnos on u.Id equals t.UsuarioId
                                          where t.Fecha.Date == dia.Fecha.Date && t.CalendarioId == context.Calendarios.OrderByDescending(cal => cal.FechaInicio).FirstOrDefault().Id
                                          select new infoTurno
                                          {
                                              Id = t.Id,
                                              Nombre = u.Nombre,
                                              Fecha = t.Fecha,
                                              ConsultorioId = t.ConsultorioId,
                                              Jornada = t.Jornada
                                          }).OrderBy(x => x.Jornada).ToListAsync();
                //Asignar los turnos al día
                dia.Turnos = turnosDelDia; // Descomenta si DiaSemanaDto tiene una propiedad Turnos
            }

            //obtener asesores
            foreach (var dia in semana)
            {
                var asesor = await (from u in context.Usuarios
                                    join uc in context.ConsultorioProfesores on u.Id equals uc.ProfesorId
                                    where uc.CalendarioId == context.Calendarios.OrderByDescending(cal => cal.FechaInicio).FirstOrDefault().Id && uc.DiaSemana == dia.Dia
                                    select new AsesorInfo
                                    {
                                        Nombre = u.Nombre,
                                        Jornada = uc.Jornada
                                    }).OrderBy(x => x.Jornada).ToListAsync();

                //Asignar los asesores al día
                dia.Asesor = asesor; // Descomenta si DiaSemanaDto tiene una propiedad Asesor
            }

            return semana;
        }

        public List<FilaPdf> ObtenerFilasPorDia(DiaSemanaDto dia)
        {
            var jornadas = new[] { "AM", "PM" };
            var filas = new List<FilaPdf>();

            foreach (var jornada in jornadas)
            {
                var estudiantes = dia.Turnos
                    .Where(t => t.Jornada == jornada)
                    .ToList();

                var asesores = dia.Asesor
                    .Where(a => a.Jornada == jornada)
                    .ToList();

                var max = Math.Max(estudiantes.Count, asesores.Count);

                for (int i = 0; i < max; i++)
                {
                    filas.Add(new FilaPdf
                    {
                        Jornada = jornada,
                        Estudiante = estudiantes.ElementAtOrDefault(i),
                        Asesor = asesores.ElementAtOrDefault(i)
                    });
                }
            }

            return filas;
        }

        public async Task<Turnos?> ObtenerUltimoNumeroTurnoAntesDe(DateTime fechaInicioSemana)
        {
            var ultimoTurno = await context.Turnos
                .Where(t => t.Fecha < fechaInicioSemana)
                .OrderByDescending(t => t.Fecha)
                .ThenByDescending(t => t.Jornada)
                .FirstOrDefaultAsync();

            return ultimoTurno;
        }
    }
}
