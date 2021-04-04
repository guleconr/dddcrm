using System.Threading;
using Serilog.Core;
using Serilog.Events;

namespace TBBProject.Core.Logging
{
    public class CorrelationIdEnricher : ILogEventEnricher
    {
        private const string CorrelationIdPropertyName = "CorrelationId";
        private static readonly AsyncLocal<string> CorrelationIdContext = new AsyncLocal<string>();

        /// <summary>Update the Correlation Id associated with the current thread context.</summary>
        /// <param name="correlationId">The correlation id.</param>
        public static void UpdateCorrelationId(string correlationId) => CorrelationIdContext.Value = correlationId;

        /// <summary>Clear the Correlation Id associated with the current thread context.</summary>
        public static void ClearCorrelationId() => CorrelationIdContext.Value = string.Empty;

        public static string GetCorrelationId() => CorrelationIdContext.Value;

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var correlationId = CorrelationIdContext.Value;
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(CorrelationIdPropertyName, new ScalarValue(correlationId)));
        }
    }
}
