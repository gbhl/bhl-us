using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System;
using System.Windows.Forms;

namespace MOBOT.BHL.Server
{
    public partial class BHLProvider
    {
        public string ApplicationCacheGet(string key)
        {
            ApplicationCache cache = new ApplicationCacheDAL().ApplicationCacheSelectByKey(null, null, key);
            return (cache != null) ? cache.CacheData : null;
        }

        public string ApplicationCacheAdd(string key, string data, DateTime? expiration = null, int? slidingExpirationDuration = null)
        {
            // Read any active data for the specified key from the cache
            string cacheData = ApplicationCacheGet(key);
            if (cacheData == null)
            {
                // Look for an expired cache entry for the specified key
                ApplicationCacheDAL dal = new ApplicationCacheDAL();
                ApplicationCache cache = dal.ApplicationCacheSelectAuto(null, null, key);
                if (cache == null)
                {
                    // No cache entry found, so add a new one
                    cache = dal.ApplicationCacheInsertAuto(null, null, key, data, expiration, slidingExpirationDuration, DateTime.Now);
                }
                else
                {
                    // Expired cache entry found, so update it with fresh data and expiration dates/times
                    cache.CacheData = data;
                    cache.AbsoluteExpirationDate = expiration;
                    cache.SlidingExpirationDuration = slidingExpirationDuration;
                    cache.LastAccessDate = DateTime.Now;
                    dal.ApplicationCacheUpdateAuto(null, null, cache);
                }
                cacheData = cache.CacheData;
            }
            return cacheData;
        }

        public bool ApplicationCacheRemove(string key)
        {
            return new ApplicationCacheDAL().ApplicationCacheDeleteAuto(null, null, key);
        }
    }
}
