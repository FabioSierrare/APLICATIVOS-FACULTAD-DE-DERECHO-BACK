using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories
{
    public class LimitesTurnosConsultorioRepositorie : ILimitesTurnosConsultorioRepositorie
    {
        private readonly ContextFacultadDerecho context;

        public LimitesTurnosConsultorioRepositorie(ContextFacultadDerecho context)
        {
            this.context = context;
        }

        public async Task<List<LimitesTurnosConsultorio>> GetLimitesTurnosConsultorio()
        {
            var data = await context.LimitesTurnosConsultorio.ToListAsync();
            return data;
        }

        public async Task<bool> PostLimitesTurnosConsultorio(LimitesTurnosConsultorio LimitesTurnosConsultorio)
        {
            await context.LimitesTurnosConsultorio.AddAsync(LimitesTurnosConsultorio);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> PutLimitesTurnosConsultorio(LimitesTurnosConsultorio LimitesTurnosConsultorio)
        {
            context.LimitesTurnosConsultorio.Update(LimitesTurnosConsultorio);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteLimitesTurnosConsultorio(int id)
        {
            var limitesTurnosConsultorio = await context.LimitesTurnosConsultorio.FindAsync(id);
            if (limitesTurnosConsultorio == null)
                return false;
            context.LimitesTurnosConsultorio.Remove(limitesTurnosConsultorio);
            await context.SaveAsync();
            return true;
        }


    }
}
