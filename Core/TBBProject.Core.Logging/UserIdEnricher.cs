using System.Threading;
using Serilog.Core;
using Serilog.Events;

namespace TBBProject.Core.Logging
{
    public class UserIdEnricher : ILogEventEnricher
    {
        private const string UserIdPropertyName = "UserId";
        private static readonly AsyncLocal<string> UserIdContext = new AsyncLocal<string>();

        /// <summary>Update the User Id associated with the current thread context.</summary>
        /// <param name=userId">The user id.</param>
        public static void UpdateUserId(string userId) => UserIdContext.Value = userId;

        /// <summary>Clear the User Id associated with the current thread context.</summary>
        public static void ClearUserId() => UserIdContext.Value = string.Empty;

        public static string GetCorrelationId() => UserIdContext.Value;

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var userId = UserIdContext.Value;
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(UserIdPropertyName, new ScalarValue(userId)));
        }
    }
}
