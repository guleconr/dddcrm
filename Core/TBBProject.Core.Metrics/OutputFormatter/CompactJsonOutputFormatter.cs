using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.Formatters;
using TBBProject.Core.Common;
using Newtonsoft.Json;

namespace TBBProject.Core.Metrics
{
    public class CompactJsonOutputFormatter : IMetricsOutputFormatter
    {
        private readonly IJsonSerializer _serializer;
        private readonly JsonSerializerSettings _serializerSettings;

        public CompactJsonOutputFormatter(IJsonSerializer serializer, JsonSerializerSettings serializerSettings)
        {
            this._serializerSettings = serializerSettings;
            this._serializer = serializer;
        }

        public MetricsMediaTypeValue MediaType => new MetricsMediaTypeValue("application", "vnd.appmetrics.metrics", "v1", "json");

        //public MetricFields MetricFields { get; set; }

        public async Task WriteAsync(Stream output, MetricsDataValueSource metricsData, CancellationToken cancellationToken = default)
        {
            using (var streamWriter = new StreamWriter(output))
            {
                using (var textWriter = new JsonTextWriter(streamWriter))
                {
                    this._serializer.Serialize(textWriter, metricsData);
                    await streamWriter.WriteAsync(Environment.NewLine);
                }
            }
        }
    }
}
