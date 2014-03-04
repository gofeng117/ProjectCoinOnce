using System.Text;
using KJFramework.Tracing;
using MobiSage.AdsAPI.CommonFramework.DbAccess;
using MobiSage.AdsAPI.CommonFramework.Enums;
using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.Domain.ProtocolMetadata;
using MobiSage.AdsAPI.Domain.Repository.Interface;
using MobiSage.AdsAPI.Domain.Repository.Objects;
using MobiSage.AdsAPI.Domain.Results;
using MySql.Data.Types;
using System;
using System.Data;
using MobiSage.AdsAPI.Domain.Enums;

namespace MobiSage.AdsAPI.Domain.Repository
{
    /// <summary>
    ///     Campaign仓储
    /// </summary>
#if TDD
    public sealed class CampaignRepository : MarshalByRefObject, ICampaignRepository
#else
    public sealed class CampaignRepository : ICampaignRepository
#endif
    {
        #region Members

        private static Database _masterDB;
        private static Database _slaveDB;
        private static readonly ITracing _tracing = TracingManager.GetTracing(typeof (CampaignRepository));
        private static IORMConvertor<Campaign> _convertor;
        private static IORMConvertor<CampaignSimpleData> _convertorSimple; 

        #endregion

        #region Methods

        /// <summary>
        /// 创建Campaign
        /// </summary>
        /// <param name="userId">当前用户Id</param>
        /// <param name="campaign">要创建的Campaign对象</param>
        /// <returns>
        ///     <para>1. 如果userid=0, 则返回执行失败</para>
        ///     <para>2. 如果执行成功，则返回CampaignId</para>
        ///     <para>3. 如果插入异常，则返回未知异常</para>
        /// </returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult Create(uint userId, Campaign campaign)
        {
            if (campaign == null) throw new ArgumentNullException("campaign");
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id!");
            try
            {
                //验证CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountId, new[] { "AccountId", "Creator" }, new object[] { campaign.AccountId, userId });
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountId + ":Executed count = 0");

                //创建一条Campaign的信息
                dt = _masterDB.SpExecuteTable(SpName.SpAddCampaign
                                                    , ParametersObject.CreateNewCampaign
                                                    , new object[]
                                                          {
                                                               campaign.AccountId
                                                              , campaign.MediaTypeId
                                                              , campaign.Name
                                                              , campaign.DailyBudget
                                                              , campaign.BidType
                                                              , campaign.StartDate
                                                              , campaign.EndDate
                                                              , campaign.DeliveryRemainingAd
                                                              , campaign.Status
                                                              , userId
                                                              , campaign.Targeting
                                                              , campaign.WaterMark
                                                              , campaign.MoreDayBudget
                                                              , campaign.IsMoreDaysBudget
                                                              , campaign.IsAutoTransform 
                                                              , campaign.IsConfine
                                                              , campaign.IsAutoActivation
                                                              , campaign.ActivationType
                                                              , campaign.AdUrl
                                                              , campaign.UdidType
                                                              , campaign.AppleStore
                                                              , campaign.AdSource
                                                          });
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpAddCampaign + ":Executed count = 0");
                return ExecuteResult.Succeed((ulong)(long)dt.Rows[0]["CampaignID"]);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     根据指定的编号更新一个Campaign
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="campaign">要更新的Campaign</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult Update(uint userId, Campaign campaign)
        {
            if (campaign == null) throw new ArgumentNullException("campaign");
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id!");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { campaign.CampaignId, campaign.AccountId, userId };
                //验证CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetStartDateByCampaignId, ParametersObject.GetCampaignByCampaignId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpGetStartDateByCampaignId + ":Executed count = 0");
                DateTime startDate = ((MySqlDateTime)dt.Rows[0]["StartDate"]).GetDateTime(); 
                //EndDate值非空时，不能小于或等于StartDate
                if (campaign.EndDate != null && startDate >= campaign.EndDate)
                    return ExecuteResult.Fail(SystemErrors.Malformed, "Illegal business roles: StartData >= EndData.");
                //修改一条Campaign的信息
                _masterDB.SpExecuteTable(SpName.SpUpdateCampaign
                                     , ParametersObject.UpdateCampaignById
                                     , new[]
                                           {
                                               campaign.CampaignId
                                               , dt.Rows[0]["AccountID"]
                                               , dt.Rows[0]["MediaTypeID"]
                                               , campaign.Name
                                               , campaign.DailyBudget
                                               , dt.Rows[0]["BidType"]
                                               , dt.Rows[0]["StartDate"]
                                               , campaign.EndDate
                                               , campaign.DeliveryRemainingAd
                                               , userId
                                               , campaign.Targeting //dt.Rows[0]["GeoIDs"]   "512, 513" *WARNNING*
                                               , campaign.WaterMark //dt.Rows[0]["WaterMark"]
                                               , campaign.Status
                                               , campaign.MoreDayBudget
                                               , campaign.IsMoreDaysBudget
                                               , campaign.IsAutoTransform
                                               , campaign.IsConfine
                                               , campaign.IsAutoActivation
                                               , campaign.ActivationType
                                               , campaign.AdUrl
                                               , campaign.UdidType
                                               , campaign.AppleStore
                                           });
                return ExecuteResult.Succeed(true);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     根据指定的编号获取一个Campaign
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="campaignId">Campaign编号</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult GetCampainById(uint userId, ulong accountId, ulong campaignId)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id!");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id!");
            if (campaignId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal campaign id!");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { campaignId, accountId, userId };
                //验证CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetCampaignByCampaignId, ParametersObject.GetCampaignByCampaignId, parameterValues);
                if (dt.Rows.Count <= 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpGetCampaignByCampaignId + ":Executed count = 0");
                Campaign campaign = _convertor.ConvertToDomain(dt.Rows[0]);//ConverterFactory.GetCampaignConverter().PackToDomainObject(dt.Rows[0]);
                return ExecuteResult.Succeed(campaign);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     获取指定的用户编号和账户编号下面的所有Campaign对象集合
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult GetCampaigns(uint userId, ulong accountId)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id!");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id!");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, userId };
                //验证CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetCampaigns, ParametersObject.GetCampaigns, parameterValues);
                if (dt.Rows.Count <= 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpGetCampaigns + ":Executed count = 0");
                Campaign[] campaigns = new Campaign[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                    campaigns[i] = _convertor.ConvertToDomain(dt.Rows[i]);
                return ExecuteResult.Succeed(campaigns);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     根据指定的编号更新一个或者批量更新Campaign的status
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账号编号</param>
        /// <param name="campaignIdData">Campaign编号</param>
        /// <param name="status">Campaign状态</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult UpdateCampaignStatus(uint userId, ulong accountId, ulong[] campaignIdData, AdStatus status)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id!");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id!");
            if (campaignIdData == null) throw new ArgumentNullException("campaignIdData");
            try
            {
                if (campaignIdData.Length == 1)
                {
                    StringBuilder campaignId = new StringBuilder();
                    campaignId = campaignId.Append(campaignIdData[0]);
                    //获取参数的值
                    object[] parameterValues = new object[] { accountId, campaignId, userId };
                    //验证CampaignId和AccountId从属关系
                    DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountIdAndCampaignId,
                                                           ParametersObject.IsNotExistByAccountIdAndCampaignId,
                                                           parameterValues);
                    if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCampaignId + ":Executed count = 0");
                    //修改一条Campaign status的信息
                    _masterDB.SpExecuteTable(SpName.SpUpdateCampaignStatusById,
                                             new[] {"CampaignID", "Status", "ModifyBy"},
                                             new object[] {campaignId, status, userId});
                    return ExecuteResult.Succeed(true);
                }
                else
                {
                    StringBuilder campaignIds = new StringBuilder();
                    for (int i = 0; i < campaignIdData.Length; i++)
                    {
                        campaignIds.Append(campaignIdData[i]);
                        if (i != campaignIdData.Length - 1) campaignIds.Append(',');
                    }
                    //获取参数的值
                    object[] parameterValues = new object[] { accountId, campaignIds, userId };
                    //验证CampaignId和AccountId从属关系
                    uint selectCount = _slaveDB.SpExecuteScalar<uint>(SpName.SpIsNotExistByAccountIdAndCampaignIds,
                                                           ParametersObject.IsNotExistByAccountIdAndCampaignIds,
                                                           parameterValues);
                    if (selectCount != campaignIdData.Length) return ExecuteResult.Fail(SystemErrors.NotFound, "Executed count != campaignId.Length");
                    //修改一条Campaign status的信息
                    _masterDB.SpExecuteTable(SpName.SpUpdateCampaignsStatus,
                                                                new[] { "CampaignIDs", "Status", "ModifyBy" },
                                                                new object[] { campaignIds, status, userId });
                    return ExecuteResult.Succeed(true);
                }
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     根据指定编号获取一个简单Campagin实体
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="campaignId">广告活动编号</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult<CampaignSimpleData> GetCampaignSimpleById(uint userId, ulong campaignId)
        {
            if (userId == 0) return ExecuteResult<CampaignSimpleData>.Fail(SystemErrors.Malformed, "#Illegal user id");
            if (campaignId == 0) return ExecuteResult<CampaignSimpleData>.Fail(SystemErrors.Malformed, "#Illegal campaignId id");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { campaignId, userId };
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetCampaignSimpleByCampaignId, new[] { "CampaignID", "Creator" }, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult<CampaignSimpleData>.Fail(SystemErrors.NotFound, SpName.SpGetCampaignSimpleByCampaignId + ":Executed count = 0");
                CampaignSimpleData campaign = _convertorSimple.ConvertToDomain(dt.Rows[0]);
                return ExecuteResult<CampaignSimpleData>.Succeed(campaign);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult<CampaignSimpleData>.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     静态注入db实例
        /// </summary>
        /// <param name="masterDB">主DB</param>
        /// <param name="slaveDB">从DB</param>
        /// <param name="convertor">对象转换器</param>
        /// <param name="convertorSimple">简单对象转换器</param>
        public static void Inject(Database masterDB, Database slaveDB, IORMConvertor<Campaign> convertor, IORMConvertor<CampaignSimpleData> convertorSimple)
        {
            if (null == masterDB) throw new ArgumentNullException("masterDB");
            if (null == convertor) throw new ArgumentNullException("convertor");
            _masterDB = masterDB;
            _slaveDB = slaveDB;
            _convertor = convertor;
            _convertorSimple = convertorSimple;
        }

        #endregion
    }
}