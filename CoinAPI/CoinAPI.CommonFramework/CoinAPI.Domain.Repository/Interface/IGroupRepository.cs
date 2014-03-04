using System;
using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.Domain.Enums;
using MobiSage.AdsAPI.Domain.Results;
using MobiSage.AdsAPI.Domain.Users.UC;

namespace MobiSage.AdsAPI.Domain.Repository.Interface
{
    public interface IGroupRepository
    {
        /// <summary>
        ///     创建一个新的AdGroup
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adGroup">需要被创建的AdGroup</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        IExecuteResult Create(uint userId, ulong accountId, AdGroup adGroup);
        /// <summary>
        ///     更新一个AdGroup
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adGroup">需要更新的AdGroup</param>
        /// <param name="user">UC用户信息</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        IExecuteResult Update(uint userId, ulong accountId, AdGroup adGroup, UCUser user);
        /// <summary>
        ///     根据指定编号获取一个AdGroup实体
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adgroupId">广告组编号</param>
        /// <returns>返回执行后的结果</returns>
        IExecuteResult GetAdGroupById(uint userId, ulong accountId, ulong adgroupId);
        /// <summary>
        ///     获取指定广告活动下的所有AdGroup信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="campaignId">活动编号</param>
        /// <returns>返回执行后的结果</returns>
        IExecuteResult GetAdGroups(uint userId, ulong accountId, ulong campaignId);
        /// <summary>
        ///     根据IDs更改物料状态
        /// </summary>
        /// <param name="accountId">账户编码</param>
        /// <param name="ids">主键IDs</param>
        /// <param name="status">状态</param>
        /// <returns>返回执行后的结果</returns>
        IExecuteResult UpdateStatusByIds(ulong accountId,ulong[] ids, AdStatus status);
        /// <summary>
        ///     根据指定编号获取一个AdGroupSimple实体
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="adGroupId">广告组编号</param>
        /// <returns>返回执行后的结果</returns>
        IExecuteResult<ProtocolMetadata.AdGroupSimpleData> GetAdGroupSimpleById(uint userId, ulong adGroupId);
    }
}
