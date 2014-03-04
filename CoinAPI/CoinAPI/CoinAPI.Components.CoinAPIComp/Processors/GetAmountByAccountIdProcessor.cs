using KJFramework.Net.Transaction;
using KJFramework.Net.Transaction.Processors;
using KJFramework.Tracing;
using MobiSage.AdsAPI.ABS.ProtocolStack;
using MobiSage.AdsAPI.CommonFramework.Enums;
using MobiSage.AdsAPI.Domain.Enums;
using MobiSage.AdsAPI.Domain.Results;
namespace MobiSage.AdsAPI.ABS.Components.AdComp.Processors
{
    /// <summary>
    ///     创建一个获取账号余额的处理器
    /// </summary>
    public class GetAmountByAccountIdProcessor : IMessageProcessor
    {
        #region Members

        private static readonly ITracing _tracing = TracingManager.GetTracing(typeof(GetAmountByAccountIdProcessor));

        #endregion

        #region Methods

        /// <summary>
        ///     处理一个事务
        /// </summary>
        /// <param name="transaction">消息事务</param>
        public void Process(BusinessMessageTransaction transaction)
        {
            GetAmountByAccountIdRequestMessage reqMsg = (GetAmountByAccountIdRequestMessage) transaction.Request;
            IExecuteResult executeResult = Global.AuthorizationService.GetAmountByAccountId(reqMsg.AccountId);
            transaction.SendResponse(executeResult.State != ExecuteResults.Succeed
                ? new GetAmountByAccountIdResponseMessage{ ErrorId = SystemErrors.Unknown, Reason = executeResult.Error }
                : new GetAmountByAccountIdResponseMessage { Amount = executeResult.GetResult<decimal>() });
        }

        #endregion
    }
}
