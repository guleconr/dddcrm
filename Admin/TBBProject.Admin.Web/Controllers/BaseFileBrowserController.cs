using System;
using System.IO;
using System.Linq;
using Kendo.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Http;
using Kendo.Mvc.UI;
using System.Net.Http.Headers;

namespace TBBProject.Admin.Web.Controllers
{
    public abstract class BaseFileBrowserController : Controller, IFileBrowserController
    {
        private readonly IDirectoryBrowser directoryBrowser;
        private readonly IDirectoryPermission permission;

        protected readonly IHostingEnvironment HostingEnvironment;

        protected BaseFileBrowserController(IHostingEnvironment hostingEnvironment)
            : this(DI.Current.Resolve<IDirectoryBrowser>(),
                  DI.Current.Resolve<IDirectoryPermission>(),
                  hostingEnvironment)
        {
        }

        protected BaseFileBrowserController(
            IDirectoryBrowser directoryBrowser,
            IDirectoryPermission permission,
            IHostingEnvironment hostingEnvironment)
        {
            this.directoryBrowser = directoryBrowser;
            this.directoryBrowser.HostingEnvironment = hostingEnvironment;
            this.permission = permission;
            this.HostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Gets the base path from which content will be served.
        /// </summary>
        public abstract string ContentPath
        {
            get;
        }

        /// <summary>
        /// Gets the valid file extensions by which served files will be filtered.
        /// </summary>
        public virtual string Filter
        {
            get
            {
                return "*.*";
            }
        }

        /// <summary>
        /// Creates a folder with a given entry.
        /// </summary>
        /// <param name="path">The path to the parent folder in which the folder should be created.</param>
        /// <param name="entry">The entry.</param>
        /// <returns>An empty <see cref="ContentResult"/>.</returns>
        /// <exception cref="Exception">Forbidden</exception>
        [AcceptVerbs("POST")]
        public virtual ActionResult Create(string path, FileBrowserEntry entry)
        {
            var fullPath = NormalizePath(path);
            var name = entry.Name;

            if (name.HasValue() && AuthorizeCreateDirectory(fullPath, name))
            {
                var physicalPath = Path.Combine(fullPath, name);

                if (!Directory.Exists(physicalPath))
                {
                    Directory.CreateDirectory(physicalPath);
                }

                return Json(entry);
            }

            throw new Exception("Forbidden");
        }

        /// <summary>
        /// Determines if a folder can be created.
        /// </summary>
        /// <param name="path">The path to the parent folder in which the folder should be created.</param>
        /// <param name="name">Name of the folder.</param>
        /// <returns>true if folder can be created, otherwise false.</returns>
        public virtual bool AuthorizeCreateDirectory(string path, string name)
        {
            return CanAccess(path);
        }

        public virtual JsonResult Read(string path)
        {
            var fullPath = NormalizePath(path);

            if (AuthorizeRead(fullPath))
            {
                try
                {
                    var files = directoryBrowser.GetFiles(fullPath, Filter);
                    var directories = directoryBrowser.GetDirectories(fullPath);
                    var result = files.Concat(directories);

                    return Json(result.ToArray());
                }
                catch (DirectoryNotFoundException)
                {
                    throw new Exception("File Not Found");
                }
            }

            throw new Exception("Forbidden");
        }

        /// <summary>
        /// Determines if content of a given path can be browsed.
        /// </summary>
        /// <param name="path">The path which will be browsed.</param>
        /// <returns>true if browsing is allowed, otherwise false.</returns>
        public virtual bool AuthorizeRead(string path)
        {
            return CanAccess(path);
        }

        protected virtual bool CanAccess(string path)
        {
            var rootPath = Path.GetFullPath(Path.Combine(this.HostingEnvironment.WebRootPath, ContentPath));

            return permission.CanAccess(rootPath, path);
        }

        protected string NormalizePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return Path.GetFullPath(Path.Combine(this.HostingEnvironment.WebRootPath, ContentPath));
            }
            else
            {
                return Path.GetFullPath(Path.Combine(this.HostingEnvironment.WebRootPath, ContentPath, path));
            }
        }

        /// <summary>
        /// Deletes a entry.
        /// </summary>
        /// <param name="path">The path to the entry.</param>
        /// <param name="entry">The entry.</param>
        /// <returns>An empty <see cref="ContentResult"/>.</returns>
        /// <exception cref="Exception">File Not Found</exception>
        [AcceptVerbs("POST")]
        public virtual ActionResult Destroy(string path, FileBrowserEntry entry)
        {
            var fullPath = NormalizePath(path);

            if (entry != null)
            {
                fullPath = Path.Combine(fullPath, entry.Name);

                if (entry.EntryType == FileBrowserEntryType.File)
                {
                    DeleteFile(fullPath);
                }
                else
                {
                    DeleteDirectory(fullPath);
                }

                return Json(new object[0]);
            }

            throw new Exception("File Not Found");
        }

        /// <summary>
        /// Determines if a file can be deleted.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>true if file can be deleted, otherwise false.</returns>
        public virtual bool AuthorizeDeleteFile(string path)
        {
            return CanAccess(path);
        }

        /// <summary>
        /// Determines if a folder can be deleted.
        /// </summary>
        /// <param name="path">The path to the folder.</param>
        /// <returns>true if folder can be deleted, otherwise false.</returns>
        public virtual bool AuthorizeDeleteDirectory(string path)
        {
            return CanAccess(path);
        }

        protected virtual void DeleteFile(string path)
        {
            if (!AuthorizeDeleteFile(path))
            {
                throw new Exception("Forbidden");
            }

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }

        protected virtual void DeleteDirectory(string path)
        {
            if (!AuthorizeDeleteDirectory(path))
            {
                throw new Exception("Forbidden");
            }

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }


        /// <summary>
        /// Uploads a file to a given path.
        /// </summary>
        /// <param name="path">The path to which the file should be uploaded.</param>
        /// <param name="file">The file which should be uploaded.</param>
        /// <returns>A <see cref="JsonResult"/> containing the uploaded file's size and name.</returns>
        /// <exception cref="Exception">Forbidden</exception>
        [AcceptVerbs("POST")]
        public virtual ActionResult Upload(string path, IFormFile file)
        {
            var fullPath = NormalizePath(path);

            if (AuthorizeUpload(fullPath, file))
            {
                SaveFile(file, fullPath);

                var result = new FileBrowserEntry
                {
                    Size = file.Length,
                    Name = GetFileName(file)
                };

                return Json(result);
            }

            throw new Exception("Forbidden");
        }

        protected virtual void SaveFile(IFormFile file, string pathToSave)
        {
            try
            {
                var path = Path.Combine(pathToSave, GetFileName(file));
                using (var stream = System.IO.File.Create(path))
                {
                    file.CopyTo(stream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Determines if a file can be uploaded to a given path.
        /// </summary>
        /// <param name="path">The path to which the file should be uploaded.</param>
        /// <param name="file">The file which should be uploaded.</param>
        /// <returns>true if the upload is allowed, otherwise false.</returns>
        public virtual bool AuthorizeUpload(string path, IFormFile file)
        {
            return CanAccess(path) && IsValidFile(GetFileName(file));
        }

        private bool IsValidFile(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            var allowedExtensions = Filter.Split(',');

            return allowedExtensions.Any(e => e.Equals("*.*") || e.EndsWith(extension, StringComparison.OrdinalIgnoreCase));
        }

        public virtual string GetFileName(IFormFile file)
        {
            var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            return Path.GetFileName(fileContent.FileName.ToString().Trim('"'));
        }
    }
}