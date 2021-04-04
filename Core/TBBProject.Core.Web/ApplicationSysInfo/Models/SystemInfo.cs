using System;
using System.Collections.Generic;

namespace TBBProject.Core.Web
{
    public sealed class SystemInfo
    {
        public string HostingEnvironment { get; set; }
        public string Culture { get; set; }
        public string UiCulture { get; set; }
        public string AspNetInfo { get; set; }

        public string IsFullTrust { get; set; }

        public string OperatingSystem { get; set; }

        public DateTime ServerLocalTime { get; set; }

        public string ServerTimeZone { get; set; }

        public DateTime UtcTime { get; set; }

        public DateTime CurrentUserTime { get; set; }

        public string HttpHost { get; set; }

        public IList<HeaderModel> Headers { get; set; } = new List<HeaderModel>();

        public IList<LoadedAssembly> LoadedAssemblies { get; set; } = new List<LoadedAssembly>();
    }
}
