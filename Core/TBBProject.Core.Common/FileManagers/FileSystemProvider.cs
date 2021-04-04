namespace TBBProject.Core.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.FileProviders;

    public class FileSystemProvider : PhysicalFileProvider, IFileSystemFileProvider
    {
        public FileSystemProvider(IHostingEnvironment hostingEnvironment)
            : base(File.Exists(hostingEnvironment.WebRootPath) ? Path.GetDirectoryName(hostingEnvironment.WebRootPath) : hostingEnvironment.WebRootPath)
        {
            var path = hostingEnvironment.ContentRootPath ?? string.Empty;
            if (File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
            }

            BaseDirectory = path;
        }

        private static void DeleteDirectoryRecursive(string path)
        {
            Directory.Delete(path, true);
            const int maxIterationToWait = 10;
            var curIteration = 0;

            while (Directory.Exists(path))
            {
                curIteration += 1;
                if (curIteration > maxIterationToWait)
                    return;
                Thread.Sleep(100);
            }
        }

        public virtual string Combine(params string[] paths) => Path.Combine(paths);

        public virtual void CreateDirectory(string path)
        {
            if (!DirectoryExists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public virtual void CreateFile(string path)
        {
            if (FileExists(path))
            {
                return;
            }

            using (File.Create(path))
            {
            }
        }

        public void DeleteDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(path);
            }

            foreach (var directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory);
            }

            try
            {
                DeleteDirectoryRecursive(path);
            }
            catch (IOException)
            {
                DeleteDirectoryRecursive(path);
            }
            catch (UnauthorizedAccessException)
            {
                DeleteDirectoryRecursive(path);
            }
        }

        public virtual void DeleteFile(string filePath)
        {
            if (!FileExists(filePath))
            {
                return;
            }

            File.Delete(filePath);
        }

        public virtual bool DirectoryExists(string path) => Directory.Exists(path);

        public virtual void DirectoryMove(string sourceDirName, string destDirName) => Directory.Move(sourceDirName, destDirName);

        public virtual IEnumerable<string> EnumerateFiles(string directoryPath, string searchPattern,
            bool topDirectoryOnly = true)
        {
            return Directory.EnumerateFiles(directoryPath, searchPattern,
                topDirectoryOnly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);
        }

        public virtual void FileCopy(string sourceFileName, string destFileName, bool overwrite = false) => File.Copy(sourceFileName, destFileName, overwrite);

        public virtual bool FileExists(string filePath) => File.Exists(filePath);

        public virtual long FileLength(string path)
        {
            if (!FileExists(path))
            {
                return -1;
            }

            return new FileInfo(path).Length;
        }

        public virtual void FileMove(string sourceFileName, string destFileName) => File.Move(sourceFileName, destFileName);

        public virtual string GetAbsolutePath(params string[] paths)
        {
            var allPaths = paths.ToList();
            allPaths.Insert(0, Root);

            return Path.Combine(allPaths.ToArray());
        }

        public virtual DateTime GetCreationTime(string path)
        {
            return File.GetCreationTime(path);
        }

        public virtual string[] GetDirectories(string path, string searchPattern = "", bool topDirectoryOnly = true)
        {
            if (string.IsNullOrEmpty(searchPattern))
                searchPattern = "*";

            return Directory.GetDirectories(path, searchPattern,
                topDirectoryOnly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);
        }

        public virtual string GetDirectoryName(string path)
        {
            return Path.GetDirectoryName(path);
        }

        public virtual string GetDirectoryNameOnly(string path)
        {
            return new DirectoryInfo(path).Name;
        }

        public virtual string GetFileExtension(string filePath)
        {
            return Path.GetExtension(filePath);
        }

        public virtual string GetFileName(string path)
        {
            return Path.GetFileName(path);
        }

        public virtual string GetFileNameWithoutExtension(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        public virtual string[] GetFiles(string directoryPath, string searchPattern = "", bool topDirectoryOnly = true)
        {
            if (string.IsNullOrEmpty(searchPattern))
                searchPattern = "*.*";

            return Directory.GetFiles(directoryPath, searchPattern,
                topDirectoryOnly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);
        }

        public virtual DateTime GetLastAccessTime(string path)
        {
            return File.GetLastAccessTime(path);
        }

        public virtual DateTime GetLastWriteTime(string path)
        {
            return File.GetLastWriteTime(path);
        }


        public virtual DateTime GetLastWriteTimeUtc(string path)
        {
            return File.GetLastWriteTimeUtc(path);
        }

        public virtual string GetParentDirectory(string directoryPath)
        {
            return Directory.GetParent(directoryPath).FullName;
        }

        public virtual bool IsDirectory(string path)
        {
            return DirectoryExists(path);
        }

        public virtual string MapPath(string path)
        {
            path = path.Replace("~/", string.Empty).TrimStart('/').Replace('/', '\\');
            return Path.Combine(BaseDirectory ?? string.Empty, path);
        }

        public virtual byte[] ReadAllBytes(string filePath)
        {
            return File.Exists(filePath) ? File.ReadAllBytes(filePath) : new byte[0];
        }

        public virtual string ReadAllText(string path, Encoding encoding)
        {
            return File.ReadAllText(path, encoding);
        }

        public virtual void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
        {
            File.SetLastWriteTimeUtc(path, lastWriteTimeUtc);
        }

        public virtual void WriteAllBytes(string filePath, byte[] bytes)
        {
            File.WriteAllBytes(filePath, bytes);
        }

        public virtual void WriteAllText(string path, string contents, Encoding encoding)
        {
            File.WriteAllText(path, contents, encoding);
        }

        protected string BaseDirectory { get; }
    }
}