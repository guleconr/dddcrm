using System.Collections.Generic;

namespace TBBProject.Core.Logging
{
    public class LoggerOptions
    {
        public string FileNameTemplate { get; set; } = "TBBProject-{Date}.log";

        public string LogTemplate { get; set; } = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] [{SourceContext}] ({AssemblyName}) | ({MachineName}) | ({ProcessId}) | ({ThreadId}) | {Message:lj} | {Exception} | {Properties:j} {NewLine}";

        public string AuditFileNameTemplate { get; set; } = "Audit-{HalfHour}.log";

        public string AuditLogTemplate { get; set; } = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] [{SourceContext}] ({MachineName}) | ({ProcessId}) | ({ThreadId}) | {Message:lj} | {Exception} | {Properties:j} {NewLine}";

        public bool Shared { get; set; } = true;

        public List<string> RestrictedProperties { get; set; } = new List<string>();

        public string LoggingPath { get; set; } = ".\\TBBProject_logs\\";
    }
}
