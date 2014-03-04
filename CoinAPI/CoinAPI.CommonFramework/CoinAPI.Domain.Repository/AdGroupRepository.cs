using System;
using System.Data;
using System.Linq;
using System.Text;
using KJFramework.Tracing;
using MobiSage.AdsAPI.CommonFramework.DbAccess;
using MobiSage.AdsAPI.CommonFramework.Enums;
using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.Domain.Enums;
using MobiSage.AdsAPI.Domain.Midiator;
using MobiSage.AdsAPI.Domain.Midiator.Extentions;
using MobiSage.AdsAPI.Domain.ProtocolMetadata;
using MobiSage.AdsAPI.Domain.Repository.Interface;
using MobiSage.AdsAPI.Domain.Repository.Objects;
using MobiSage.AdsAPI.Domain.Results;
using MobiSage.AdsAPI.Domain.Users.UC;
using MobiSage.AdsAPI.UAS.Common;

namespace MobiSage.AdsAPI.Domain.Repository
{
    /// <summary>
    ///     AdGroup仓储
    /// </summary>
#if TDD
    public sealed class AdGroupRepository : MarshalByRefObject, IGroupRepository
#else
    public sealed class AdGroupRepository
#endif
    {
        #region Members

        private static Database _masterDB;
        private static Database _slaveDB;
        private static IORMConvertor<AdGroup> _convertor;
        private static IORMConvertor<AdGroupSimpleData> _convertorSimplle;
        private static readonly ITracing _tracing = TracingManager.GetTracing(typeof(AdGroupRepository));
        //业务数据默认值
        private const int mediaTypeId = 1;


        #endregion

        #region Methods

        /// <summary>
        ///     创建一个新的AdGroup
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adGroup">需要被创建的AdGroup</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult Create(uint userId, ulong accountId, AdGroup adGroup)
        {
            if (adGroup == null) throw new ArgumentNullException("adGroup");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, adGroup.CampaignId, userId };

                //验证CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountIdAndCampaignId, ParametersObject.IsNotExistByAccountIdAndCampaignId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCampaignId + ":Executed count = 0");
                //check publish id.
                if (!string.IsNullOrEmpty(adGroup.PublishId))
                {
                    string[] subIds = adGroup.PublishId.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string subId in subIds)
                    {
                        dt = _slaveDB.SpExecuteTable(SpName.SpGetAppByPublishId, ParametersObject.GetAppByPublishId, new object[] { subId });
                        if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, string.Format("#There isn't any records indicated that this publish id: {0} is valid.", subId));
                    }
                }

                //获取参数的值
                parameterValues = new object[]
                       {
                           adGroup.CampaignId
                           , accountId
                           , adGroup.MediaTypeId
                           , adGroup.Name
                           , Math.Round(adGroup.BidPrice, 2)
                           , adGroup.PricePolicy.DevPrice
                           , adGroup.Status
                           , adGroup.DeliveryMode
                           , adGroup.Weight
                           , userId
                           , adGroup.Targetings.FrequencyTargeting.MaxClicksEveryDayByDevice
                           , adGroup.Targetings.FrequencyTargeting.MaxImpressionsEveryDayByDevice
                           , adGroup.Targetings.FrequencyTargeting.MaxClicksSevenDaysByDevice
                           , adGroup.Targetings.FrequencyTargeting.MaxImpressionsSevenDaysByDevice
                           , adGroup.Targetings.FrequencyTargeting.MaxClicksEveryDay
                           , adGroup.Targetings.FrequencyTargeting.MaxImpressionsEveryDay
                           , adGroup.Targetings.FrequencyTargeting.MaxClicksPutIn
                           , adGroup.Targetings.FrequencyTargeting.MaxImpressionsPutIn
                           , adGroup.ITunesAppId
                           , adGroup.ActionType
                           , adGroup.ContentType.AdContentId
                           , adGroup.Targetings.TimeTargeting
                           , adGroup.ThirdpartyPlatformId
                           , adGroup.DeviceCategory
                           , adGroup.NetworkTypes
                           , adGroup.Targetings.DeviceTargeting.GetCatagoryString()
                           , adGroup.Targetings.DeviceTargeting.GetSDKString()
                           , adGroup.Targetings.DeviceTargeting.GetOSString()
                           , adGroup.Targetings.DeviceTargeting.GetOpString()
                           , adGroup.Targetings.DeviceTargeting.GetAppString()
                           , adGroup.ITunesAppCategoryId
                           , adGroup.CPAPrice
                           , adGroup.MaxImpressions
                           , adGroup.MaxClicks
                           , adGroup.MaxPerUserImpressions
                           , adGroup.PublishId
                           , adGroup.Targetings.TagTargeting.GetData(UserTagDirectionTypes.Forward)==null ? null : adGroup.Targetings.TagTargeting.GetData(UserTagDirectionTypes.Forward).Concat()
                           , adGroup.Targetings.TagTargeting.GetData(UserTagDirectionTypes.Backward)==null ? null : adGroup.Targetings.TagTargeting.GetData(UserTagDirectionTypes.Backward).Concat()
                           , adGroup.ITunesBundleId
                           , adGroup.DisplayType
                       };
                //创建一条Campaign的信息
                dt = _masterDB.SpExecuteTable(SpName.SpAddAdGroup
                                                   , ParametersObject.CreateNewAdGroup
                                                   , parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpAddAdGroup + ":Executed count = 0");
                return ExecuteResult.Succeed((ulong)(long)dt.Rows[0]["GroupID"]);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     更新一个AdGroup
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adGroup">需要更新的AdGroup</param>
        /// <param name="user">UC用户信息</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult Update(uint userId, ulong accountId, AdGroup adGroup, UCUser user)
        {
            if (adGroup == null) throw new ArgumentNullException("adGroup");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { adGroup.CampaignId, accountId, adGroup.AdGroupId, userId };
                //验证CampaignId、AccountId、AdGroup从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountIdAndCampaignIdAndAdGroupId,
                                                    ParametersObject.IsNotExistByAccountIdAndCampaignIdAndAdGroupId,
                                                    new object[] { accountId, adGroup.CampaignId, adGroup.AdGroupId, userId });
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCampaignIdAndAdGroupId + ":Executed count = 0");
                //check publish id.
                if (!string.IsNullOrEmpty(adGroup.PublishId))
                {
                    string[] subIds = adGroup.PublishId.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string subId in subIds)
                    {
                        dt = _slaveDB.SpExecuteTable(SpName.SpGetAppByPublishId, ParametersObject.GetAppByPublishId, new object[] { subId });
                        if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, string.Format("#There isn't any records indicated that this publish id: {0} is valid.", subId));
                    }
                }
                dt = _slaveDB.SpExecuteTable(SpName.SpGetAdGroupByAdGroupId, ParametersObject.CheckAndGetAdGroup, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpGetAdGroupByAdGroupId + ":Executed count = 0");
                //check account type.
                DataTable accountTypeDt = _slaveDB.SpExecuteTable(SpName.SpGetAccountTypeFromAccountDetail, ParametersObject.GetAccountTypeFromAccountDetail, new object[] { accountId });
                if (accountTypeDt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, "#There isn't any accout accords current condition: AccountId: " + accountId);
                AccountTypes accountTypes = (AccountTypes)byte.Parse(accountTypeDt.Rows[0]["AccountTypeID"].ToString());

                #region Different Login Platform Logical.

                UASToken token;
                if (!UASToken.TryParse(user.Token, out token)) return ExecuteResult.Fail(SystemErrors.AuthoriztionFail, "#Sadly, We cannot parse currently user's login token correctly.");
                if (token.LoginType == LoginTypes.WebSiteLogin) adGroup.PricePolicy.DevPrice = decimal.Parse(dt.Rows[0]["AcBidPrice"].ToString());

                #endregion


                if (decimal.Parse(dt.Rows[0]["AcBidPrice"].ToString()) != adGroup.PricePolicy.DevPrice)
                {
                    if (!user.IsAdministrator || accountTypes == AccountTypes.MediaAccount)
                        return ExecuteResult.Fail(SystemErrors.BusinessObjIsNotSupported, "#Cannot update AcBidPrice field value when you were under the Administrator permission!!!");
                }


                #region Details Validation.

                uint dbAdContentId = (uint)(int)dt.Rows[0]["AdContentID"];
                string dbITunesApId = Convert.IsDBNull(dt.Rows[0]["ITunesAppID"]) ? null : dt.Rows[0]["ITunesAppID"].ToString();
                if (adGroup.ActionType != 0)
                {
                    if (dbAdContentId < 2 || (dbAdContentId == 5 && adGroup.ITunesAppId != null)) return ExecuteResult.Fail(SystemErrors.BusinessObjIsNotSupported, "#Cannot pass details validation.");
                    if (adGroup.ContentType.AdContentId != 7 && adGroup.ContentType.AdContentId != 8)
                    {
                        //AdContentID = 2,DeviceCategory的范围在2,3,4,5,6,7之内
                        //AdContentID = 3,DeviceCategory的范围在2,4,5,7之内
                        //AdContentID = 4,DeviceCategory的范围在3,6之内
                        //AdContentID = 5,DeviceCategory只能是8
                        string[] reqMsgDeviceCategory = adGroup.DeviceCategory.Split(',');
                        if (reqMsgDeviceCategory.Any(t => MidiatorGlobal.DeviceCategory[dbAdContentId.ToString()].Contains(t) == false))
                            return ExecuteResult.Fail(SystemErrors.BusinessObjIsNotSupported, "#Cannot pass details validation.");
                        //AdContentID等于5的时候，ITunesAppID不能传值
                        if (dbAdContentId == 5 && !string.IsNullOrEmpty(adGroup.ITunesAppId.ToString()))
                            return ExecuteResult.Fail(SystemErrors.BusinessObjIsNotSupported, "#Cannot pass details validation.");
                    }
                }
                #endregion

                //修改一条AdGroup的信息
                _masterDB.SpExecuteTable(SpName.SpUpdateAdGroup, ParametersObject.UpdateAdGroup, new object[]
                       {
                           dt.Rows[0]["GroupID"]
                          ,dt.Rows[0]["CampaignID"]
                          ,dt.Rows[0]["AccountID"]
                          ,dt.Rows[0]["MediaTypeID"] //cannot update
                          ,adGroup.Name
                          ,Math.Round(adGroup.BidPrice, 2)
                          ,adGroup.PricePolicy.DevPrice //dt.Rows[0]["AcBidPrice"]
                          ,adGroup.Status
                          ,dt.Rows[0]["DeliveryMode"] //adGroup.DeliveryMode
                          ,dt.Rows[0]["Weight"] //adGroup.Weight
                          ,dt.Rows[0]["AdActionID"] //cannot update
                          ,dt.Rows[0]["AdContentID"] //cannot update
                          ,adGroup.Targetings.TimeTargeting //dt.Rows[0]["WeekTimePeriodIDs"] 
                          ,Convert.IsDBNull(dt.Rows[0]["ThirdPartPlatformID"]) ? 0 : dt.Rows[0]["ThirdPartPlatformID"] //adGroup.ThirdpartyPlatformId
                          ,adGroup.Targetings.FrequencyTargeting.MaxClicksEveryDayByDevice
                          ,adGroup.Targetings.FrequencyTargeting.MaxImpressionsEveryDayByDevice
                          ,adGroup.Targetings.FrequencyTargeting.MaxClicksSevenDaysByDevice
                          ,adGroup.Targetings.FrequencyTargeting.MaxImpressionsSevenDaysByDevice
                          ,adGroup.Targetings.FrequencyTargeting.MaxClicksEveryDay
                          ,adGroup.Targetings.FrequencyTargeting.MaxImpressionsEveryDay
                          ,adGroup.Targetings.FrequencyTargeting.MaxClicksPutIn
                          ,adGroup.Targetings.FrequencyTargeting.MaxImpressionsPutIn
                          ,adGroup.ITunesAppId
                          ,userId
                          ,adGroup.DeviceCategory
                          ,adGroup.NetworkTypes
                          ,adGroup.Targetings.DeviceTargeting.GetCatagoryString()
                          ,adGroup.Targetings.DeviceTargeting.GetSDKString()
                          ,adGroup.Targetings.DeviceTargeting.GetOSString()
                          ,adGroup.Targetings.DeviceTargeting.GetOpString()
                          ,adGroup.Targetings.DeviceTargeting.GetAppString()
                          ,adGroup.ITunesAppCategoryId
                          ,adGroup.CPAPrice
                          ,adGroup.MaxImpressions
                          ,adGroup.MaxClicks
                          ,adGroup.MaxPerUserImpressions
                          ,adGroup.PublishId
                          ,adGroup.Targetings.TagTargeting.GetData(UserTagDirectionTypes.Forward)==null ? null : adGroup.Targetings.TagTargeting.GetData(UserTagDirectionTypes.Forward).Concat()
                          ,adGroup.Targetings.TagTargeting.GetData(UserTagDirectionTypes.Backward)==null ? null : adGroup.Targetings.TagTargeting.GetData(UserTagDirectionTypes.Backward).Concat()
                          ,adGroup.ITunesBundleId
                          ,dt.Rows[0]["DisplayType"] //cannot update
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
        ///     根据指定编号获取一个AdGroup实体
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adgroupId">广告组编号</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult GetAdGroupById(uint userId, ulong accountId, ulong adgroupId)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id");
            if (adgroupId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal adgroup id");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, adgroupId, userId };
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetAdGroupByAdGroupId, ParametersObject.GetAdGroupById, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpGetAdGroupByAdGroupId + ":Executed count = 0");
                AdGroup adGroup = _convertor.ConvertToDomain(dt.Rows[0]);
                return ExecuteResult.Succeed(adGroup);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }


        /// <summary>
        ///     根据指定编号获取一个AdGroup实体
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="adgroupId">广告组编号</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult<AdGroupSimpleData> GetAdGroupSimpleById(uint userId, ulong adgroupId)
        {
            if (userId == 0) return ExecuteResult<AdGroupSimpleData>.Fail(SystemErrors.Malformed, "#Illegal user id");
            if (adgroupId == 0) return ExecuteResult<AdGroupSimpleData>.Fail(SystemErrors.Malformed, "#Illegal adgroup id");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { adgroupId, userId };
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetAdGroupSimpleByAdGroupId, ParametersObject.GetAdGroupSimpleById, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult<AdGroupSimpleData>.Fail(SystemErrors.NotFound, SpName.SpGetAdGroupSimpleByAdGroupId + ":Executed count = 0");
                AdGroupSimpleData adGroup = _convertorSimplle.ConvertToDomain(dt.Rows[0]);
                return ExecuteResult<AdGroupSimpleData>.Succeed(adGroup);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult<AdGroupSimpleData>.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     获取指定广告活动下的所有AdGroup信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="campaignId">活动编号</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult GetAdGroups(uint userId, ulong accountId, ulong campaignId)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id");
            if (campaignId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal campaign id");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { campaignId, accountId, userId };
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetAdGroups, ParametersObject.GetAdGroups, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpGetAdGroups + ":Executed count = 0");
                AdGroup[] adGroups = new AdGroup[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                    adGroups[i] = _convertor.ConvertToDomain(dt.Rows[i]);
                return ExecuteResult.Succeed(adGroups);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     根据ID更改物料状态
        /// </summary>
        /// <param name="accountId">账户id</param>
        /// <param name="groupIds">主键IDs</param>
        /// <param name="status">状态</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult UpdateStatusByIds(ulong accountId, ulong[] groupIds, AdStatus status)
        {
            if (groupIds == null || groupIds.Length == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal groupId");
            try
            {
                object[] parameterValues;

                StringBuilder sGroup = new StringBuilder();
                foreach (ulong groupId in groupIds) sGroup.Append(groupId).Append(',');
                string groupString = sGroup.ToString().TrimEnd(',');
                //获取参数的值
                parameterValues = new object[] { accountId, groupString };
                //验证CampaignId和AccountId从属关系
                uint selectCount = _slaveDB.SpExecuteScalar<uint>(SpName.SpIsNotExistByAccountIdAndGroupId,
                                                       ParametersObject.IsNotExistByAccountIdAndGroupId,
                                                       parameterValues);
                if (selectCount != groupIds.Length) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndGroupId + ":Executed count = 0 #Paramate [AccountId] is " + accountId + " [GroupId] is " + groupString);

                //获取参数的值
                parameterValues = new object[] { groupString, status, 0 };
                _masterDB.SpExecuteTable(SpName.SpUpdateGroupsStatus, ParametersObject.UpdateGroupsStatus, parameterValues);
                return ExecuteResult.Succeed(true);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }


        /// <summary>
        ///     静态注入db实例
        /// </summary>
        /// <param name="masterDB">主DB</param>
        /// <param name="slaveDB">从DB</param>
        /// <param name="convertor">对象转换器</param>
        /// <param name="convertorSimplle">简单对象转换器</param>
        public static void Inject(Database masterDB, Database slaveDB, IORMConvertor<AdGroup> convertor, IORMConvertor<AdGroupSimpleData> convertorSimplle)
        {
            if (null == masterDB) throw new ArgumentNullException("masterDB");
            if (null == convertor) throw new ArgumentNullException("convertor");
            _masterDB = masterDB;
            _slaveDB = slaveDB;
            _convertor = convertor;
            _convertorSimplle = convertorSimplle;
        }

        #endregion
    }
}