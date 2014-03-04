using KJFramework.Messages.Attributes;
using KJFramework.Net.Transaction.Identities;
using KJFramework.Net.Transaction.Messages;
namespace MobiSage.AdsAPI.ABS.ProtocolStack
{
    /// <summary>
    ///     创建获取账号余额的应答信息
    /// </summary>
    public class GetAmountByAccountIdResponseMessage:BaseMessage
    {
         #region Constructor

        /// <summary>
        ///     创建获取账号余额的应答信息
        /// </summary>
        public GetAmountByAccountIdResponseMessage()
        {
            MessageIdentity = new MessageIdentity {ProtocolId = 1, ServiceId = 247, DetailsId = 1};
        }

        #endregion

        #region Members

        /// <summary>
        ///     获取或设置错误编号
        /// </summary>
        [IntellectProperty(10)]
        public byte ErrorId { get; set; }
        /// <summary>
        ///     获取或设置错误原因
        /// </summary>
        [IntellectProperty(11)]
        public string Reason { get; set; }
        /// <summary>
        ///     获取或设置账号余额
        /// </summary>
        [IntellectProperty(12, AllowDefaultNull = true)]
        public decimal Amount { get; set; }

        #endregion
    }
}
