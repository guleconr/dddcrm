using TBBProject.Core.Common;
using Microsoft.Extensions.DependencyInjection;
using BeatPulse;
using System;
using BeatPulse.Core;
using System.Collections.Generic;

namespace TBBProject.Core.Health
{
    public class TBBProjectHealthChecker : IHealthChecker, IHealthCheckBuilder
    {
        private IServiceCollection _services;
         
        private IList<Action<BeatPulseContext>> _setups;

        internal TBBProjectHealthChecker()
        {
        }

        /// <summary>
        /// Initializes healthcheck services.
        /// Needs to be called first.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IHealthChecker Begin(IServiceCollection services)
        {
            Ensure.That("services", services).Is.NotNull();

            this._services = services;
            this._setups = new List<Action<BeatPulseContext>>();

            return this;
        }

        /// <summary>
        /// Registers rabbit mq with connection string
        /// </summary>
        /// <param name="rabbitMqConnectionString"></param>
        /// <returns></returns>
        public IHealthChecker AddRabbitMqHealthCheck(string rabbitMqConnectionString)
        {
            Ensure.That("rabbit mq connection string", rabbitMqConnectionString).Is.NotNullOrEmpty();

            void setup(BeatPulseContext options) => options.AddRabbitMQ(rabbitMqConnectionString);

            this._setups.Add(setup);

            return this;
        }

        /// <summary>
        /// registers redis health check via connections tring
        /// </summary>
        /// <param name="redisConnectionString"></param>
        /// <returns></returns>
        public IHealthChecker AddRedisHealtCheck(string redisConnectionString)
        {
            Ensure.That("redis connection string", redisConnectionString).Is.NotNullOrEmpty();

            void setup(BeatPulseContext options) => options.AddRedis(redisConnectionString);

            this._setups.Add(setup);

            return this;
        }

        /// <summary>
        /// Registers sqlserver health check via connections string
        /// </summary>
        /// <param name="sqlServerConnectionString"></param>
        /// <returns></returns>
        public IHealthChecker AddSqlServerHealthCheck(string sqlServerConnectionString)
        {
            Ensure.That("sql server connection string", sqlServerConnectionString).Is.NotNullOrEmpty();

            void setup(BeatPulseContext options) => options.AddSqlServer(sqlServerConnectionString);

            this._setups.Add(setup);

            return this;
        }

        /// <summary>
        /// Register a URI for health checking
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="displayname"></param>
        /// <returns></returns>
        public IHealthChecker AddUri(string uri, string displayname)
        {
            Ensure.That("uri", uri).Is.NotNullOrEmpty();
            Ensure.That("display name", displayname).Is.NotNullOrEmpty();

            void setup(BeatPulseContext options) => options.AddUrlGroup(new Uri(uri), name: displayname);

            this._setups.Add(setup);

            return this;
        }

        public IServiceCollection Build()
        {
            Action<BeatPulseContext> combinedSetup = null;
            
            foreach(var setup in this._setups)
            {
                combinedSetup += setup;
            }

            this._services.AddBeatPulse(combinedSetup);
            return this._services;
        }

    }
}
