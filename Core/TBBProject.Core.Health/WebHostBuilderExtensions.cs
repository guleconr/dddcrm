using Microsoft.AspNetCore.Hosting;

namespace TBBProject.Core.Health
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder TBBProjectHealth(this IWebHostBuilder webhost)
        {
            webhost.UseBeatPulse(options =>
            {
                options.ConfigurePath(path: "health")
                     .ConfigureTimeout(milliseconds: 2000)
                     .ConfigureDetailedOutput(detailedOutput: true, includeExceptionMessages: true);
            });

            return webhost;
        }
    }
}
