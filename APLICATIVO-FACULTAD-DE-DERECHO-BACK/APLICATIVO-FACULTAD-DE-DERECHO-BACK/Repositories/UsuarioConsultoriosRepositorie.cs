using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories
{
    public class UsuariosConsultoriosRepositorie : IUsuariosConsultorios
    {
        private readonly ContextFacultadDerecho context;

        public UsuariosConsultoriosRepositorie(ContextFacultadDerecho context)
        {
            this.context = context;
        }


        public async Task<List<UsuarioConsultorio>> GetUsuariosConsultorios()
        {
            var data = await context.UsuarioConsultorio.ToListAsync();
            return data;
        }

        public async Task<bool> PostUsuariosConsultorios(UsuarioConsultorio usuarioConsultorio)
        {
            await context.UsuarioConsultorio.AddAsync(usuarioConsultorio);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> PutUsuariosConsultorios(UsuarioConsultorio usuarioConsultorio)
        {
            context.UsuarioConsultorio.Update(usuarioConsultorio);
            await context.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteUsuariosConsultorios(int id)
        {
            var usuarioConsultorio = await context.UsuarioConsultorio.FindAsync(id);
            if (usuarioConsultorio == null)
                return false;
            context.UsuarioConsultorio.Remove(usuarioConsultorio);
            await context.SaveAsync();
            return true;
        }
    }
}
