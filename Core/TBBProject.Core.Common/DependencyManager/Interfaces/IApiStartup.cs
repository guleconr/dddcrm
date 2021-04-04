namespace TBBProject.Core.Common
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Diagnostics.CodeAnalysis;
    
    public interface IApiStartup
    {
        int Order { get; }

        void ConfigureServices(IServiceCollection services, IConfiguration configuration, ITypeLocator typeLocator);

        void Configure(IApplicationBuilder application);
    }
}
