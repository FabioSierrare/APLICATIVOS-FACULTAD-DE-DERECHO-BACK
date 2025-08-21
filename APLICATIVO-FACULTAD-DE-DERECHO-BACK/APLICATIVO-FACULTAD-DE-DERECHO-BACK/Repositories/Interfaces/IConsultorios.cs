using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces
{
    public interface IConsultorios
    {
        Task<List<Consultorios>> GetConsultorios();
        Task<bool> PostConsultorios(Consultorios consultorios);
        Task<bool> PutConsultorios(Consultorios consultorios);
        Task<bool> DeleteConsultorios(int id);
    }
}
