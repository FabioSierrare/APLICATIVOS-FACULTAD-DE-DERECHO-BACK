using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories.Interfaces;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Repositories;
using APLICATIVO_FACULTAD_DE_DERECHO_BACK.Context;
using Microsoft.EntityFrameworkCore;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, IConfiguration _configuration)
        {
            // Obtener la cadena de conexión desde la configuración
            string connectionString = _configuration["ConnectionStrings:SQLConnectionStrings"];

            // Registrar el DbContext para la base de datos
            services.AddDbContext<ContextFacultadDerecho>(options => options.UseSqlServer(connectionString));

            // Registrar interfaces sin el sufijo Repository
            services.AddScoped<ICalendarios, CalendarioRepositorie>();
            services.AddScoped<IConfiguracionDias, ConfiguracionDiasRepositorie>();
            services.AddScoped<IConsultorios, ConsultoriosRepositorie>();
            services.AddScoped<ITiposDocumento, TiposDocumentoRepositorie>();
            services.AddScoped<ITurnos, TurnosRepositorie>();
            services.AddScoped<IUsuarios, UsuariosRepositorie>();
            services.AddScoped<ILimitesTurnosConsultorioRepositorie, LimitesTurnosConsultorioRepositorie>();
            services.AddScoped<IUsuariosConsultorios, UsuariosConsultoriosRepositorie>();
            services.AddScoped<IRol, RolRepositorie>();

            return services;
        }
    }
}
