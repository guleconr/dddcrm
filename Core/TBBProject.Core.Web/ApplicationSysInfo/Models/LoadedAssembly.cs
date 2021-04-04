using System;

namespace TBBProject.Core.Web
{
    public sealed class LoadedAssembly
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Location { get; set; }
        public bool IsDebug { get; set; }
        public DateTime? BuildDate { get; set; }
        public string Version { get; set; }
    }
}
