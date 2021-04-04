using Microsoft.AspNetCore.Builder;
using TBBProject.Core.Common;
using TBBProject.Core.Localization;

namespace TBBProject.Core.Web
{
    public static class TBBProjectApplicationBuilderExtensions
    {

        public static void ConfigureRequestPipeline(this IApplicationBuilder application) => TBBProjectContext.Current.ConfigureRequestPipeline(application);

        public static void UseTBBProjectResponseCompression(this IApplicationBuilder application) => application.UseResponseCompression();
        public static void UseTBBProjectResponseCache(this IApplicationBuilder application) => application.UseResponseCaching();

        public static void UseTBBProjectStaticFiles(this IApplicationBuilder application) => application.UseFileProviderForStaticFiles();

        public static void UseTBBProjectMvc(this IApplicationBuilder app)
        {
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public static void UseTBBProjectLocalization(this IApplicationBuilder app) => app.UseTBBProjectInternationalization();
    }
}
