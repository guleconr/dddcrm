using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TBBProject.Core.Common;

namespace TBBProject.Core.Web
{
    public static class AuditServiceCollectionExtensions
    {
        public static IServiceCollection AddAudit(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAuditHelper), typeof(AuditHelper));
            services.AddScoped(typeof(IAuditStore), typeof(AuditStore));
            services.AddScoped(typeof(AuditActionFilter));
            services.AddSingleton<IJsonSerializer, Common.JsonSerializer>();

            services.AddMvc(options => options.Filters.Add(typeof(AuditActionFilter)));

            return services;
        }
    }
}
