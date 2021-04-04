using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Destructurama;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Filters;
using Serilog.Formatting.Compact;

namespace TBBProject.Core.Logging
{
    [ExcludeFromCodeCoverage]
    public static class SerilogConfigurator
    {
        private static string CurrentEnv { get; } = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        public static IConfigurationBuilder Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{CurrentEnv}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        public static void ConfigureLogger(IConfiguration configuration, ILoggingBuilder builder, LoggerOptions options)
        {
            var isAuditStore = Matching.FromSource("TBBProject.Core.Audit.AuditStore");

            Console.WriteLine("options.LoggingPath:" + options.LoggingPath);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration ?? Configuration.Build())
                .MinimumLevel.Error()
                .Filter.With(new LogEventFilter(options.RestrictedProperties))
                .Enrich.With(new CorrelationIdEnricher())
                .Enrich.With(new UserIdEnricher())
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()
                .Enrich.WithMachineName()
                .Enrich.WithAssemblyName()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .Destructure.UsingAttributes()
                .Destructure.JsonNetTypes()
                .WriteTo.Logger(loggerContext => loggerContext.Filter
                    .ByIncludingOnly(le => !isAuditStore.Invoke(le) && CurrentEnv == "Local").WriteTo
                    .RollingFile(pathFormat: options.LoggingPath + options.FileNameTemplate, shared: options.Shared,
                        outputTemplate: options.LogTemplate))
                .WriteTo.Logger(loggerContext => loggerContext.Filter
                    .ByIncludingOnly(le => !isAuditStore.Invoke(le) && CurrentEnv != "Local").WriteTo
                    .RollingFile(pathFormat: options.LoggingPath + options.FileNameTemplate, shared: options.Shared,
                        formatter: new CompactJsonFormatter()))
                .WriteTo.Logger(loggerContext => loggerContext.Filter.ByIncludingOnly(isAuditStore)
                    .WriteTo.RollingFile(
                        pathFormat: options.LoggingPath + options.AuditFileNameTemplate,
                        outputTemplate: options.AuditLogTemplate, shared: options.Shared))
                // .WriteTo.Logger(loggerContext => loggerContext.Filter.ByIncludingOnly(e => e.Properties.ContainsKey("EventId") && e.Properties["EventId"].ToString().Contains(LogType.AuditLog.Name))
                //    .WriteTo.RollingFile(pathFormat: "Audit-{Date}.log", outputTemplate: options.LogTemplate, shared: true)
                // )
                .CreateLogger();

            builder.AddSerilog(dispose: true);
        }
    }
}
