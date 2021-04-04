using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TBBProject.Core.Common;

namespace TBBProject.Core.Web
{
    public static class PageNotFoundHandler
    {
        public static void UsePageNotFoundHandler(this IApplicationBuilder application)
        {
            application.UseStatusCodePages(async context =>
            {
                if (context.HttpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    IWebHelper webHelper = (IWebHelper)application.ApplicationServices.GetService(typeof(IWebHelper));

                    if (webHelper.IsStaticResource() == false)
                    {
                        var originalPath = context.HttpContext.Request.Path;
                        var originalQueryString = context.HttpContext.Request.QueryString;

                        context.HttpContext.Features.Set<IStatusCodeReExecuteFeature>(new StatusCodeReExecuteFeature
                        {
                            OriginalPathBase = context.HttpContext.Request.PathBase.Value,
                            OriginalPath = originalPath.Value,
                            OriginalQueryString = originalQueryString.HasValue ? originalQueryString.Value : string.Empty,
                        });

                        context.HttpContext.Request.Path = "/errors/page-not-found";
                        context.HttpContext.Request.QueryString = QueryString.Empty;

                        try
                        {
                            await context.Next(context.HttpContext);
                        }
                        finally
                        {
                            context.HttpContext.Request.QueryString = originalQueryString;
                            context.HttpContext.Request.Path = originalPath;
                            context.HttpContext.Features.Set<IStatusCodeReExecuteFeature>(null);

                            TBBProjectContext.Current.Resolve<ILogger<ErrorMarkerClazz>>().LogError($"Page not found on {originalPath}, {originalQueryString}");
                        }
                    }
                }
            });
        }
    }
}
