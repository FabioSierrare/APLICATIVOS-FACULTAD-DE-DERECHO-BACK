using System.Text.Json.Serialization;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model
{
    public class ConsultorioProfesores
    {
        public int Id { get; set; }
        public int ProfesorId { get; set; }
        public int CalendarioId { get; set; }
        public string DiaSemana { get; set; }
        public string Jornada { get; set; } // AM / PM
        [JsonIgnore]
        public Usuarios? Profesor { get; set; }

    }
}
