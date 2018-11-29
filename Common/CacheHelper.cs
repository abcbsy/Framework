using CacheManager.Core;
using CacheManager.Serialization.Json;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class CacheManager
    {
        public static IMemoryCache MemoryCache;
        public static ICacheManager<object> MultilevelCache;
        public static void ConfigMemoryCache(IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;
        }
        public static void ConfigMultilevelCache(ICacheManager<object> multilevelCache)
        {
            MultilevelCache = multilevelCache;
        }
        //public static ICacheManager<object> MultilevelCache
        //{
        //    get
        //    {
        //        if (_ICacheManager == null)
        //            _ICacheManager = CacheFactory.Build("UsersCenter", settings =>
        //            {
        //                settings.WithRedisConfiguration("redis", "10.2.21.216:6380,ssl=false,password=", 10)
        //                .WithMaxRetries(1000)//尝试次数
        //                .WithRetryTimeout(100)//尝试超时时间
        //                                      //.WithRedisBackplane("redis")//redis使用Back plane
        //                .WithSerializer(typeof(JsonCacheSerializer), null)
        //                .WithRedisCacheHandle("redis", true)//redis缓存handle
        //                ;
        //            });
        //        return _ICacheManager;
        //    }
        //}
    }
    public static class CacheHelper
    {
        public static void ToMemoryCache(this object value, string key, double seconds = 0)
        {
            var cache = CacheManager.MemoryCache;
            cache.Set<object>(key, value);
            if (seconds > 0)
            {
                cache.Set<object>(key, value, TimeSpan.FromSeconds(seconds));
            }
            else
            {
                cache.Set<object>(key, value);
            }
        }

        public static T FromMemoryCache<T>(this string key)
        {
            return CacheManager.MemoryCache.Get<T>(key);
        }

        public static void ToCache(this object value, string key, double seconds = 0)
        {
            var cache = CacheManager.MultilevelCache;
            cache.AddOrUpdate(key, value, v => v);
            if (seconds > 0)
            {
                cache.Expire(key, TimeSpan.FromSeconds(seconds));
            }
        }

        public static void KeyExpire(this string key, TimeSpan timeSpan)
        {
            CacheManager.MultilevelCache.Expire(key, timeSpan);
        }

        public static T FromCache<T>(this string key)
        {
            return CacheManager.MultilevelCache.Get<T>(key);
        }
        public static void RemoveCache(this string key)
        {
            CacheManager.MultilevelCache.Remove(key);
        }
        //public static void BatchRemoveCache(this string context)
        //{
        //    var cache = CacheManager.Instance;
        //    var list = cache.SearchKeys(context);
        //    foreach (string i in list)
        //    {
        //        Instance.Remove(i.Substring(i.IndexOf(':') + 1));
        //    }
        //}
        public static long Increment(this string key, long increment = 1)
        {
            var v = key.FromCache<long>();
            v += increment;
            v.ToCache(key);
            return v;
        }
    }
}
