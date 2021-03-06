using System;
using Microsoft.Extensions.Options;

namespace TBBProject.Core.Common
{
    public class Clock : IClock
    {
        protected readonly ClockOptions Options;
        public Clock(IOptions<ClockOptions> options)
        {
            Options = options.Value;
        }
        public virtual DateTime Now => Options.Kind == DateTimeKind.Utc ? DateTime.UtcNow : DateTime.Now;

        public virtual DateTimeKind Kind => Options.Kind;

        public DateTime Normalize(DateTime dateTime)
        {
            if (Kind == DateTimeKind.Unspecified || Kind == dateTime.Kind)
            {
                return dateTime;
            }

            if (Kind == DateTimeKind.Local && dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime.ToLocalTime();
            }

            if (Kind == DateTimeKind.Utc && dateTime.Kind == DateTimeKind.Local)
            {
                return dateTime.ToUniversalTime();
            }

            return DateTime.SpecifyKind(dateTime, Kind);
        }
   
    }
    
}