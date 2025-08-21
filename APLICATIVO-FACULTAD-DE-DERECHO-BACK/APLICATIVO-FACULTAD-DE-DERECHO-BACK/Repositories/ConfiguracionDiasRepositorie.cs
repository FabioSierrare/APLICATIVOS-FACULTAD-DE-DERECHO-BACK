using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories
{
    public class ConfiguracionDiasRepositorie : IConfiguracionDias
    {
        private readonly ContextFacultadDerecho context;

        public ConfiguracionDiasRepositorie(ContextFacultadDerecho context)
        {
            this.context = context;
        }


        public async Task<List<ConfiguracionDias>> GetConfiguracionDias()
        {
            var data = await context.ConfiguracionDias.ToListAsync();
            return data;
        }

        public async Task<bool> PostConfiguracionDias(ConfiguracionDias configuracionDias)
        {
            await context.ConfiguracionDias.AddAsync(configuracionDias);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> PutConfiguracionDias(ConfiguracionDias configuracionDias)
        {
            context.ConfiguracionDias.Update(configuracionDias);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteConfiguracionDias(int id)
        {
            var configuracionDias = await context.ConfiguracionDias.FindAsync(id);
            if (configuracionDias == null)
                return false;
            context.ConfiguracionDias.Remove(configuracionDias);
            await context.SaveAsync();
            return true;
        }
    }
}
