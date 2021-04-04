namespace TBBProject.Core.Web
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using TBBProject.Core.Logging;

    public static class TBBProjectWebHostBuilderExtensions
    {
        public static IWebHostBuilder AddTBBProjectMvc(this IWebHostBuilder builder)
        {
            return builder
                .AddTBBProjectLogging(options =>
                {
                    options.FileNameTemplate = "TBBProject-{Date}.log";
                    options.RestrictedProperties = new List<string>() {};
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .Build();
                })
                ;
        }
    }
}
