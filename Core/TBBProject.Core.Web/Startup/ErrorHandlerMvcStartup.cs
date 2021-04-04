using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TBBProject.Core.Common;

namespace TBBProject.Core.Web
{
    [ExcludeFromCodeCoverage]
    public class ErrorHandlerMvcStartup : IAppStartup
    {
        public int Order => 0;

        public void Configure(IApplicationBuilder application)
        {
            application.UseApplicationExceptionHandler();
            application.UsePageNotFoundHandler();
            application.UseBadRequestHandler();
            application.UseForbiddenPageHandler();
            application.UseUnAuthorisedHandler();
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }
    }
}
