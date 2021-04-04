using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TBBProject.Core.Web
{
    public class ResponseCacheProvider
    {
        private CacheProfile Default()
        {
            var cache = new CacheProfile()
            {
                Duration = 3600,
                Location = ResponseCacheLocation.Any
            };
            return cache;
        }
        private CacheProfile Never()
        {
            var cache = new CacheProfile()
            {
                Location = ResponseCacheLocation.None,
                NoStore = true
            };
            return cache;
        }
        private CacheProfile TwoHours()
        {
            var cache = new CacheProfile()
            {
                Duration = 7200,
                Location = ResponseCacheLocation.Any
            };
            return cache;
        }
        private CacheProfile ThreeHours()
        {
            var cache = new CacheProfile()
            {
                Duration = 10800,
                Location = ResponseCacheLocation.Any 
            };
            return cache;
        }
        private CacheProfile OneDay()
        {
            var cache = new CacheProfile()
            {
                Duration = 86400,
                Location = ResponseCacheLocation.Any
            };
            return cache;
        }
        public Dictionary<string, CacheProfile> GetCacheProfiles()
        {
            var cacheProfiles = new Dictionary<string, CacheProfile>
            {
                { ResponseCacheProfile.Never.ToString(), Never() },
                { ResponseCacheProfile.Default.ToString(), Default() },
                { ResponseCacheProfile.TwoHours.ToString(), TwoHours() },
                { ResponseCacheProfile.ThreeHours.ToString(), ThreeHours() },
                { ResponseCacheProfile.OneDay.ToString(), OneDay() }
            };

            return cacheProfiles;
        }

    }
}