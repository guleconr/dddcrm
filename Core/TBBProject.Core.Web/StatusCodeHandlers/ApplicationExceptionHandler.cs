using System.Threading.Tasks;
using TBBProject.Core.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace TBBProject.Core.Web
{
    [DebuggerStepThrough]
    public static class ApplicationExceptionHandler
    {
        public static void UseApplicationExceptionHandler(this IApplicationBuilder application)
        {
            var hostingEnvironment = (IHostingEnvironment)application.ApplicationServices.GetService(typeof(IHostingEnvironment));


            application.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = async context =>
                {
                    var correlationIdAccessor = (ICorrelationIdAccessor)application.ApplicationServices.GetService(typeof(ICorrelationIdAccessor));
                    var correlationId = correlationIdAccessor.GetCorrelationId();
                    context.Response.Redirect("/errors/internal-server-error?correlationid=" + correlationId);
                    await Task.CompletedTask;
                }
            });

            application.UseExceptionHandler(handler =>
            {
                handler.Run(context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    if (exception is null)
                    {
                        return Task.CompletedTask;
                    }

                    try
                    {
                        TBBProjectContext.Current.Resolve<ILogger<ErrorMarkerClazz>>().LogError(exception.Message, exception);
                    }
                    finally
                    {
                        throw exception;
                    }
                });
            });
        }
    }
}
