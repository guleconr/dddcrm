using App.Metrics;
using App.Metrics.Meter;

namespace TBBProject.Core.Metrics
{
    public static class CacheMetricsRegistry
    {
        public static MeterOptions CacheHit => new MeterOptions
        {
            Name = "cache_hit",
            MeasurementUnit = Unit.Calls
        };

        public static MeterOptions CacheMiss => new MeterOptions
        {
            Name = "cache_miss",
            MeasurementUnit = Unit.Calls
        };

        public static MeterOptions CacheSpace => new MeterOptions
        {
            Name = "cache_space",
            MeasurementUnit = Unit.Calls
        };
    }
}
