using System;
using System.Runtime.Caching;
using F1Solutions.InfrastructureStatistics.ApiCalls.Utils;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Helpers
{
    public static class CacheHelper
    {
        static readonly MemoryCache _cache = MemoryCache.Default;
        public static void SaveToCache(string cacheKey, object savedItem, DateTime expirationTime)
        {
            _cache.Add(cacheKey, savedItem, expirationTime);
        }

        public static T GetFromCache<T>(string cacheKey) where T : class
        {
            return (T)_cache.Get(cacheKey);
        }

        //public static void RemoveFromCache(string cacheKey)
        //{
        //    _cache.Remove(Constants.CacheKey);
        //}

        //public static bool IsInCache(string cacheKey)
        //{
        //    return _cache.Contains(Constants.CacheKey);
        //}
    }
}