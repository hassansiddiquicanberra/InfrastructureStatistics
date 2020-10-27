using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using F1Solutions.InfrastructureStatistics.ApiCalls.Utils;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Helpers
{
    public class CacheHelper
    {
        static readonly MemoryCache _cache = MemoryCache.Default;

        public void SaveToCache(string cacheKey, object objectToSaveToCache, DateTime expirationTime)
        {
            if (!_cache.Contains(cacheKey))
            {
                _cache.Add(cacheKey, objectToSaveToCache, DateTime.Now.AddHours(4.0));
            }
        }

        public void ModifyTimeEntriesInCache(string cacheKey, TimeEntry timeEntry, DateTime expirationTime, int ticketEntryId = 0, bool willUpdateTimeEntryObject = false)
        {
            if (willUpdateTimeEntryObject)
            {
                var allCacheObjects = _cache.Get(cacheKey);
                var casteCacheObjects = (List<CachedModel>)allCacheObjects;

                if (casteCacheObjects != null)
                {
                    casteCacheObjects[ticketEntryId].CachedTimeEntry.CreatedAt = timeEntry.CreatedAt;
                    casteCacheObjects[ticketEntryId].CachedTimeEntry.Billable = timeEntry.Billable;
                    casteCacheObjects[ticketEntryId].CachedTimeEntry.OwnerId = timeEntry.OwnerId;
                    casteCacheObjects[ticketEntryId].CachedTimeEntry.Status = timeEntry.Status;
                    casteCacheObjects[ticketEntryId].CachedTimeEntry.TimeSpent = timeEntry.TimeSpent;
                    casteCacheObjects[ticketEntryId].CachedTimeEntry.UpdatedAt = timeEntry.UpdatedAt;
                    casteCacheObjects[ticketEntryId].CachedTimeEntry.Urgency = timeEntry.Urgency;
                }
            }
        }

        public T GetFromCache<T>(string cacheKey) where T : class
        {
            return (T)_cache.Get(cacheKey);
        }

        public void RemoveFromCache(string cacheKey)
        {
            _cache.Remove(Constants.CacheKey);
        }

        public bool IsInCache(string cacheKey)
        {
            return _cache.Contains(Constants.CacheKey);
        }
    }
}

