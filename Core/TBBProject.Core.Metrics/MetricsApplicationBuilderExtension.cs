namespace TBBProject.Core.Metrics
{
    using Microsoft.AspNetCore.Builder;
    public static class MetricsApplicationBuilderExtension
    {
        public static IApplicationBuilder UseTBBProjectMetrics(this IApplicationBuilder app)
        {
            // Configures : /env
            // Displays environment information
            app.UseEnvInfoEndpoint();

            // Configures: /metrics and /metrics-text
            // Displays metrics end points
            app.UseMetricsAllEndpoints(); 

            app.UseMetricsAllMiddleware();
            app.UseCors("MetricsPolicy");

            return app;
        }
    }
}
