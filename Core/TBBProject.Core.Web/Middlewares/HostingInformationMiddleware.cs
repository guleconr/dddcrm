using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TBBProject.Core.Web
{
    [DebuggerStepThrough]
    public class HostingInformationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string EnvironmentNameHeader = "X-Environment-Name";
        private readonly string ServerNameHeader = "X-Server-Name";

        public HostingInformationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IHostingEnvironment hostingEnvironment)
        {
            if(!context.Response.Headers.Keys.Contains(EnvironmentNameHeader))
            {
                context.Response.Headers.Add(EnvironmentNameHeader, hostingEnvironment.EnvironmentName);
            }

            if(!context.Response.Headers.Keys.Contains(ServerNameHeader))
            {
                context.Response.Headers.Add(ServerNameHeader, Environment.MachineName);
            }
            
            await _next(context);
        }
    }
}
