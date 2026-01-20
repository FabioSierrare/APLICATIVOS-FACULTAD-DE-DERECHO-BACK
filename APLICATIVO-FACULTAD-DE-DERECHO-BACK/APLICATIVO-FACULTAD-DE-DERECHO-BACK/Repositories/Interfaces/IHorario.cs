using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces
{
    public interface IHorario
    {
        Task<List<List<DiaSemanaDto>>> ObtenerSemanas();
        int ObtenerIndiceSemanaActual(List<List<DiaSemanaDto>> semanas);
        Task<List<DiaSemanaDto>> ObtenerTurnosYasesores(List<DiaSemanaDto> semana);
        List<FilaPdf> ObtenerFilasPorDia(DiaSemanaDto dia);
        Task<Turnos?> ObtenerUltimoNumeroTurnoAntesDe(DateTime fechaInicioSemana);

    }
}
