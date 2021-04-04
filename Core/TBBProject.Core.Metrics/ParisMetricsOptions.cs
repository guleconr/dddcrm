namespace TBBProject.Core.Metrics
{
    public class TBBProjectMetricsOptions
    {
        public string Folder { get; set; } = "collect";

        public string File { get; set; } = "metrics.log";

        public bool Enabled { get; set; } = true;

        public bool ReportingEnabled { get; set; } = false;

        public int FlushIntervalSeconds { get; set; } = 120;
    }
}
