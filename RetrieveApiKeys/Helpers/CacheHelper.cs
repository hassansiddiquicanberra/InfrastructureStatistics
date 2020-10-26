using System;
using System.Web;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Helpers
{
    public static class CacheHelper
    {
        public static void SaveToCache(string cacheKey, object savedItem, DateTime expirationTime)
        {
            if (IsInCache(cacheKey))
            {
                HttpContext.Current.Cache.Remove(cacheKey);
            }

            HttpContext.Current.Cache.Add(cacheKey, savedItem, null, expirationTime, 
                new TimeSpan(5, 0, 0), System.Web.Caching.CacheItemPriority.Normal, null);
        }

        public static T GetFromCache<T>(string cacheKey) where T : class
        {
            return HttpContext.Current.Cache[cacheKey] as T;
        }

        public static void RemoveFromCache(string cacheKey)
        {
            HttpContext.Current.Cache.Remove(cacheKey);
        }

        public static bool IsInCache(string cacheKey)
        {
            return HttpContext.Current.Cache[cacheKey] != null;
        }
    }
}

