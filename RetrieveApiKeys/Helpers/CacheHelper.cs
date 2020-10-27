using System;
using System.Runtime.Caching;
using F1Solutions.InfrastructureStatistics.ApiCalls.Utils;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Helpers
{
    public static class CacheHelper
    {
        static readonly MemoryCache _cache = MemoryCache.Default;

        public static void SaveToCache(string cacheKey, object objectToSaveToCache)
        {
            var cacheItemPolicy = new CacheItemPolicy();

            if (!_cache.Contains(cacheKey))
            {
                
                _cache.Set(cacheKey,objectToSaveToCache,cacheItemPolicy);
                _cache.Set("CacheExpiryValue",DateTime.Now.AddHours(4), cacheItemPolicy);
            }
        }

        public static DateTime GetCacheExpiryValue()
        {
            var cacheExpiryDate = Convert.ToDateTime(_cache.Get("CacheExpiryValue"));

            return cacheExpiryDate;
        }

        public static T GetFromCache<T>(string cacheKey) where T : class
        {
            return (T)_cache.Get(cacheKey);
        }

        public static void RemoveFromCache(string cacheKey)
        {
            _cache.Remove(Constants.CacheKey);
        }

        public static bool IsInCache(string cacheKey)
        {
            return _cache.Contains(Constants.CacheKey);
        }

    }
}

