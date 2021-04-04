using Microsoft.Extensions.Logging;

namespace TBBProject.Core.Logging
{
    public class LogType
    {
        public static readonly EventId TechnicalLog = new EventId(1, "TechnicalLog");

        public static readonly EventId AuditLog = new EventId(2, "AuditLog");
    }
}
