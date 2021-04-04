using System;

namespace TBBProject.Core.Common
{
    public interface IClock
    {
        DateTime Now { get; }
        DateTimeKind Kind { get; }
        DateTime Normalize(DateTime dateTime);
   
    }
    
}