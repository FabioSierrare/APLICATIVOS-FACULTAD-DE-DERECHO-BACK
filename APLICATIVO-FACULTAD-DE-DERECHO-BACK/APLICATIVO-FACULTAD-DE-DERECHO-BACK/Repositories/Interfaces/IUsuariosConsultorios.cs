using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces
{
    public interface IUsuariosConsultorios
    {
        Task<List<UsuarioConsultorio>> GetUsuariosConsultorios();
        Task<bool> PostUsuariosConsultorios(UsuarioConsultorio usuarioConsultorio);
        Task<bool> PutUsuariosConsultorios(UsuarioConsultorio usuarioConsultorios);
        Task<bool> DeleteUsuariosConsultorios(int id);
    }
}
