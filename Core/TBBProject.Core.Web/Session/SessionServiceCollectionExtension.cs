using System;
using Microsoft.Extensions.DependencyInjection;

namespace TBBProject.Core.Web
{
    public static class SessionServiceCollectionExtension
    { 
        public static IServiceCollection ConfigureTBBProjectSession(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = ".TBBProject.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });

            return services;
        }
    }
}
