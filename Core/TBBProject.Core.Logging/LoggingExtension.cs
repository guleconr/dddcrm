using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TBBProject.Core.Logging
{
    public static class LoggingExtension
    {
        public static IWebHostBuilder AddTBBProjectLogging(this IWebHostBuilder builder, Action<LoggerOptions> setup)
        {
            var options = new LoggerOptions();
            setup(options);

            return builder.ConfigureLogging((hostingContext, loggingBuilder) =>
                {
                    loggingBuilder.ClearProviders();
                    hostingContext.Configuration.GetSection("LoggerOptions").Bind(options);
                    SerilogConfigurator.ConfigureLogger(hostingContext.Configuration, loggingBuilder, options);
                });
        }
    }
}
