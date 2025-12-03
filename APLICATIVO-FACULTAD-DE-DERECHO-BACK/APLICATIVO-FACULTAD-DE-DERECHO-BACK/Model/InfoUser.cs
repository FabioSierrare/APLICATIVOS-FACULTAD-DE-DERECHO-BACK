namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model
{
    public class InfoUser
    {
        public Usuarios usuario { get; set; }
        public int consultorioId { get; set; }
        public List<Consultorios> consultorioInfo { get; set; }
        public List<TiposDocumento> tipoDocumento { get; set; }
        public List<Turnos> turno { get; set; }
    }
}
