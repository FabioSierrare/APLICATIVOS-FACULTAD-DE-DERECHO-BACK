namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model
{
    public class DiaSemanaDto
    {
        public DateTime Fecha { get; set; }
        public string Dia { get; set; } = string.Empty;
        public List<infoTurno>? Turnos { get; set; }
        public List<AsesorInfo>? Asesor { get; set; }
    }
}
