namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model
{
    public class CorreoMasivo
    {
        public List<string> Destinatarios { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }

        // Adjuntos
        public byte[] Archivo { get; set; }
        public string NombreArchivo { get; set; }
        public string TipoContenido { get; set; } // application/pdf
    }

}
