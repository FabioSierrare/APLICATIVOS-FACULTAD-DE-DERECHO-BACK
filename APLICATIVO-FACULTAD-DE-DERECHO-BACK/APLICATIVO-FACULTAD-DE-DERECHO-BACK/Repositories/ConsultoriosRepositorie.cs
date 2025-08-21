using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories
{
    public class ConsultoriosRepositorie : IConsultorios
    {
        private readonly ContextFacultadDerecho context;

        public ConsultoriosRepositorie(ContextFacultadDerecho context)
        {
            this.context = context;
        }


        public async Task<List<Consultorios>> GetConsultorios()
        {
            var data = await context.Consultorios.ToListAsync();
            return data;
        }

        public async Task<bool> PostConsultorios(Consultorios consultorios)
        {
            await context.Consultorios.AddAsync(consultorios);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> PutConsultorios(Consultorios consultorios)
        {
            context.Consultorios.Update(consultorios);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteConsultorios(int id)
        {
            var consultorios = await context.Consultorios.FindAsync(id);
            if (consultorios == null)
                return false;
            context.Consultorios.Remove(consultorios);
            await context.SaveAsync();
            return true;
        }
    }
}
