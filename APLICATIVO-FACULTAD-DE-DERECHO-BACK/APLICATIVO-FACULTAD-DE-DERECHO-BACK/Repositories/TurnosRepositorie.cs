using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories
{
    public class TurnosRepositorie : ITurnos
    {
        private readonly ContextFacultadDerecho context;

        public TurnosRepositorie(ContextFacultadDerecho context)
        {
            this.context = context;
        }


        public async Task<List<Turnos>> GetTurnos()
        {
            var data = await context.Turnos.ToListAsync();
            return data;
        }

        public async Task<bool> PostTurnos(Turnos turnos)
        {
            await context.Turnos.AddAsync(turnos);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> PutTurnos(Turnos turnos)
        {
            context.Turnos.Update(turnos);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteTurnos(int id)
        {
            var turnos = await context.Turnos.FindAsync(id);
            if (turnos == null)
                return false;
            context.Turnos.Remove(turnos);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> CambioTurnos(List<TurnosId> id, [FromBody] List<UsuarioId> userid)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                if (id.Count < 2)
                    return false;

                var TurnoA = await context.Turnos.FindAsync(id[0].IdTurno);
                var TurnoB = await context.Turnos.FindAsync(id[1].IdTurno);

                if (TurnoA == null || TurnoB == null)
                    return false;

                if (userid.Count > 1)
                {
                    TurnoA.UsuarioId = userid[1].Id;
                    TurnoB.UsuarioId = userid[0].Id;
                }

                await context.SaveAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch(Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }

        }
    
    }
}
