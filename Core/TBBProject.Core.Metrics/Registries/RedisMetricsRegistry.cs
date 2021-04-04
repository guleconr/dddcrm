using App.Metrics;
using App.Metrics.Meter;

namespace TBBProject.Core.Metrics
{
    public static class RedisMetricsRegistry
    {

        public static MeterOptions CacheHit => new MeterOptions
        {
            Name = "Redis_Cache_Hit",
            MeasurementUnit = Unit.Calls
        };

        public static MeterOptions CacheMiss => new MeterOptions
        {
            Name = "Redis_Cache_Miss",
            MeasurementUnit = Unit.Calls
        };

        public static MeterOptions CacheSpace => new MeterOptions
        {
            Name = "Redis_Cache_Space",
            MeasurementUnit = Unit.Calls
        };
    }
}
