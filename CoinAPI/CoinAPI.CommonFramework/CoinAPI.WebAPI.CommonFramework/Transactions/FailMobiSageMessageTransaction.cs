using System.Net;
using KJFramework.Net.Channels;
using KJFramework.Net.Transaction;
using KJFramework.Net.Transaction.Identities;
using KJFramework.Net.Transaction.Messages;
using KJFramework.Tracing;

namespace CoinAPI.CommonFramework.Transactions
{
    /// <summary>
    ///     失败的微讯消息事务，提供了相关的基本操作
    /// </summary>
    public class FailMobiSageMessageTransaction : BusinessMessageTransaction
    {
        #region Constrcutor

        /// <summary>
        ///     失败的微讯消息事务，提供了相关的基本操作
        /// </summary>
        public FailMobiSageMessageTransaction(string iep)
        {
            _iep = iep;
            Identity = new TransactionIdentity {EndPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 1000)};
        }

        /// <summary>
        ///     失败的微讯消息事务，提供了相关的基本操作
        /// </summary>
        /// <param name="channel">消息通信信道</param>
        public FailMobiSageMessageTransaction(IMessageTransportChannel<BaseMessage> channel)
            : base(channel)
        {
            Identity = new TransactionIdentity { EndPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 1000) };
        }

        /// <summary>
        ///     失败的微讯消息事务，提供了相关的基本操作
        /// </summary>
        /// <param name="lease">事务生命租期租约</param>
        /// <param name="channel">消息通信信道</param>
        public FailMobiSageMessageTransaction(ILease lease, IMessageTransportChannel<BaseMessage> channel)
            : base(lease, channel)
        {
            Identity = new TransactionIdentity { EndPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 1000) };
        }

        #endregion

        #region Members

        private readonly string _iep;
        private static ITracing _tracing = TracingManager.GetTracing(typeof(FailMobiSageMessageTransaction));

        #endregion

        #region Methods

        /// <summary>
        ///     发送一个请求消息
        /// </summary>
        /// <param name="message">请求消息</param>
        public override void SendRequest(BaseMessage message)
        {
            _tracing.Error("Cannot call method FailWXMessageTransaction.SendRequest! #addr: " + _iep);
            FailedHandler(null);
        }

        /// <summary>
        ///     发送一个响应消息
        /// </summary>
        /// <param name="message">响应消息</param>
        public override void SendResponse(BaseMessage message)
        {
            _tracing.Error("Cannot call method FailWXMessageTransaction.SendResponse! #addr: " + _iep);
            FailedHandler(null);
        }

        #endregion
    }
}