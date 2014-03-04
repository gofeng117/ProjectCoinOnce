using MobiSage.AdsAPI.Domain.Business.Dmp;
using MobiSage.AdsAPI.Domain.Results;

namespace MobiSage.AdsAPI.Domain.Repository
{
    /// <summary>
    ///     DMP仓储接口
    /// </summary>
    public interface IDMPRepository
    {
        /// <summary>
        ///     获取指定活动所绑定的标签信息集合
        /// </summary>
        /// <param name="campaignId">活动编号</param>
        /// <returns>执行结果</returns>
        IExecuteResult<SimpleUserTag[]> GetUserTagsByCampaignId(ulong campaignId);
        /// <summary>
        ///     获取指定广告组所绑定的标签信息集合
        /// </summary>
        /// <param name="adgroupId">广告组编号</param>
        /// <returns>执行结果</returns>
        IExecuteResult<SimpleUserTag[]> GetUserTagsByAdGroupId(ulong adgroupId);
        /// <summary>
        ///     检查指定广告组编号集合中是否存在不具有启动资格的广告组信息
        /// </summary>
        /// <param name="adgroupIds">广告组编号集合</param>
        /// <returns>返回执行结果</returns>
        IExecuteResult<SimpleAdGroup[]> CheckRunStatusByAdGroupIds(ulong[] adgroupIds);
        /// <summary>
        ///     检查指定活动下是否存在不具有启动资格的广告组信息
        /// </summary>
        /// <param name="campaignId">活动编号</param>
        /// <returns>返回执行结果</returns>
        IExecuteResult<SimpleAdGroup[]> CheckRunStatusByCampaignId(ulong campaignId);
        /// <summary>
        ///     获取指定活动下的所有标签编号
        /// </summary>
        /// <param name="campaignId">活动编号</param>
        /// <returns>返回执行结果</returns>
        IExecuteResult<ushort[]> GetTagIdByCampaignId(ulong campaignId);
    }
}