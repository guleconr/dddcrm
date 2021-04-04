using System;

namespace TBBProject.Core.Metrics
{
    public static class BytesTo
    {
        public static double Megabytes(this long bytes) => Math.Round(bytes / 1024f / 1024f, 2);

        public static double Gigabytes(this long bytes) => Math.Round(bytes / 1024f / 1024f / 1024f, 2);
    }
}
