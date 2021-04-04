using TBBProject.Core.Common;
using App.Metrics;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;

namespace TBBProject.Core.Metrics
{
    public static class MetricsServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureMvcMetrics(this IServiceCollection services, IConfiguration configuration)
        {
            Configure(services, configuration);
            services.AddMvc(options => options.Filters.Add(new MetricsResourceFilter(new MvcRouteTemplateResolver())));

            return services;
        }

        internal static IServiceCollection Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<JsonSerializerSettings, CompactSerializerSettings>();
            services.TryAddSingleton(typeof(IJsonSerializer), typeof(TBBProject.Core.Common.JsonSerializer));
            var serviceProvider = services.BuildServiceProvider();
            var jsonSerializer = serviceProvider.GetService<IJsonSerializer>();
            var serializerSettings = serviceProvider.GetService<JsonSerializerSettings>();

            var metricsOptions = new TBBProjectMetricsOptions();
            configuration.Bind("TBBProjectMetricsOptions", metricsOptions);
            var path = Path.Combine(metricsOptions.Folder, metricsOptions.File);

            var metrics = new MetricsBuilder().Configuration
                .Configure(new MetricsOptions { Enabled = metricsOptions.Enabled, ReportingEnabled = metricsOptions.ReportingEnabled })
                .Build();

            services.AddSingleton(metrics);
            services.AddMetrics(metrics);

            var clrMetrics = new ClrMetrics(metrics);
            clrMetrics.Register();

            services.AddMetricsEndpoints(options =>
            {
                options.MetricsEndpointOutputFormatter = new CompactJsonOutputFormatter(jsonSerializer, serializerSettings);
            });

            services.AddMetricsTrackingMiddleware(options =>
            {
                options.ApdexTrackingEnabled = true;
                options.ApdexTSeconds = 1;
            });

            //services.AddMetricsReportScheduler();

            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();

            services.AddCors(options =>
            {
                options.AddPolicy("MetricsPolicy", corsBuilder.Build());
            });

            services.AddMvcCore()
                .AddJsonOptions(options =>
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver() { NamingStrategy = new CamelCaseNamingStrategy() })
                .AddControllersAsServices();

            return services;
        }
    }
}
