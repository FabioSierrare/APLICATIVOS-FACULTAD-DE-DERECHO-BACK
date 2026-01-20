namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model
{
    public class infoTurno
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int ConsultorioId { get; set; }
        public DateTime Fecha { get; set; }
        public string Jornada { get; set; }
    }
}
