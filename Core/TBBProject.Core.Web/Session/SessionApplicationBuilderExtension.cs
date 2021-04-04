using Microsoft.AspNetCore.Builder;

namespace TBBProject.Core.Web
{
    public static class SessionApplicationBuilderExtension
    {
        public static IApplicationBuilder UseTBBProjectSession(this IApplicationBuilder app)
        {
            app.UseSession();
            return app;
        }
    }
}
