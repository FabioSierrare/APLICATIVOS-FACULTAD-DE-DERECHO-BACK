using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces
{
    public interface IConsultorioProfesores
    {
        Task<List<ConsultorioProfesores>> GetConsultorioProfesores();
        Task<bool> PostConsultorioProfesores(ConsultorioProfesores consultorioProfesores);
        Task<bool> PutConsultorioProfesores(ConsultorioProfesores consultorioProfesores);
        Task<bool> DeleteConsultorioProfesores(int id);
        Task<bool> SincronizarCalendario(ConsultorioProfesoresCalendarioDto dto);

    }
}
