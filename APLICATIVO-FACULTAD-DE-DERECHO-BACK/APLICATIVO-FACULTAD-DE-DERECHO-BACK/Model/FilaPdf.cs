namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model
{
    public class FilaPdf
    {
        public string Jornada { get; set; } = "";
        public infoTurno? Estudiante { get; set; }
        public AsesorInfo? Asesor { get; set; }
    }

}
