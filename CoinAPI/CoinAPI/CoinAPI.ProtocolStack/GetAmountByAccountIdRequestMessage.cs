using KJFramework.Messages.Attributes;
using KJFramework.Net.Transaction.Identities;
using KJFramework.Net.Transaction.Messages;
namespace MobiSage.AdsAPI.ABS.ProtocolStack
{
    /// <summary>
    ///     创建获取账号余额的请求信息
    /// </summary>
    public class GetAmountByAccountIdRequestMessage : BaseMessage
    {
        #region Constructor

        /// <summary>
        ///     创建获取账号余额的请求信息
        /// </summary>
        public GetAmountByAccountIdRequestMessage()
        {
            MessageIdentity = new MessageIdentity { ProtocolId = 1, ServiceId = 247, DetailsId = 0 };
        }

        #endregion

        #region Members

        /// <summary>
        ///     获取或设置账户编号
        /// </summary>
        [IntellectProperty(10, AllowDefaultNull = true)]
        public ulong AccountId { get; set; }

        #endregion

    }
}
