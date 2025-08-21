using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces
{
    public interface ICalendarios
    {
        Task<List<Calendarios>> GetCalendarios();

        Task<bool> PostCalendarios(Calendarios calendarios);


        Task<bool> PutCalendarios(Calendarios calendarios);


        Task<bool> DeleteCalendarios(int id);
        Task<bool> PostCalendarioCompleto(
    Calendarios calendario,
    List<LimitesTurnosConsultorio> limiteTurnos,
    List<ConfiguracionDias> configuracionesDias);

        Task<List<DisponibilidadDTO>> ObtenerDisponibilidadMesAsync(
    int usuarioId,
    int consultorioId,
    int mes,
    int anio);
    }


}
