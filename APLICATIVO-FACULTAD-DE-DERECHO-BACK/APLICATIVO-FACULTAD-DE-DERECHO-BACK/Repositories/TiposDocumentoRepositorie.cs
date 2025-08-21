using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories
{
    public class TiposDocumentoRepositorie : ITiposDocumento
    {
        private readonly ContextFacultadDerecho context;

        public TiposDocumentoRepositorie(ContextFacultadDerecho context)
        {
            this.context = context;
        }


        public async Task<List<TiposDocumento>> GetTiposDocumentos()
        {
            var data = await context.TiposDocumento.ToListAsync();
            return data;
        }

        public async Task<bool> PostTiposDocumentos(TiposDocumento tiposDocumento)
        {
            await context.TiposDocumento.AddAsync(tiposDocumento);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> PutTiposDocumentos(TiposDocumento tiposDocumentos)        {
            context.TiposDocumento.Update(tiposDocumentos);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteTiposDocumentos(int id)
        {
            var tiposDocumentos = await context.TiposDocumento.FindAsync(id);
            if (tiposDocumentos == null)
                return false;
            context.TiposDocumento.Remove(tiposDocumentos);
            await context.SaveAsync();
            return true;
        }
    }
}
