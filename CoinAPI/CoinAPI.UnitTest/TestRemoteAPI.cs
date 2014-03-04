using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoinAPI.Domain.Business;
using CoinAPI.Domain.Enums;
using CoinAPI.Domain.Results;
using CoinAPI.Domain.Service;
using CoinAPI.Domain.Service.MemoryCache;

namespace CoinAPI.UnitTest
{
    [TestClass]
    public class TestRemoteAPI
    {
        #region method

        private RemoteApiService _remoteApiService;

        #endregion

        [TestInitialize]
        public void Initialze()
        {
            _remoteApiService = new RemoteApiService();
        }

        [TestMethod]
        public void Test1()
        {
            IExecuteResult result =   _remoteApiService.GetBTCTickerFromOkCoin();
            Assert.IsTrue(result.State == ExecuteResults.Succeed);
            if (result.State == ExecuteResults.Succeed)
            {
                Ticker ticker = result.GetResult<Ticker>();
                Console.WriteLine(ticker);
            }
        }

        [TestMethod]
        public void Test2()
        {
            KeyParse keyParse = new KeyParse { CoinType = 0, PlatformType = 0 };
            string key = keyParse.ToString();
            IExecuteResult<Ticker> result = _remoteApiService.GetDataFromDataBase(key);
            Assert.IsTrue(result.State == ExecuteResults.Succeed);
            if (result.State == ExecuteResults.Succeed)
            {
                Ticker sss = result.GetResult();
                Console.WriteLine(sss.ToString());
            }
        }

        [TestMethod]
        public void Test3()
        {
            IExecuteResult sss = CacheManager.Instance.GetValue(0, 0);
            Assert.IsNotNull(sss);
            Console.WriteLine(sss);
            IExecuteResult xxx = CacheManager.Instance.GetValue(0, 0);
            Assert.IsNotNull(xxx);
            Console.WriteLine(xxx);
        }
    }
}
