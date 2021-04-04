using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TBBProject.Core.Web
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRuntimeInfo(this IServiceCollection services)
        {
            var assembly = typeof(AppRuntimeController).GetTypeInfo().Assembly;

            services.AddMvc().AddApplicationPart(assembly).AddControllersAsServices();

            return services;
        }
    }
}
