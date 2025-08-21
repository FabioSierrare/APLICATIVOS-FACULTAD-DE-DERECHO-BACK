namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model
{
    public class ConfiguracionDias
    {
        public int Id { get; set; }
        public string DiaSemana { get; set; }
        public int MaxTurnosAM { get; set; }
        public int MaxTurnosPM { get; set; }
        public int CalendarioId { get; set; }
    }
}
