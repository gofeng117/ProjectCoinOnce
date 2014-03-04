using CoinAPI.CommonFramework.Enums;
using CoinAPI.Components.AdComp.Processors.Basic;
using CoinAPI.Domain.Business;
using CoinAPI.Domain.Enums;
using CoinAPI.Domain.Midiator.Converters;
using CoinAPI.Domain.Results;
using CoinAPI.Domain.Service.MemoryCache;
using KJFramework.Messages.Contracts;
using KJFramework.Messages.Types;
using KJFramework.Messages.ValueStored;
using KJFramework.Net.Transaction;
using KJFramework.Net.Transaction.Attribute;
using KJFramework.Net.Transaction.Identities;
using KJFramework.Net.Transaction.ValueStored;
using KJFramework.Tracing;

namespace CoinAPI.Components.AdComp.Processors.CoinAPI
{
    /// <summary>
    ///     获取具有指定平台，币种的最新ticker数据信息处理器
    /// </summary>
    [ProcessorMessageIdentity(ProtocolId = 1, ServiceId = 0, DetailsId = 0)]
    public class GetTickerFromOKCoinProcessor : MessageProcessorBase
    {
        #region Members

        private static readonly ITracing _tracing = TracingManager.GetTracing(typeof(GetTickerFromOKCoinProcessor));

        #endregion

        #region Implementation of MessageProcessorBase

        private MetadataContainer GetSucceedResponseMessage(ResourceBlock data)
        {
            MetadataContainer rspMsg = new MetadataContainer();
            rspMsg.SetAttribute(0x00, new MessageIdentityValueStored(new MessageIdentity { ProtocolId = 1, ServiceId = 0, DetailsId = 1 }));
            rspMsg.SetAttribute(0x0F, new ResourceBlockStored(data));
            return rspMsg;
        }

        protected override void DoBussiness(MetadataContainer reqMsg, MetadataMessageTransaction transaction)
        {
            CoinTypes coinType = (CoinTypes) reqMsg.GetAttribute(0x0F).GetValue<byte>();
            PlatformTypes platformType = (PlatformTypes) reqMsg.GetAttribute(0x10).GetValue<byte>();
            IExecuteResult executeResult = CacheManager.Instance.GetValue(coinType, platformType);
            if (executeResult.State != ExecuteResults.Succeed)
                transaction.SendResponse(GetErrorResponseMessage(SystemErrors.Unknown, executeResult.Error));
            else
            {
                ResourceBlock rspCampaignData = ConverterFactory.GetTickerConverter().ConvertToNetworkObject(executeResult.GetResult<Ticker>());
                transaction.SendResponse(GetSucceedResponseMessage(rspCampaignData));
            }
        }

        /// <summary>
        ///     获取SQS需要的服务名称以及信令名称
        ///     <para>* Index 0: service name</para>
        ///     <para>* Index 1: business name</para>
        /// </summary>
        /// <returns>返回SQS需要的服务名称以及信令名称</returns>
        protected override string[] GetSQSNames()
        {
            return new[] { "ABS", "GetCampaignById" };
        }

        /// <summary>
        ///     检查当前业务字段是否合法
        /// </summary>
        /// <param name="reqMsg">请求消息</param>
        /// <param name="errorMsg">返回错误信息</param>
        /// <returns>返回是否合法</returns>
        protected override bool CheckDataFields(MetadataContainer reqMsg, out string errorMsg)
        {
            errorMsg = null;
            if (!reqMsg.IsAttibuteExsits(0x0F)) errorMsg = "#CoinTypeId不存在";
            else if (!reqMsg.IsAttibuteExsits(0x10)) errorMsg = "#PlatformId不存在";
            return errorMsg == null;
        }

        #endregion
    }
}
