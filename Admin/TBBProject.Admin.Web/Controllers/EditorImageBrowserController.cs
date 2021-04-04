using System;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TBBProject.Admin.Web.Controllers;

namespace TBBProject.Admin.Web.Controllers
{
    public abstract class EditorImageBrowserController : BaseFileBrowserController, IImageBrowserController
    {
        protected EditorImageBrowserController(IHostingEnvironment hostingEnvironment)
            : this(DI.Current.Resolve<IDirectoryBrowser>(),
                  DI.Current.Resolve<IDirectoryPermission>(),
                  hostingEnvironment)
        {
        }

        protected EditorImageBrowserController(
            IDirectoryBrowser directoryBrowser,
            IDirectoryPermission permission,
            IHostingEnvironment hostingEnvironment)
             : base(directoryBrowser, permission, hostingEnvironment)
        {
        }

        /// <summary>
        /// Gets the valid file extensions by which served files will be filtered.
        /// </summary>
        public override string Filter
        {
            get
            {
                return EditorImageBrowserSettings.DefaultFileTypes;
            }
        }

        public virtual bool AuthorizeThumbnail(string path)
        {
            return CanAccess(path);
        }

        /// <summary>
        /// You can use a third-party library to create thumbnails as System.Drawing is not curretnly part of ASP.NET Core https://blogs.msdn.microsoft.com/dotnet/2016/02/10/porting-to-net-core/
        /// </summary>
        public virtual IActionResult Thumbnail(string path)
        {
            return null;
        }
    }
}