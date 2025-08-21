using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces
{
    public interface IConfiguracionDias
    {
        Task<List<ConfiguracionDias>> GetConfiguracionDias();
        Task<bool> PostConfiguracionDias(ConfiguracionDias configuracionDias);
        Task<bool> PutConfiguracionDias(ConfiguracionDias configuracionDias);
        Task<bool> DeleteConfiguracionDias(int id);
    }
}
