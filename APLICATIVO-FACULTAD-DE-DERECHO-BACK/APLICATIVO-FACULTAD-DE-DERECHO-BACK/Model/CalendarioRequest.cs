namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.Model
{
    public class CalendarioRequest
    {
            public Calendarios Calendarios { get; set; }
            public List<LimitesTurnosConsultorio> LimitesTurnosConsultorio { get; set; }
            public List<ConfiguracionDias> ConfiguracionDias { get; set; }

    }
}
