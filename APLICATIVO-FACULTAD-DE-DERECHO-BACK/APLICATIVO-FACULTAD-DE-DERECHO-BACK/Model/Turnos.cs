namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model
{
    public class Turnos
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ConsultorioId { get; set; }
        public DateTime Fecha { get; set; }
        public string Jornada { get; set; }
        public int CalendarioId { get; set; } 
    }
}
