using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces
{
    public interface ITurnos
    {
        Task<List<Turnos>> GetTurnos();
        Task<bool> PostTurnos(Turnos turnos);
        Task<bool> PutTurnos(Turnos turnos);
        Task<bool> DeleteTurnos(int id);
    }
}
