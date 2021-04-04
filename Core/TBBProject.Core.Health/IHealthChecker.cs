using Microsoft.Extensions.DependencyInjection;

namespace TBBProject.Core.Health
{
    public interface IHealthChecker
    {
        IHealthChecker AddRedisHealtCheck(string redisConnectionString);
        IHealthChecker AddSqlServerHealthCheck(string sqlServerConnectionString);
        IHealthChecker AddRabbitMqHealthCheck(string rabbitMqConnectionString);
        IHealthChecker AddUri(string uri, string displayname);

        IServiceCollection Build();
    }
}
