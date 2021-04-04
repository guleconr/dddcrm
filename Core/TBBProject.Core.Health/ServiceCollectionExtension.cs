using Microsoft.Extensions.DependencyInjection;
using System;

namespace TBBProject.Core.Health
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureHealth(this IServiceCollection services, 
            string sqlServerConnectionString = "",
            string redisConnectionString ="",
            string rabbitmqConnectionString ="",
            params Tuple<string,string> [] uris)
        {
            var healthChecker = HealthCheckBuilder.Create(services);

            if(!string.IsNullOrEmpty(sqlServerConnectionString))
            {
                healthChecker.AddSqlServerHealthCheck(sqlServerConnectionString);
            }

            if(!string.IsNullOrEmpty(redisConnectionString))
            {
                healthChecker.AddRedisHealtCheck(redisConnectionString);
            }

            if(!string.IsNullOrEmpty(rabbitmqConnectionString))
            {
                healthChecker.AddRabbitMqHealthCheck(rabbitmqConnectionString);
            }

            foreach(var uri in uris)
            {
                healthChecker.AddUri(uri.Item1, uri.Item2);
            }

            healthChecker.Build();

            return services;
        }
    }
}
