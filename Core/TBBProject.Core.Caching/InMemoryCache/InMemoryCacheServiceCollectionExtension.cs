namespace TBBProject.Core.Caching
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    public static class InMemoryCacheServiceCollectionExtension
    {
        public static IServiceCollection AddInMemoryCache(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<InMemoryCacheOptions>(configuration.GetSection("InMemoryCacheOptions"));
            services.AddSingleton<ICacheProvider, InMemoryCacheProvider>();

            services.AddMemoryCache();

            return services;
        }
    }
}
