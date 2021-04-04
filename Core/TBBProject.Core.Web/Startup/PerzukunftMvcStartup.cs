using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using TBBProject.Core.Common;

namespace TBBProject.Core.Web
{
    public class TBBProjectMvcStartup : IAppStartup
    {
        public int Order => 999;

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureResponseCache();
            services.ConfigureTBBProjectHealth();
            services.ConfigureTBBProjectMvcLocalization();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.ConfigureTBBProjectSession();
            var mvcCore = services.AddMvc();
            mvcCore.Configure(services);
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseTBBProjectLocalization();
            application.UseTBBProjectStaticFiles();
            application.UseTBBProjectResponseCache();
            application.UseCookiePolicy();
            application.UseTBBProjectSession();
            application.UseTBBProjectMvc();
        }
    }
}
