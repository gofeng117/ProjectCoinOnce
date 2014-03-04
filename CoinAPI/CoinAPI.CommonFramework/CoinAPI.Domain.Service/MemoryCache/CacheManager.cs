using System;
using System.Runtime.Caching;
using CoinAPI.CommonFramework.Enums;
using CoinAPI.Domain.Business;
using KJFramework.Tracing;
using CoinAPI.Domain.Enums;
using CoinAPI.Domain.Results;
using Newtonsoft.Json;

namespace CoinAPI.Domain.Service.MemoryCache
{
    public class CacheManager
    {
        private static readonly ITracing _tracing = TracingManager.GetTracing(typeof(CacheManager));

        public static readonly CacheManager Instance = new CacheManager();
        private RemoteApiService _remoteApiService = new RemoteApiService();

        private CacheManager()
        {
        }

        private ObjectCache _cache;
        private ObjectCache m_Cache
        {
            get { return _cache ?? (_cache = System.Runtime.Caching.MemoryCache.Default); }
        }

        public IExecuteResult GetValue(CoinTypes coinType, PlatformTypes platformType)
        {
            try
            {
                KeyParse keyParse = new KeyParse { CoinType = coinType, PlatformType = platformType };
                string key = keyParse.ToString();
                string content = m_Cache[key] as string;
                if (content == null)
                {
                    CacheItemPolicy policy = new CacheItemPolicy();
                    policy.AbsoluteExpiration = DateTime.Now.AddMilliseconds(25000);
                    IExecuteResult<Ticker> result = _remoteApiService.GetDataFromDataBase(key);
                    if (result.State != ExecuteResults.Succeed)
                    {
                        return ExecuteResult.Fail(SystemErrors.Unknown, null);
                    }
                    content = result.GetResult().ToString();
                    m_Cache.Set(key, content, policy);
                }
                return ExecuteResult.Succeed(JsonConvert.DeserializeObject<Ticker>(content));
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.NotFound, null);
            }
        }
    }
}
