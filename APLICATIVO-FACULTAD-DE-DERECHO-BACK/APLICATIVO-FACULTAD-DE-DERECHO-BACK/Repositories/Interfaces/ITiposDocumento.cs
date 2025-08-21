using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces
{
    public interface ITiposDocumento
    {
        Task<List<TiposDocumento>> GetTiposDocumentos();
        Task<bool> PostTiposDocumentos(TiposDocumento tiposDocumento);
        Task<bool> PutTiposDocumentos(TiposDocumento tiposDocumento);
        Task<bool> DeleteTiposDocumentos(int id);
    }
}
