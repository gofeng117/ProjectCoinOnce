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
    ///     Ticker����������������ת����
    /// </summary>
    public class TickerConverter 
    {
        #region Methods

        /// <summary>
        ///     ��һ���������ת��Ϊ����ģ�Ͷ���
        /// </summary>
        /// <param name="data">Ticker��������</param>
        /// <returns>����ת�������Ľ��</returns>
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
        ///     �����ݱ��е����ݰ�װ��һ���������
        /// </summary>
        /// <param name="row">���ݱ�</param>
        /// <returns>�����������</returns>
        /// <exception cref="ArgumentNullException">��������Ϊ��</exception>
        public Ticker PackToDomainObject(DataRow row)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     ��һ������ģ�Ͷ���ת��Ϊ�������
        /// </summary>
        /// <param name="ticker">Ticker����</param>
        /// <returns>����ת������������</returns>
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