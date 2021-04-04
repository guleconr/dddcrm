using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TBBProject.Core.Common;
using TBBProject.Core.Data;

namespace TBBProject.Core.Web
{
    public class DbStartup : IAppStartup
    {
        public int Order => 2;

        public void Configure(IApplicationBuilder application)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddData<DataContext>(connectionString);
        }
    }
}
