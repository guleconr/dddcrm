namespace TBBProject.Core.Metrics
{
    using App.Metrics;
    using App.Metrics.Gauge;
    public static class ClrMetricsRegistry
    {
        public static readonly GaugeOptions PagedMemory64;

        public static readonly GaugeOptions NonpagedSystemMemorySize64;

        public static readonly GaugeOptions PagedSystemMemorySize64;

        public static readonly GaugeOptions PeakPagedMemorySize64;

        public static readonly GaugeOptions PeakVirtualMemorySize64;

        public static readonly GaugeOptions PeakWorkingSet64;

        public static readonly GaugeOptions PrivateMemorySize64;

        public static readonly GaugeOptions VirtualMemorySize64;

        public static readonly GaugeOptions WorkingSet64;

        public static readonly GaugeOptions PrivilegedProcessorTime;

        public static readonly GaugeOptions UserProcessorTime;

        public static readonly GaugeOptions TotalProcessorTime;

        public static readonly GaugeOptions ThreadCount;

        public static readonly GaugeOptions Generation0GarbageCollectionCount;
        public static readonly GaugeOptions Generation1GarbageCollectionCount;
        public static readonly GaugeOptions Generation2GarbageCollectionCount;

        static ClrMetricsRegistry()
        {
            PagedMemory64 = new GaugeOptions
            {
                Name = "paged_memory_64",
                MeasurementUnit = Unit.MegaBytes
            };

            NonpagedSystemMemorySize64 = new GaugeOptions
            {
                Name = "non_paged_system_memory_size_64",
                MeasurementUnit = Unit.MegaBytes
            };

            PagedSystemMemorySize64 = new GaugeOptions
            {
                Name = "paged_system_memory_size_64",
                MeasurementUnit = Unit.MegaBytes
            };

            PeakPagedMemorySize64 = new GaugeOptions
            {
                Name = "peak_system_memory_size_64",
                MeasurementUnit = Unit.MegaBytes
            };

            PeakVirtualMemorySize64 = new GaugeOptions
            {
                Name = "peak_virtual_memory_size_64",
                MeasurementUnit = Unit.GigaBytes
            };

            PeakWorkingSet64 = new GaugeOptions
            {
                Name = "peak_working_set_64",
                MeasurementUnit = Unit.MegaBytes
            };

            PrivateMemorySize64 = new GaugeOptions
            {
                Name = "private_memory_size_64",
                MeasurementUnit = Unit.MegaBytes
            };

            VirtualMemorySize64 = new GaugeOptions
            {
                Name = "virtual_memory_size_64",
                MeasurementUnit = Unit.GigaBytes
            };

            WorkingSet64 = new GaugeOptions
            {
                Name = "working_set_64",
                MeasurementUnit = Unit.MegaBytes
            };

            PrivilegedProcessorTime = new GaugeOptions
            {
                Name = "priviliged_processor_time",
                MeasurementUnit = Unit.Custom("Seconds")
            };

            UserProcessorTime = new GaugeOptions
            {
                Name = "user_processor_time",
                MeasurementUnit = Unit.Custom("Seconds")
            };

            TotalProcessorTime = new GaugeOptions
            {
                Name = "total_processor_time",
                MeasurementUnit = Unit.Custom("Seconds")
            };

            ThreadCount = new GaugeOptions
            {
                Name = "thread_count",
                MeasurementUnit = Unit.Custom("Count")
            };

            Generation0GarbageCollectionCount = new GaugeOptions
            {
                Name = "generation_zero_garbage_collection_count",
                MeasurementUnit = Unit.Custom("Count")
            };

            Generation1GarbageCollectionCount = new GaugeOptions
            {
                Name = "generation_one_garbage_collection_count",
                MeasurementUnit = Unit.Custom("Count")
            };

            Generation2GarbageCollectionCount = new GaugeOptions
            {
                Name = "generation_two_garbage_collection_count",
                MeasurementUnit = Unit.Custom("Count")
            };

        }
    }
}
