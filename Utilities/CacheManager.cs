using CacheManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class CacheManager
    {

        
        private static ICacheManager<object> cache = CacheFactory.Build(settings => settings
       .WithUpdateMode(CacheUpdateMode.Up)
       .WithSystemRuntimeCacheHandle());
        


        public static string Get(string key)
        {
            return cache.Get(key).ToString();
        }

        public static void Set(string key, object value)
        {
            cache.Put(key, value);
        }

        public static void Remove(string key)
        {
            cache.Remove(key);
        }

        public static bool HasKey(string key)
        {           
            return cache.Exists(key);
        }

        public static void SetWithTimeout(string key, object value, TimeSpan expireAfter)
        {
            cache.Put(key, value);
            cache.Put(key + "ExpDate", DateTime.Now.Add(expireAfter));
        }

        public static object GetWithTimeout(string key)
        {
            var value = cache.Get(key).ToString();
            if (value == null) return null;

            DateTime? expDate = cache.Get(key + "ExpDate") as DateTime?;
            if (expDate == null) return null;

            if (expDate < DateTime.Now)
            {
                cache.Remove(key);
                cache.Remove(key + "ExpDate");
                return null;
            }

            return value;
        }
    }
}
