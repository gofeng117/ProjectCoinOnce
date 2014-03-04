using KJFramework.Messages.Contracts;
using KJFramework.Messages.ValueStored;
using KJFramework.Net.Transaction;
using KJFramework.Net.Transaction.Attribute;
using KJFramework.Net.Transaction.Identities;
using KJFramework.Net.Transaction.Processors;
using KJFramework.Net.Transaction.ValueStored;
using CoinAPI.CommonFramework.Enums;

namespace CoinAPI.Components.AdComp.Processors.Basic
{
    /// <summary>
    ///     消息处理器基础类
    /// </summary>
    public abstract class MessageProcessorBase :
        IMessageTransactionProcessor<MetadataMessageTransaction, MetadataContainer>
    {
        #region Methods

        /// <summary>
        ///     处理一个事务
        /// </summary>
        /// <param name="transaction">消息事务</param>
        public virtual void Process(MetadataMessageTransaction transaction)
        {
            MetadataContainer reqMsg = transaction.Request;
            if (!reqMsg.IsAttibuteExsits(0x01))
            {
                transaction.SendResponse(GetErrorResponseMessage(SystemErrors.NotFound, "#TransactionIdentity信息不存在"));
                return;
            }
            string errorMsg;
            if (!CheckDataFields(reqMsg, out errorMsg))
            {
                transaction.SendResponse(GetErrorResponseMessage(SystemErrors.NotFound, errorMsg));
                return;
            }
            CheckSQS(reqMsg, transaction);
        }

        /// <summary>
        ///     检查调用频率,后期扩展
        /// </summary>
        /// <param name="reqMsg">请求消息</param>
        /// <param name="transaction">当前网络消息事务</param>
        protected void CheckSQS(MetadataContainer reqMsg, MetadataMessageTransaction transaction)
        {
            string[] names = GetSQSNames();
            if (names == null || names.Length != 2 || string.IsNullOrEmpty(names[0]) || string.IsNullOrEmpty(names[1]))
            {
                transaction.SendResponse(GetErrorResponseMessage(SystemErrors.Unknown, "#当前消息处理器无法返回相关的SQS业务名称 #" + GetType().Name));
                return;
            }
            DoBussiness(reqMsg, transaction);
        }

        /// <summary>
        ///     获取当前信令的错误应答消息
        /// </summary>
        /// <param name="errorId">错误编号</param>
        /// <param name="reason">错误原因</param>
        /// <returns>返回当前信令的错误应答消息</returns>
        protected MetadataContainer GetErrorResponseMessage(byte errorId, string reason)
        {
            ProcessorMessageIdentityAttribute attr = (ProcessorMessageIdentityAttribute)GetType().GetCustomAttributes(typeof(ProcessorMessageIdentityAttribute), true)[0];
            MetadataContainer rspMsg = new MetadataContainer();
            rspMsg.SetAttribute(0x00, new MessageIdentityValueStored(new MessageIdentity { ProtocolId = attr.ProtocolId, ServiceId = attr.ServiceId, DetailsId = (byte)(attr.DetailsId + 1) }));
            rspMsg.SetAttribute(0x0A, new ByteValueStored(errorId));
            rspMsg.SetAttribute(0x0B, new StringValueStored(reason));
            return rspMsg;
        }
        /// <summary>
        ///     开始处理业务操作
        ///     <para>* 在调用此方法之前，已经完成了用户权限的验证以及SQS操作。</para>
        /// </summary>
        /// <param name="reqMsg">请求消息</param>
        /// <param name="transaction">当前网络消息事务</param>
        protected abstract void DoBussiness(MetadataContainer reqMsg, MetadataMessageTransaction transaction);
        /// <summary>
        ///     获取SQS需要的服务名称以及信令名称
        ///     <para>* Index 0: service name</para>
        ///     <para>* Index 1: business name</para>
        /// </summary>
        /// <returns>返回SQS需要的服务名称以及信令名称</returns>
        protected abstract string[] GetSQSNames();
        /// <summary>
        ///     检查当前业务字段是否合法
        /// </summary>
        /// <param name="reqMsg">请求消息</param>
        /// <param name="errorMsg">返回错误信息</param>
        /// <returns>返回是否合法</returns>
        protected abstract bool CheckDataFields(MetadataContainer reqMsg, out string errorMsg);

        #endregion
    }
}
