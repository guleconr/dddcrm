using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TBBProject.Core.Data
{
    public class MigrationDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var contentRootPath = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
                            .SetBasePath(contentRootPath)
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{environmentName}.json", true);

            builder.AddUserSecrets(typeof(MigrationDbContextFactory).Assembly, optional: true);
            builder.AddEnvironmentVariables();
            var _configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();

            var cs = builder.Build().GetConnectionString("DefaultConnection");

            services.AddData<DataContext>(cs);
            var _serviceProvider = services.BuildServiceProvider();

            return _serviceProvider.GetRequiredService<DataContext>();
        }
    }
}
