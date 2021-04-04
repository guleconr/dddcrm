namespace TBBProject.Core.Metrics
{
    using System;
    using System.Diagnostics;

    public static class ClrMetricsValueProvider
    {
        public static double GetPagedMemory64()
        {
            var process = Process.GetCurrentProcess();
            return process.PagedMemorySize64.Megabytes();
        }

        public static double GetNonPagedSystemMemorySize64()
        {
            var process = Process.GetCurrentProcess();
            return process.NonpagedSystemMemorySize64.Megabytes();
        }

        public static double GetPagedSystemMemorySize64()
        {
            var process = Process.GetCurrentProcess();
            return process.PagedSystemMemorySize64.Megabytes();
        }

        public static double GetPeakPagedMemorySize64()
        {
            var process = Process.GetCurrentProcess();
            return process.PeakPagedMemorySize64.Megabytes();
        }

        public static double GetPeakVirtualMemorySize64()
        {
            var process = Process.GetCurrentProcess();
            return process.PeakVirtualMemorySize64.Gigabytes();
        }

        public static double GetPeakWorkingSet64()
        {
            var process = Process.GetCurrentProcess();
            return process.PeakWorkingSet64.Megabytes();
        }

        public static double GetPrivateMemorySize64()
        {
            var process = Process.GetCurrentProcess();
            return process.PrivateMemorySize64.Megabytes();
        }

        public static double GetVirtualMemorySize64()
        {
            var process = Process.GetCurrentProcess();
            return process.VirtualMemorySize64.Gigabytes();
        }

        public static double GetWorkingSet64()
        {
            var process = Process.GetCurrentProcess();
            return process.WorkingSet64.Megabytes();
        }

        public static double GetPrivilegedProcessorTime()
        {
            var process = Process.GetCurrentProcess();
            return process.PrivilegedProcessorTime.TotalMilliseconds / 1000.0;
        }

        public static double GetUserProcessorTime()
        {
            var process = Process.GetCurrentProcess();
            return process.UserProcessorTime.TotalMilliseconds / 1000.0;
        }

        public static double GetTotalProcessorTime()
        {
            var process = Process.GetCurrentProcess();
            return process.TotalProcessorTime.TotalMilliseconds / 1000.0;
        }

        public static double GetThreadCount()
        {
            var process = Process.GetCurrentProcess();
            return process.Threads.Count;
        }

        public static double GetGenerationZeroGarbageCollectionCount() => GC.CollectionCount(0);

        public static double GetGenerationOneGarbageCollectionCount() => GC.CollectionCount(1);

        public static double GetGenerationTwoGarbageCollectionCount() => GC.CollectionCount(2);
    }
}
