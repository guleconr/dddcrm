namespace TBBProject.Core.Common
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    
    public interface IAppStartup
    {
        int Order { get; }

        void ConfigureServices(IServiceCollection services);

        void Configure(IApplicationBuilder application);
    }
}
