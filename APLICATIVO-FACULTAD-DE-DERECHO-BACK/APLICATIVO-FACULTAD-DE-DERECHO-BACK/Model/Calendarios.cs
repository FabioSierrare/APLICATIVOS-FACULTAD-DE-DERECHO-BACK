namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model
{
    public class Calendarios
    {
        public int Id { get; set; }
        public int Anio { get; set; }
        public string Semestre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string DiaConciliacion { get; set; }
        public string Estado { get; set; }
    }
}
