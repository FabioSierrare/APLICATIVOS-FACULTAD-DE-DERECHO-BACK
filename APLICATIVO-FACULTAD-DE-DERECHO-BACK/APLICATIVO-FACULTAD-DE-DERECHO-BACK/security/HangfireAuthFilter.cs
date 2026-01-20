using Hangfire.Dashboard;

namespace APLICATIVO_FACULTAD_DE_DERECHO_BACK.security
{
    public class HangfireAuthFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            return httpContext.User.Identity?.IsAuthenticated == true;
        }
    }
}
