namespace TBBProject.Core.Metrics
{
    using App.Metrics;
    using App.Metrics.Gauge;

    public class ClrMetrics
    {
        private readonly IMetrics _metrics;

        public ClrMetrics(IMetrics metrics) => this._metrics = metrics;

        public void Register()
        {
            this._metrics.Provider.Gauge.Instance(
                ClrMetricsRegistry.PagedMemory64,
                () => new FunctionGauge(() => ClrMetricsValueProvider.GetPagedMemory64()));

            this._metrics.Provider.Gauge.Instance(
                ClrMetricsRegistry.NonpagedSystemMemorySize64,
                () => new FunctionGauge(() => ClrMetricsValueProvider.GetNonPagedSystemMemorySize64()));

            this._metrics.Provider.Gauge.Instance(
                ClrMetricsRegistry.PagedSystemMemorySize64,
                () => new FunctionGauge(() => ClrMetricsValueProvider.GetPagedSystemMemorySize64()));

            this._metrics.Provider.Gauge.Instance(
                ClrMetricsRegistry.PeakPagedMemorySize64,
                () => new FunctionGauge(() => ClrMetricsValueProvider.GetPeakPagedMemorySize64()));

            this._metrics.Provider.Gauge.Instance(
                ClrMetricsRegistry.PeakVirtualMemorySize64,
                () => new FunctionGauge(() => ClrMetricsValueProvider.GetPeakVirtualMemorySize64()));

            this._metrics.Provider.Gauge.Instance(
                ClrMetricsRegistry.PeakWorkingSet64,
                () => new FunctionGauge(() => ClrMetricsValueProvider.GetPeakWorkingSet64()));

            this._metrics.Provider.Gauge.Instance(
                ClrMetricsRegistry.PrivateMemorySize64,
                () => new FunctionGauge(() => ClrMetricsValueProvider.GetPrivateMemorySize64()));

            this._metrics.Provider.Gauge.Instance(
                ClrMetricsRegistry.VirtualMemorySize64,
                () => new FunctionGauge(() => ClrMetricsValueProvider.GetVirtualMemorySize64()));

            this._metrics.Provider.Gauge.Instance(
                ClrMetricsRegistry.WorkingSet64,
                () => new FunctionGauge(() => ClrMetricsValueProvider.GetWorkingSet64()));

            this._metrics.Provider.Gauge.Instance(
                ClrMetricsRegistry.PrivilegedProcessorTime,
                () => new FunctionGauge(() => ClrMetricsValueProvider.GetPrivilegedProcessorTime()));

            this._metrics.Provider.Gauge.Instance(
                ClrMetricsRegistry.UserProcessorTime,
                () => new FunctionGauge(() => ClrMetricsValueProvider.GetUserProcessorTime()));

            this._metrics.Provider.Gauge.Instance(
                ClrMetricsRegistry.TotalProcessorTime,
                () => new FunctionGauge(() => ClrMetricsValueProvider.GetTotalProcessorTime()));

            this._metrics.Provider.Gauge.Instance(
                ClrMetricsRegistry.ThreadCount,
                () => new FunctionGauge(() => ClrMetricsValueProvider.GetThreadCount()));

            this._metrics.Provider.Gauge.Instance(
               ClrMetricsRegistry.Generation0GarbageCollectionCount,
               () => new FunctionGauge(() => ClrMetricsValueProvider.GetGenerationZeroGarbageCollectionCount()));

            this._metrics.Provider.Gauge.Instance(
               ClrMetricsRegistry.Generation1GarbageCollectionCount,
               () => new FunctionGauge(() => ClrMetricsValueProvider.GetGenerationOneGarbageCollectionCount()));

            this._metrics.Provider.Gauge.Instance(
               ClrMetricsRegistry.Generation2GarbageCollectionCount,
               () => new FunctionGauge(() => ClrMetricsValueProvider.GetGenerationTwoGarbageCollectionCount()));
        }
    }
}
