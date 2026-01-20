using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories
{
    public class ConsultorioProfesoresRepositorie : IConsultorioProfesores
    {
        private readonly ContextFacultadDerecho context;

        public ConsultorioProfesoresRepositorie(ContextFacultadDerecho context)
        {
            this.context = context;
        }


        public async Task<List<ConsultorioProfesores>> GetConsultorioProfesores()
        {
            var data = await context.ConsultorioProfesores.ToListAsync();
            return data;
        }

        public async Task<bool> PostConsultorioProfesores(ConsultorioProfesores consultorioProfesores)
        {
            await context.ConsultorioProfesores.AddAsync(consultorioProfesores);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> PutConsultorioProfesores(ConsultorioProfesores consultorioProfesores)
        {
            context.ConsultorioProfesores.Update(consultorioProfesores);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteConsultorioProfesores(int id)
        {
            var consultorioProfesores = await context.ConsultorioProfesores.FindAsync(id);
            if (consultorioProfesores == null)
                return false;
            context.ConsultorioProfesores.Remove(consultorioProfesores);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> SincronizarCalendario(ConsultorioProfesoresCalendarioDto dto)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var existentes = await context.ConsultorioProfesores
                    .Where(x => x.CalendarioId == dto.CalendarioId)
                    .ToListAsync();

                // INSERT (Profesor + Día + Jornada)
                var nuevos = dto.Profesores
                    .Where(p => !existentes.Any(e =>
                        e.ProfesorId == p.ProfesorId &&
                        e.DiaSemana == p.DiaSemana &&
                        e.Jornada == p.Jornada))
                    .Select(p => new ConsultorioProfesores
                    {
                        ProfesorId = p.ProfesorId,
                        DiaSemana = p.DiaSemana,
                        Jornada = p.Jornada,
                        CalendarioId = dto.CalendarioId
                    })
                    .ToList();

                if (nuevos.Any())
                    context.ConsultorioProfesores.AddRange(nuevos);

                // DELETE (los que ya no vienen en el DTO)
                var eliminados = existentes
                    .Where(e => !dto.Profesores.Any(p =>
                        p.ProfesorId == e.ProfesorId &&
                        p.DiaSemana == e.DiaSemana &&
                        p.Jornada == e.Jornada))
                    .ToList();

                if (eliminados.Any())
                    context.ConsultorioProfesores.RemoveRange(eliminados);

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }


    }
}
