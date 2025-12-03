using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces
{
    public interface IUsuarios
    {
        Task<List<Usuarios>> GetUsuarios();
        Task<bool> PostUsuarios(Usuarios usuario);
        Task<bool> PutUsuarios(Usuarios usuario);
        Task<bool> DeleteUsuarios(int id);
        Task<Usuarios> GetUsuarioById(int id);
        Task<bool> PostUsuarios(Usuarios usuario, UsuarioConsultorio consultorio);
        Task<bool> PostUsuariosListado(List<UsuarioEstudianteRegistro> usuarios);
        Task<InfoUser> GetInfoUser(int id);
        Task<bool> PutUsuarioEstudiante(Usuarios usuario, UsuarioConsultorio consultorio);
    }
}
