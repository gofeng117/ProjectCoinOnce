using CoinAPI.CommonFramework.Core;
using CoinAPI.CommonFramework.Enums;
using CoinAPI.Domain.Enums;
using CoinAPI.Domain.Midiator.Converters;
using CoinAPI.Domain.Results;
using KJFramework.EventArgs;
using KJFramework.Messages.Contracts;
using KJFramework.Messages.Types;
using KJFramework.Messages.ValueStored;
using KJFramework.Net.Transaction;
using KJFramework.Net.Transaction.Identities;
using KJFramework.Net.Transaction.ValueStored;
using KJFramework.Tracing;
using System.Threading.Tasks;

namespace CoinAPI.Domain.Midiator.Proxies
{
    /// <summary>
    ///     CoinAPI远程代理器
    /// </summary>
    public sealed class CoinAPIRemoteProxy
    {
        #region Constructor

        /// <summary>
        ///     CoinAPI远程代理器
        /// </summary>
        private CoinAPIRemoteProxy()
        {

        }

        #endregion

        #region Members

        /// <summary>
        ///     CoinAPI远程代理器
        /// </summary>
        public static readonly CoinAPIRemoteProxy Instance = new CoinAPIRemoteProxy();
        private static readonly ITracing _tracing = TracingManager.GetTracing(typeof(CoinAPIRemoteProxy));

        #endregion

        #region Methods

        /// <summary>
        ///     根据指定编号异步获取Ticker对象的操作
        /// </summary>
        /// <param name="coinType">币种编号</param>
        /// <param name="platformType">平台编号</param>
        /// <returns>返回执行结果</returns>
        public async Task<IExecuteResult> GetTickerAsync(CoinTypes coinType, PlatformTypes platformType)
        {
            MetadataMessageTransaction transaction = SystemWorker.Instance.CreateMetadataTransaction("CoinAPI");
            MetadataContainer reqMsg = new MetadataContainer();
            reqMsg.SetAttribute(0x00, new MessageIdentityValueStored(new MessageIdentity
            {
                ProtocolId = 1,
                ServiceId = 0,
                DetailsId = 0
            }));
            reqMsg.SetAttribute(0x0F, new ByteValueStored((byte) coinType));
            reqMsg.SetAttribute(0x10, new ByteValueStored((byte) platformType));
            TaskCompletionSource<IExecuteResult> completionSource = new TaskCompletionSource<IExecuteResult>();
            Task<IExecuteResult> task = completionSource.Task;
            transaction.ResponseArrived += delegate(object sender, LightSingleArgEventArgs<MetadataContainer> e)
            {
                MetadataContainer rspMsg = e.Target;
                completionSource.SetResult((!rspMsg.IsAttibuteExsits(0x0A))
                             ? (rspMsg.GetAttribute(0x0F).GetValue<ResourceBlock>() == null
                                 ? ExecuteResult.Succeed(null)
                                 : ExecuteResult.Succeed(ConverterFactory.GetTickerConverter().ConvertToDomainObject(rspMsg.GetAttribute(0x0F).GetValue<ResourceBlock>())))
                             : ExecuteResult.Fail(rspMsg.GetAttribute(0x0A).GetValue<byte>(), rspMsg.GetAttribute(0x0B).GetValue<string>()));
            };
            transaction.Timeout += delegate { completionSource.SetResult(ExecuteResult.Fail(SystemErrors.Timeout, string.Format("[Async Handle] Transaction: {0} execute timeout!", transaction.Identity))); };
            transaction.Failed += delegate { completionSource.SetResult(ExecuteResult.Fail(SystemErrors.Unknown, string.Format("[Async Handle] Transaction: {0} execute failed!", transaction.Identity))); };
            transaction.SendRequest(reqMsg);
            return await task;
        }

        #endregion
    }
}