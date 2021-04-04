using System;

namespace TBBProject.Core.Common
{
    public class ClockOptions
    {
        public DateTimeKind Kind { get; set; }

        public ClockOptions()
        {
            Kind = DateTimeKind.Utc;
        }
    }
}