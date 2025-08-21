using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;
namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces
{
    public interface ILimitesTurnosConsultorioRepositorie
    {
        Task<List<LimitesTurnosConsultorio>> GetLimitesTurnosConsultorio();
        Task<bool> PostLimitesTurnosConsultorio(LimitesTurnosConsultorio limitesTurnosConsultorio);
        Task<bool> PutLimitesTurnosConsultorio(LimitesTurnosConsultorio limitesTurnosConsultorio);
        Task<bool> DeleteLimitesTurnosConsultorio(int id);
    }
}
