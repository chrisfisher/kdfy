using System;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace Friendly.Infrastructure
{
    public interface ICache
    {
        T Get<T>(string key, Func<T> getWhenNotInCache);
    }

    public class Cache : ICache
    {
        private readonly IDatabase _database;
        private readonly TimeSpan _cacheExpiration;

        public Cache(string redisConnectionString, TimeSpan cacheExpiration)
        {
            _cacheExpiration = cacheExpiration;
            var connection = ConnectionMultiplexer.Connect(redisConnectionString);
            _database = connection.GetDatabase();
        }

        public T Get<T>(string key, Func<T> getWhenNotInCache)
        {
            var cachedResult = _database.StringGet(key);

            if (!cachedResult.IsNullOrEmpty && cachedResult.ToString() != "[]")
                return JsonConvert.DeserializeObject<T>(cachedResult);
            
            var result = getWhenNotInCache();
            var serializedResult = JsonConvert.SerializeObject(result);
            _database.StringSet(key, serializedResult);

            return result;
        }
    }
}