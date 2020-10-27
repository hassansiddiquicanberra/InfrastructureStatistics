using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Web.Caching;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
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

        public static void SaveTimeEntriesToCache(string cacheKey, TimeEntry timeEntry, DateTime expirationTime, bool willUpdateTimeEntryObject = false)
        {
            if (willUpdateTimeEntryObject)
            {
                var allCacheObjects = _cache.Get(cacheKey);
                var casteCacheObjects = (List<CachedModel>)allCacheObjects;

                foreach (var cachedObject in casteCacheObjects)
                {
                    cachedObject.CachedTimeEntry = new TimeEntry()
                    {
                        CreatedAt = timeEntry.CreatedAt,
                        Billable = timeEntry.Billable,
                        OwnerId = timeEntry.OwnerId,
                        Status = timeEntry.Status,
                        TimeSpent = timeEntry.TimeSpent,
                        UpdatedAt = timeEntry.UpdatedAt,
                        Urgency = timeEntry.Urgency
                    };
                }
            }

            var checkCachedObjectsAfterFinalUpdate = _cache.Get(cacheKey);
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

