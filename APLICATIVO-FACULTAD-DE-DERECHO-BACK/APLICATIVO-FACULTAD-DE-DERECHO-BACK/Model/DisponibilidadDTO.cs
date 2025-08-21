namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model
{
    public class DisponibilidadDTO
    {
        public DateTime Fecha { get; set; }
        public string DiaSemana { get; set; }
        public int TurnosOcupadosAM { get; set; }
        public int TurnosOcupadosPM { get; set; }
        public int LimiteDiaAM { get; set; }
        public int LimiteDiaPM { get; set; }
        public int LimiteConsultorio { get; set; }
        public int TurnosEstudianteAM { get; set; }
        public int TurnosEstudiantePM { get; set; }
        public bool Bloqueado { get; set; }
    }
}
