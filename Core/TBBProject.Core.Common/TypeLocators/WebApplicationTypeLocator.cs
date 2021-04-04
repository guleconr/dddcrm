namespace TBBProject.Core.Common
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class WebApplicationTypeLocator : AppDomainTypeLocator
    {

        private bool _binFolderAssembliesLoaded;

        public WebApplicationTypeLocator(IFileSystemFileProvider fileProvider = null)
            : base(fileProvider)
        {
        }

        public bool EnsureBinFolderAssembliesLoaded { get; set; } = true;

        public virtual string GetBinDirectory() => AppContext.BaseDirectory;

        public override IList<Assembly> GetAssemblies()
        {
            if (!EnsureBinFolderAssembliesLoaded || _binFolderAssembliesLoaded)
            {
                return base.GetAssemblies();
            }

            _binFolderAssembliesLoaded = true;
            var binPath = GetBinDirectory();
            LoadMatchingAssemblies(binPath);

            return base.GetAssemblies();
        }
    }
}
