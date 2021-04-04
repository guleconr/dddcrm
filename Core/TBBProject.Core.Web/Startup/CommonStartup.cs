using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TBBProject.Core.Common;
using TBBProject.Core.Caching;
using TBBProject.Core.Services;

namespace TBBProject.Core.Web
{
    public class CommonStartup : IAppStartup
    {
        public int Order => 1;

        public void Configure(IApplicationBuilder application)
        {
            application.UseTBBProjectResponseCompression();

            application.UseMiddleware<HostingInformationMiddleware>();

            application.UseMiddleware<CorrelationIdMiddleware>();

            application.UseMiddleware<UserIdCookieMiddleware>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

            services.AddHttpContextAccessor();
            services.AddSingleton<ITypeLocator, WebApplicationTypeLocator>();
            services.AddScoped<ICorrelationIdAccessor, CorrelationIdAccessor>();
            services.AddScoped<ICommonContext, CommonContext>();
            services.AddTransient<IClock, Clock>();

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddOptions();

            services.AddSingleton<IWebHelper, WebHelper>();
            services.AddInMemoryCache(configuration);
            //services.AddDataProtectionService();

            services.AddSingleton<JsonSerializerSettings, CompactSerializerSettings>();
            services.AddRuntimeInfo();

            services.AddIntegrationServices();
        }
    }
}
