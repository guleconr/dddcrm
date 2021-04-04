using System;

namespace TBBProject.Core.Common
{
    public class GuidGenerator : IGuidGenerator
    {
        // For direct access with out DI.
        public static GuidGenerator Instance { get; } = new GuidGenerator();

        public Guid Generate()
        {
            return Guid.NewGuid();
        }
    }
}
