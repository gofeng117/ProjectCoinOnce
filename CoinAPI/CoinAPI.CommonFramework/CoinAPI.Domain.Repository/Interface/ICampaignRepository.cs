using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.Domain.Enums;
using MobiSage.AdsAPI.Domain.ProtocolMetadata;
using MobiSage.AdsAPI.Domain.Results;

namespace MobiSage.AdsAPI.Domain.Repository.Interface
{
    public interface ICampaignRepository
    {
        /// <summary>
        /// 创建Campaign
        /// </summary>
        /// <param name="userId">当前用户Id</param>
        /// <param name="campaign">要创建的Campaign对象</param>
        /// <returns></returns>
        IExecuteResult Create(uint userId, Campaign campaign);
        /// <summary>
        /// 更新Campaign
        /// </summary>
        /// <param name="userId">当前用户Id</param>
        /// <param name="campaign">要更新的Campaign对象</param>
        /// <returns></returns>
        IExecuteResult Update(uint userId, Campaign campaign);
        /// <summary>
        /// 通过CampaignId获取Campaign
        /// </summary>
        /// <param name="userId">当前用户Id</param>
        /// <param name="accountId">当前AccountId</param>
        /// <param name="campaignId">要获取的CampaignId</param>
        /// <returns></returns>
        IExecuteResult GetCampainById(uint userId, ulong accountId, ulong campaignId);
        /// <summary>
        /// 获取一个Account下的所有Campaign
        /// </summary>
        /// <param name="userId">当前用户Id</param>
        /// <param name="accountId">当前AccountId</param>
        /// <returns></returns>
        IExecuteResult GetCampaigns(uint userId, ulong accountId);

        /// <summary>
        /// 更新Campaign状态
        /// </summary>
        /// <param name="userId">当前用户Id</param>
        /// <param name="accountId">当前AccountId</param>
        /// <param name="campaignId">当前CampaignId</param>
        /// <param name="status">更改后的状态</param>
        /// <returns></returns>
        IExecuteResult UpdateCampaignStatus(uint userId, ulong accountId, ulong[] campaignId, AdStatus status);

        /// <summary>
        ///     根据指定编号获取一个简单Campagin实体
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="campaignId">广告活动编号</param>
        /// <returns>返回执行后的结果</returns>
        IExecuteResult<CampaignSimpleData> GetCampaignSimpleById(uint userId, ulong campaignId);

    }
}
