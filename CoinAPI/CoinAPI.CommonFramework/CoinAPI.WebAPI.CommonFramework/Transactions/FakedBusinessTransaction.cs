using System;
using System.Net;
using KJFramework.Net.Transaction;
using KJFramework.Net.Transaction.Helpers;

namespace CoinAPI.CommonFramework.Transactions
{
    /// <summary>
    ///     虚拟的消息事物，仅用于单元测试中使用
    /// </summary>
    public class FakedBusinessTransaction : BusinessMessageTransaction
    {
        #region Constructor

        /// <summary>
        ///     虚拟的消息事物，仅用于单元测试中使用
        /// </summary>
        public FakedBusinessTransaction()
        {
            Identity = IdentityHelper.Create(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 10000));
        }

        #endregion

        #region Methods

        /// <summary>
        ///     发送一个请求消息
        /// </summary>
        /// <param name="message">请求消息</param>
        /// <exception cref="System.ArgumentNullException">参数不能为空</exception>
        public override void SendRequest(KJFramework.Net.Transaction.Messages.BaseMessage message)
        {
            if (message == null) throw new ArgumentNullException("message");
            _request = message;
        }

        /// <summary>
        ///     发送一个响应消息
        /// </summary>
        /// <param name="message">响应消息</param>
        /// <exception cref="System.ArgumentNullException">参数不能为空</exception>
        public override void SendResponse(KJFramework.Net.Transaction.Messages.BaseMessage message)
        {
            if (message == null) throw new ArgumentNullException("message");
            _response = message;
        }

        /// <summary>
        ///     设置一个应答消息
        /// </summary>
        /// <param name="response">应答消息</param>
        /// <exception cref="System.ArgumentNullException">参数不能为空</exception>
        public override void SetResponse(KJFramework.Net.Transaction.Messages.BaseMessage response)
        {
            if (response == null) throw new ArgumentNullException("response");
            _response = response;
        }

        #endregion
    }
}