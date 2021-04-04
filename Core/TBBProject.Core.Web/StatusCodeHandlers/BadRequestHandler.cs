using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TBBProject.Core.Common;

namespace TBBProject.Core.Web
{
    public static class BadRequestHandler
    {
        public static void UseBadRequestHandler(this IApplicationBuilder application)
        {
            application.UseStatusCodePages(context =>
            {
                if (context.HttpContext.Response.StatusCode == StatusCodes.Status400BadRequest)
                {
                    TBBProjectContext.Current.Resolve<ILogger<ErrorMarkerClazz>>().LogError("Error 400 bad request");
                }

                return Task.CompletedTask;
            });
        }
    }
}
