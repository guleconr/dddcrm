using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace TBBProject.Core.Data
{
    public static class ServiceCollectionExtensions
    {
        private static void AddDataInternal<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext
        {
            var hostingEnvironment = services.BuildServiceProvider().GetService<IHostingEnvironment>();

            services.AddOptions();

            services.AddDbContextPool<TContext>(options =>
                options.UseSqlServer(
                    connectionString,
                    providerOptions => providerOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(3),
                        null
                    )
            ));

            //services.AddDbContext<TContext>(options =>
            //{
            //   options.UseSqlServer(connectionString);
            //});
        }

        public static void AddData<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext, IDataContext
        {
            services.AddDataInternal<TContext>(connectionString);
            services.AddScoped<IUnitOfWork, UnitofWork>();
            services.AddScoped<IDataContext, TContext>();
        }
    }
}