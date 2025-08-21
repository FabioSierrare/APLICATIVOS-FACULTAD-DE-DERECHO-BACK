using System.ComponentModel.DataAnnotations;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model
{
    public class LimitesTurnosConsultorio
    {
        public int CalendarioId { get; set; }
        public int ConsultorioId { get; set; }
        public int LimiteTurnos { get; set; }
    }
}
