using System.Text.Json.Serialization;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public int TipoDocumentoId { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
        public int RolId { get; set; }
        [JsonIgnore]
        public Rol? Rol { get; set; }
    }
}
