using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces
{
    public interface ICorreo
    {
        Task<bool> EnviarCorreo(Correo correo);                 // Envío de un solo correo
        Task<bool> EnviarCorreosMasivos(List<Correo> correos);
    }
}
