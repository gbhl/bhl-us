using MOBOT.BHL.DAL;
using MOBOT.BHL.DataObjects;
using System;

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
            string value = ApplicationCacheGet(key);
            if (value == null) value = (new ApplicationCacheDAL().ApplicationCacheInsertAuto(null, null, key, data, expiration, slidingExpirationDuration, DateTime.Now)).CacheData;
            return value;
        }

        public bool ApplicationCacheRemove(string key)
        {
            return new ApplicationCacheDAL().ApplicationCacheDeleteAuto(null, null, key);
        }
    }
}
