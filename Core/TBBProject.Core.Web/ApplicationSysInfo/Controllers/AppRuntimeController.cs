using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace TBBProject.Core.Web
{
    public class AppRuntimeController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AppRuntimeController(IHttpContextAccessor httpContext, IHostingEnvironment hostingEnvironment)
        {
            _httpContext = httpContext;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet("info")]
        public IActionResult Index()
        {
            var systemInfo = new SystemInfo
            {
                Culture = Thread.CurrentThread.CurrentCulture.DisplayName,
                UiCulture = Thread.CurrentThread.CurrentUICulture.DisplayName,
                ServerTimeZone = TimeZoneInfo.Local.StandardName,
                ServerLocalTime = DateTime.Now,
                UtcTime = DateTime.UtcNow,
                CurrentUserTime = DateTime.Now,
                HttpHost = _httpContext.HttpContext.Request.Headers[HeaderNames.Host],
                HostingEnvironment = _hostingEnvironment.EnvironmentName
            };


            try
            {
                systemInfo.OperatingSystem = Environment.OSVersion.VersionString;
                systemInfo.AspNetInfo = RuntimeEnvironment.GetSystemVersion();
                systemInfo.IsFullTrust = AppDomain.CurrentDomain.IsFullyTrusted.ToString();
            }
            catch { }

            foreach (var header in _httpContext.HttpContext.Request.Headers)
            {
                systemInfo.Headers.Add(new HeaderModel
                {
                    Name = header.Key,
                    Value = header.Value
                });
            }

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var loadedAssemblyModel = new LoadedAssembly
                {
                    FullName = assembly.FullName,
                    Name = assembly.GetName().Name
                };

                try
                {
                    loadedAssemblyModel.Location = assembly.IsDynamic ? null : assembly.Location;
                    var dt = System.IO.File.GetLastWriteTimeUtc(assembly.Location);

                    FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                    string version = fvi.FileVersion;

                    loadedAssemblyModel.Version = version;

                    loadedAssemblyModel.IsDebug = assembly.GetCustomAttributes(typeof(DebuggableAttribute), false)
                        .FirstOrDefault() is DebuggableAttribute attribute && attribute.IsJITOptimizerDisabled;
                    loadedAssemblyModel.BuildDate = assembly.IsDynamic ? null : (DateTime?)TimeZoneInfo.ConvertTimeFromUtc(dt, TimeZoneInfo.Local);

                }
                catch { }
                systemInfo.LoadedAssemblies.Add(loadedAssemblyModel);
            }
            return Json(systemInfo);
        }
    }
}
