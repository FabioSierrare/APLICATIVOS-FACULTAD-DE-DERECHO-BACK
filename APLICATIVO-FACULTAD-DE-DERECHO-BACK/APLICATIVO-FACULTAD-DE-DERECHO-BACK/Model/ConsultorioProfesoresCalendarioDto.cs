namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model
{
    public class ConsultorioProfesoresCalendarioDto
    {
        public int CalendarioId { get; set; }
        public List<ConsultorioProfesorDiaDto> Profesores { get; set; } = new();
    }
}
