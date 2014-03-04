using System;
using CoinAPI.Domain.Enums;
using Newtonsoft.Json;

namespace CoinAPI.Domain.Business
{
    /// <summary>
    ///     交易信息
    /// </summary>
    public class Tradescs
    {
        private CoinTypes _coinType;
        private Ticker _ticker;
        private PlatformTypes _platformType;
        private DateTime _nowTime;

        /// <summary>
        ///      构造器
        /// </summary>
        /// <param name="coinType"></param>
        /// <param name="ticker"></param>
        /// <param name="platformTypes"></param>
        /// <param name="dateTime"></param>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public Tradescs(CoinTypes coinType, Ticker ticker, PlatformTypes platformTypes, DateTime dateTime)
        {
            if(ticker ==null) throw new ArgumentNullException("ticker");
            _coinType = coinType;
            _ticker = ticker;
            _platformType = platformTypes;
            _nowTime = dateTime;
        }

        /// <summary>
        ///      获取ticker的json串表现形式
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(_ticker);
        }
    }
}
