using Microsoft.Extensions.DependencyInjection;

namespace TBBProject.Core.Health
{
    public static class HealthCheckBuilder
    {
        public static IHealthChecker Create(IServiceCollection services) => new TBBProjectHealthChecker().Begin(services);
    }
}
