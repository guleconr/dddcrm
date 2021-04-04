using System.Collections.Generic;
using Serilog.Core;
using Serilog.Events;

namespace TBBProject.Core.Logging
{
    public class LogEventFilter : ILogEventFilter
    {
        public LogEventFilter(List<string> restrictedProperties)
        {
            // Initial items
            this.RestrictedProperties.Add("Password");
            this.RestrictedProperties.Add("CreditCardNumber");

            // Additional items
            if (restrictedProperties != null)
            {
                this.RestrictedProperties.AddRange(restrictedProperties);
            }
        }

        public List<string> RestrictedProperties { get; } = new List<string>();

        public bool IsEnabled(LogEvent logEvent)
        {
            foreach (var prop in this.RestrictedProperties)
            {
                logEvent.RemovePropertyIfPresent(prop);
            }

            return true;
        }
    }
}
