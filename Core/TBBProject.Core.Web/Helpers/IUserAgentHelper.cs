using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using TBBProject.Core.Common;

namespace TBBProject.Core.Web
{
    public interface IUserAgentHelper
    {
        bool IsSearchEngine();
    }

    public class UserAgentHelper : IUserAgentHelper
    {
        private static readonly object _locker = new object();

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileSystemFileProvider _fileProvider;

        public UserAgentHelper(IHttpContextAccessor httpContextAccessor, IFileSystemFileProvider fileProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _fileProvider = fileProvider;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        protected virtual BrowscapXmlHelper GetBrowscapXmlHelper()
        {
            if (Singleton<BrowscapXmlHelper>.Instance != null)
                return Singleton<BrowscapXmlHelper>.Instance;
            //var browserCapPath = @"~/App_Data\browscap.xml";

            var browserCapPath = Path.Combine("App_Data", "browscap.xml");
            //no database created
            if (string.IsNullOrEmpty(browserCapPath))
                return null;

            //prevent multi loading data
            lock (_locker)
            {
                //data can be loaded while we waited
                if (Singleton<BrowscapXmlHelper>.Instance != null)
                    return Singleton<BrowscapXmlHelper>.Instance;
 
                var crawlerOnlyUserAgentStringsPath = string.Empty;

                var browscapXmlHelper = new BrowscapXmlHelper(browserCapPath, browserCapPath, _fileProvider);
                Singleton<BrowscapXmlHelper>.Instance = browscapXmlHelper;

                return Singleton<BrowscapXmlHelper>.Instance;
            }
        }

        public virtual bool IsSearchEngine()
        {
            if (_httpContextAccessor?.HttpContext == null)
                return false;

            try
            {
                var browscapXmlHelper = GetBrowscapXmlHelper();

                //we cannot load parser
                if (browscapXmlHelper == null)
                    return false;

                var userAgent = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.UserAgent];
                return !string.IsNullOrWhiteSpace(userAgent) && browscapXmlHelper.IsCrawler(userAgent);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                // ignored
            }

            return false;
        }
    }

    public class BrowscapXmlHelper
    {
        private Regex _crawlerUserAgentsRegexp;
        private readonly IFileSystemFileProvider _fileProvider;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userAgentStringsPath">User agent file path</param>
        /// <param name="crawlerOnlyUserAgentStringsPath">User agent with crawlers only file path</param>
        /// <param name="fileProvider">File provider</param>
        public BrowscapXmlHelper(string userAgentStringsPath, string crawlerOnlyUserAgentStringsPath, IFileSystemFileProvider fileProvider)
        {
            _fileProvider = fileProvider;

            Initialize(userAgentStringsPath, crawlerOnlyUserAgentStringsPath);
        }

        private static bool IsBrowscapItemIsCrawler(XElement browscapItem)
        {
            var el = browscapItem.Elements("item").FirstOrDefault(e => e.Attribute("name")?.Value == "Crawler");

            return el != null && el.Attribute("value")?.Value.ToLower() == "true";
        }

        private static string ToRegexp(string str)
        {
            var sb = new StringBuilder(Regex.Escape(str));
            sb.Replace("&amp;", "&").Replace("\\?", ".").Replace("\\*", ".*?");
            return $"^{sb}$";
        }

        private void Initialize(string userAgentStringsPath, string crawlerOnlyUserAgentStringsPath)
        {
            List<XElement> crawlerItems = null;
            var needSaveCrawlerOnly = false;

            if (!string.IsNullOrEmpty(crawlerOnlyUserAgentStringsPath) && _fileProvider.FileExists(crawlerOnlyUserAgentStringsPath))
            {
                //try to load crawler list from crawlers only file
                using (var sr = new StreamReader(crawlerOnlyUserAgentStringsPath))
                {
                    crawlerItems = XDocument.Load(sr).Root?.Elements("browscapitem").ToList();
                }
            }

            if (crawlerItems == null || !crawlerItems.Any())
            {
                //try to load crawler list from full user agents file
                using (var sr = new StreamReader(userAgentStringsPath))
                {
                    crawlerItems = XDocument.Load(sr).Root?.Element("browsercapitems")?.Elements("browscapitem")
                        //only crawlers
                        .Where(IsBrowscapItemIsCrawler).ToList();
                    needSaveCrawlerOnly = true;
                }
            }

            if (crawlerItems == null || !crawlerItems.Any())
                throw new Exception("Incorrect file format");

            var crawlerRegexpPattern = string.Join("|",
                crawlerItems
                //get only user agent names
                .Select(e => e.Attribute("name"))
                .Where(e => !string.IsNullOrEmpty(e?.Value))
                .Select(e => e.Value)
                .Select(ToRegexp));

            _crawlerUserAgentsRegexp = new Regex(crawlerRegexpPattern);

            if (( string.IsNullOrEmpty(crawlerOnlyUserAgentStringsPath) || _fileProvider.FileExists(crawlerOnlyUserAgentStringsPath) ) && !needSaveCrawlerOnly)
                return;

            //try to write crawlers file
            using (var sw = new StreamWriter(crawlerOnlyUserAgentStringsPath))
            {
                var root = new XElement("browsercapitems");

                foreach (var crawler in crawlerItems)
                {
                    foreach (var element in crawler.Elements().ToList())
                    {
                        if (( element.Attribute("name")?.Value.ToLower() ?? string.Empty ) == "crawler")
                            continue;
                        element.Remove();
                    }

                    root.Add(crawler);
                }

                root.Save(sw);
            }
        }

        /// <summary>
        /// Determines whether a user agent is a crawler
        /// </summary>
        /// <param name="userAgent">User agent string</param>
        /// <returns>True if user agent is a crawler, otherwise - false</returns>
        public bool IsCrawler(string userAgent)
        {
            return _crawlerUserAgentsRegexp.IsMatch(userAgent);
        }
    }

}
