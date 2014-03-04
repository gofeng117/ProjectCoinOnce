using System;
using System.Data;
using CoinAPI.CommonFramework.Extends;
using CoinAPI.Domain.Business;
using KJFramework.Messages.Contracts;
using KJFramework.Messages.Types;
using KJFramework.Messages.ValueStored;

namespace CoinAPI.Domain.Midiator.Converters
{
    /// <summary>
    ///     Ticker领域对象与网络对象转换器
    /// </summary>
    public class TickerConverter 
    {
        #region Methods

        /// <summary>
        ///     将一个网络对象转换为领域模型对象
        /// </summary>
        /// <param name="data">Ticker网络数据</param>
        /// <returns>返回转换回来的结果</returns>
        public Ticker ConvertToDomainObject(ResourceBlock data)
        {
            if (data == null) return null;

            Ticker ticker = new Ticker
            {
                BuyPrice = data.GetAttributeByIdSafety<double>(0x00),
                SellPrice = data.GetAttributeByIdSafety<double>(0x01),
                HignPrice = data.GetAttributeByIdSafety<double>(0x02),
                LowPrice = data.GetAttributeByIdSafety<double>(0x03),
                LastPrice = data.GetAttributeByIdSafety<double>(0x04),
                Vol = data.GetAttributeByIdSafety<double>(0x05)
            };

            return ticker;
        }

        /// <summary>
        ///     将数据表中的数据包装成一个领域对象
        /// </summary>
        /// <param name="row">数据表</param>
        /// <returns>返回领域对象</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public Ticker PackToDomainObject(DataRow row)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     将一个领域模型对象转换为网络对象
        /// </summary>
        /// <param name="ticker">Ticker对象</param>
        /// <returns>返回转换后的网络对象</returns>
        public ResourceBlock ConvertToNetworkObject(Ticker ticker)
        {
            if (ticker == null) return null;

            ResourceBlock metadata = new MetadataContainer();

            metadata.SetAttribute(0x00, new DoubleValueStored(ticker.BuyPrice));
            metadata.SetAttribute(0x01, new DoubleValueStored(ticker.SellPrice));
            metadata.SetAttribute(0x02, new DoubleValueStored(ticker.HignPrice));
            metadata.SetAttribute(0x03, new DoubleValueStored(ticker.LowPrice));
            metadata.SetAttribute(0x04, new DoubleValueStored(ticker.LastPrice));
            metadata.SetAttribute(0x05, new DoubleValueStored(ticker.Vol));

            return metadata;
        }

        #endregion
    }
}