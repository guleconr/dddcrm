using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TBBProject.Core.Common;
using TBBProject.Core.Common.Web;

namespace TBBProject.Core.Web
{
    public class MvcSecurityStartup : IAppStartup
    {
        public int Order => 3;

        public void Configure(IApplicationBuilder application)
        {
            application.UseDefaultCorsPolicy();

            //logger.LogInformation("Configuring XFrame Options");
            //application.UsePerzufuntXFrameOptions();

            application.UseTBBProjectHsts();

            application.UseXXssProtectionWithBlockedMode();
            application.UseReferrerPolicies();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultCorsPolicy();
            //services.AddDataProtectionService();
            services.ConfigureTBBProjectHsts();
        }
    }
}
