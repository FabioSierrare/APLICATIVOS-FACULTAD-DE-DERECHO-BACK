using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces
{
    public interface IRol
    {
        Task<List<Rol>> GetRol();
        Task<bool> PostRol(Rol rol);
        Task<bool> PutRol(Rol rol);
        Task<bool> DeleteRol(int id);
    }
}
