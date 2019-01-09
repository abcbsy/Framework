using CacheManager.Core;
using CacheManager.Serialization.Json;
using Common;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UsersCenter.Models;
using UsersCenter.Services;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Startup();
            UsersService service = new UsersService();
            var obj = service.GetObject(new UsersInfo() { UserAccount = "jack", Password = StringEncrypt.EncryptWithMD5("123456") });
            var m = service.GetObject(1);
        }

        public void Startup()
        {
            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                //.SetBasePath("")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);                                                                
            var Configuration = builder.Build();

            ConfigurationManager.Config(Configuration);

            Common.CacheManager.ConfigMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            Common.CacheManager.ConfigMultilevelCache(CacheManager.Core.CacheFactory.Build("UsersCenter", settings =>
            {
                settings.WithRedisConfiguration("redis", "10.2.21.216:6380,ssl=false,password=", 10)
                .WithMaxRetries(1000)//尝试次数
                .WithRetryTimeout(100)//尝试超时时间
                                      //.WithRedisBackplane("redis")//redis使用Back plane
                .WithSerializer(typeof(JsonCacheSerializer), null)
                .WithRedisCacheHandle("redis", true)//redis缓存handle
                ;
            }));
        }
    }
}
