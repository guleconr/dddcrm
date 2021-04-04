using Microsoft.Extensions.DependencyInjection;

namespace TBBProject.Core.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIntegrationServices(this IServiceCollection services)
        {
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<ISmsService, SmsSender>();
            return services;
        }
    }
}
