namespace TBBProject.Core.Common.Web
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Cors.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public static class SecurityServiceCollectionExtension
    {
        public static IServiceCollection ConfigureTBBProjectHsts(this IServiceCollection services)
        {
            services.AddHsts(options =>
            {
                options.MaxAge.Add(TimeSpan.FromDays(7));
                options.IncludeSubDomains = true;
            });

            return services;
        }

        public static IServiceCollection ConfigureHttpsRedirection(this IServiceCollection services)
        {
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            });

            return services;
        }

        public static IServiceCollection AddDefaultCorsPolicy(this IServiceCollection services)
        {
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();

            services.AddCors(options =>
            {
                options.AddPolicy("TBBProjectDefaultCorsPolicy", corsBuilder.Build());
            });
            return services;
        }
    }
}
