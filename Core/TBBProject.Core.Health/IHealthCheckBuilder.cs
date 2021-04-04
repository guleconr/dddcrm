using Microsoft.Extensions.DependencyInjection;

namespace TBBProject.Core.Health
{
    public interface IHealthCheckBuilder
    {
        IHealthChecker Begin(IServiceCollection services);
    }
}
