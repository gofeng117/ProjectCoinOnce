using MobiSage.AdsAPI.Domain.Results;

namespace MobiSage.AdsAPI.Domain.Repository
{
    /// <summary>
    /// 账户落地借口
    /// </summary>
    interface IAdAccountRepository
    {
        ///// <summary>
        /////     新增AccountInBc
        ///// </summary>
        ///// <param name="userId">用户编码</param>
        ///// <param name="accountId">账户编码</param>
        ///// <param name="customId">广告主编号</param>
        ///// <returns>执行结果</returns>
        //IExecuteResult CreateAccountInBc(uint userId, uint accountId, ushort customId);

        /// <summary>
        ///     正常新建Account进入BC落地
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="accountId">账户ID</param>
        /// <param name="sourceId">账户来源编号</param>
        /// <returns>结果</returns>
        IExecuteResult CreateAccountNormal(uint userId, uint accountId,ushort sourceId);

        /// <summary>
        ///     新建AccountId与CustomId的关系在OA中
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="accountId">账户ID</param>
        /// <param name="customId">广告主ID</param>
        /// <returns>结果</returns>
        IExecuteResult CreateCustomerRel(uint userId, uint accountId, ushort customId);

        /// <summary>
        ///     修改账户类型（目前只对自投放开放）
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="accountId">账户ID</param>
        /// <param name="typeId">类型ID *自投放默认为3</param>
        /// <returns>结果</returns>
        IExecuteResult UpdateAccountType(uint userId, uint accountId, uint typeId = 3);

    }
}
