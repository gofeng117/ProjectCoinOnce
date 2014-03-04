using System.Text;
using KJFramework.Tracing;
using MobiSage.AdsAPI.CommonFramework.DbAccess;
using MobiSage.AdsAPI.CommonFramework.Enums;
using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.Domain.Enums;
using MobiSage.AdsAPI.Domain.Midiator.Converters;
using MobiSage.AdsAPI.Domain.ProtocolMetadata;
using MobiSage.AdsAPI.Domain.ProtocolMetadata.Themes.Templates;
using MobiSage.AdsAPI.Domain.Repository.Objects;
using MobiSage.AdsAPI.Domain.Results;
using System;
using System.Collections.Generic;
using System.Data;

namespace MobiSage.AdsAPI.Domain.Repository
{
    /// <summary>
    ///     AdCreative仓储
    /// </summary>
#if TDD
    public sealed class AdCreativeRepository : MarshalByRefObject, IAdTextCreativeRepository
#else
    public sealed class AdCreativeRepository : IAdTextCreativeRepository
#endif
    {
        #region Members

        private static Database _slaveDB;
        private static Database _masterDB;
        private static AdCreativeORMConvertor _convertor;
        private static readonly ITracing _tracing = TracingManager.GetTracing(typeof(AdCreativeRepository));
        //业务数据默认值
        private const int imageWidth = 72;
        private const int imageHeight = 72;
        private const int dataType = 1;

        #endregion

        #region Methods

        /// <summary>
        ///     创建一个新的文字创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">文字创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult CreateTextCreative(uint userId, ulong accountId, TextAdCreative adCreative)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            if (adCreative == null) throw new ArgumentNullException("adCreative");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, adCreative.CampaignId, adCreative.GroupId, userId };
                //验证AdGroupId,CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountIdAndCampaignIdAndAdGroupId, ParametersObject.IsNotExistByAccountIdAndCampaignIdAndAdGroupId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCampaignIdAndAdGroupId + ":Executed count = 0");
                //创建一条Campaign的信息
                dt = _masterDB.SpExecuteTable(SpName.SpAddAdCreativeText
                                          , ParametersObject.CreateNewAdCreative
                                          , new object[]
                                                {
                                                      adCreative.GroupId
                                                    , adCreative.CampaignId
                                                    , accountId
                                                    , adCreative.MediaTypeId
                                                    , adCreative.Name ?? string.Empty
                                                    , adCreative.CreativeType
                                                    , adCreative.AdCreativeDeliveryType //themeDeliveryType
                                                    , adCreative.Status
                                                    , adCreative.Weight
                                                    , userId
                                                    , adCreative.Title ?? string.Empty
                                                    , adCreative.DestinationUrl ?? string.Empty
                                                    , adCreative.DisplayUrl ?? string.Empty
                                                    , adCreative.Desc1 ?? string.Empty
                                                    , adCreative.Desc2 ?? string.Empty
                                                    , adCreative.LogoImageUrl ?? string.Empty
                                                    , dataType
                                                    , adCreative.Size.ImageSizeId
                                                    , adCreative.CustomElement.ToString()
                                                    , adCreative.IsSuspension
                                                    , adCreative.TemplateId
                                                    , adCreative.PackageName
                                                    , adCreative.IsAutoOpen ? 1 : 0
                                                    , adCreative.ThirdPartyTrackingUrl
                                                });
                return dt.Rows.Count == 0 ? ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpAddAdCreativeText + ":Executed count = 0") : ExecuteResult.Succeed((ulong)(long)dt.Rows[0]["CreativeID"]);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     创建一个新的文字链创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">文字链创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult CreateTextLinkCreative(uint userId, ulong accountId, TextLinkAdCreative adCreative)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            if (adCreative == null) throw new ArgumentNullException("adCreative");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, adCreative.CampaignId, adCreative.GroupId, userId };
                //验证AdGroupId,CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountIdAndCampaignIdAndAdGroupId, ParametersObject.IsNotExistByAccountIdAndCampaignIdAndAdGroupId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCampaignIdAndAdGroupId + ":Executed count = 0");
                //创建一条Campaign的信息
                dt = _masterDB.SpExecuteTable(SpName.SpAddCreativeTextLink
                                          , ParametersObject.CreateNewAdTextLink
                                          , new object[]
                                                {       
                                                      adCreative.GroupId
                                                    , adCreative.CampaignId
                                                    , accountId
                                                    , adCreative.MediaTypeId
                                                    , adCreative.Name
                                                    , adCreative.CreativeType
                                                    , adCreative.AdCreativeDeliveryType //themeDeliveryType
                                                    , adCreative.Status
                                                    , adCreative.Weight
                                                    , userId
                                                    , adCreative.DestinationUrl
                                                    , dataType
                                                    , adCreative.PackageName
                                                    , adCreative.IsAutoOpen ? 1 : 0
                                                    , adCreative.ThirdPartyTrackingUrl
                                                    , adCreative.Title
                                                });
                return dt.Rows.Count == 0 ? ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpAddCreativeTextLink + ":Executed count = 0") : ExecuteResult.Succeed((ulong)(long)dt.Rows[0]["CreativeID"]);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }


        /// <summary>
        ///     创建一个新的Html创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">Html创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult CreateHtmlCreative(uint userId, ulong accountId, HtmlAdCreative adCreative)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            if (adCreative == null) throw new ArgumentNullException("adCreative");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, adCreative.CampaignId, adCreative.GroupId, userId };
                //验证AdGroupId,CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountIdAndCampaignIdAndAdGroupId, ParametersObject.IsNotExistByAccountIdAndCampaignIdAndAdGroupId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCampaignIdAndAdGroupId + ":Executed count = 0");
                //创建一条Campaign的信息
                dt = _masterDB.SpExecuteTable(SpName.SpAddCreativeHtml
                                          , ParametersObject.CreateNewAdHtml
                                          , new object[]
                                                {       
                                                      adCreative.GroupId
                                                    , adCreative.CampaignId
                                                    , accountId
                                                    , adCreative.MediaTypeId
                                                    , adCreative.Name
                                                    , adCreative.CreativeType
                                                    , adCreative.AdCreativeDeliveryType //themeDeliveryType
                                                    , adCreative.Status
                                                    , adCreative.Weight
                                                    , userId
                                                    , adCreative.DestinationUrl
                                                    , dataType
                                                    , adCreative.PackageName
                                                    , adCreative.IsAutoOpen ? 1 : 0
                                                    , adCreative.ThirdPartyTrackingUrl
                                                    , adCreative.Size.ImageSizeId
                                                    , adCreative.HtmlUrl
                                                });
                return dt.Rows.Count == 0 ? ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpAddCreativeHtml + ":Executed count = 0") : ExecuteResult.Succeed(ulong.Parse(dt.Rows[0]["CreativeID"].ToString()));
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     更新一个文字创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">文字创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult UpdateTextCreative(uint userId, ulong accountId, TextAdCreative adCreative)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            if (adCreative == null) throw new ArgumentNullException("adCreative");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, adCreative.CampaignId, adCreative.GroupId, adCreative.CreativeId, userId };
                //验证CampaignId、AccountId、CreativeID从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountIdAndCampaignIDAndAdGroupIDAndCreativeID, ParametersObject.IsNotExistByAccountIdAndCampaignIdAndAdGroupIdAndCreativeId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCampaignIDAndAdGroupIDAndCreativeID + ":Executed count = 0");
                //获取数据库中的数据
                dt = _slaveDB.SpExecuteTable(SpName.SpGetTextCreativeByCreativeId, new[] { "CreativeID", "Creator" }, new object[] { adCreative.CreativeId, userId });
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, "#Cannot read any ad-Text creative data from slave database.");
                //修改一条TextCreative的信息
                _masterDB.SpExecuteTable(SpName.SpUpdateTextCreative
                                     , ParametersObject.UpdateAdTextCreative
                                     , new object[]
                                           {
                                                 adCreative.CreativeId
                                               , adCreative.GroupId
                                               , adCreative.CampaignId
                                               , accountId
                                               , dt.Rows[0]["MediaTypeID"] //adCreative.MediaTypeId
                                               , adCreative.Name ?? string.Empty
                                               , dt.Rows[0]["AdCreativeDeliveryType"] //adCreative.AdCreativeDeliveryType
                                               , adCreative.Status
                                               , dt.Rows[0]["Weight"] //adCreative.Weight
                                               , userId
                                               , adCreative.Title ?? string.Empty
                                               , adCreative.DestinationUrl ?? string.Empty
                                               , dt.Rows[0]["DisplayURL"] //adCreative.DisplayUrl ?? string.Empty
                                               , dt.Rows[0]["Desc1"] //adCreative.Desc1 ?? string.Empty
                                               , dt.Rows[0]["Desc2"] //adCreative.Desc2 ?? string.Empty
                                               , adCreative.LogoImageUrl ?? string.Empty
                                               , dataType
                                               , dt.Rows[0]["Size"]
                                               , adCreative.CustomElement.ToString()
                                               , adCreative.IsSuspension
                                               , adCreative.PackageName
                                               , adCreative.IsAutoOpen ? 1 : 0
                                               , adCreative.ThirdPartyTrackingUrl
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
        ///     根据指定编号获取一个文字创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="adCreativeId">广告创意编号</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult GetAdTextCreativeById(uint userId, ulong adCreativeId)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (adCreativeId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal adCreative id.");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { adCreativeId, userId };
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetTextCreativeByCreativeId, new[] { "CreativeID", "Creator" }, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpGetTextCreativeByCreativeId + ":Executed count = 0");
                return ExecuteResult.Succeed(ConverterFactory.GetAdTextCreativeConverter().PackToDomainObject(dt.Rows[0]));
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     创建一个新的荐计划创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">荐计划创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult CreateNewRecommandCreative(uint userId, ulong accountId, RecommandAdCreative adCreative)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            if (adCreative == null) throw new ArgumentNullException("adCreative");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, adCreative.CampaignId, adCreative.GroupId, userId };
                //验证AdGroupId,CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountIdAndCampaignIdAndAdGroupId, ParametersObject.IsNotExistByAccountIdAndCampaignIdAndAdGroupId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCampaignIdAndAdGroupId + ":Executed count = 0");
                //ensure that currently creative's campaign bid-type.
                parameterValues = new object[] { adCreative.CampaignId, accountId, userId };
                dt = _slaveDB.SpExecuteTable(SpName.SpGetCampaignByCampaignId, ParametersObject.GetCampaignByCampaignId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, "#CANNOT query out any infomation about currently creative's campaign object");
                BidTypes campaignBidType = (BidTypes)byte.Parse(dt.Rows[0]["BidType"].ToString());
                if ((campaignBidType != BidTypes.CPM) && string.IsNullOrEmpty(adCreative.DestinationUrl))
                    return ExecuteResult.Fail(SystemErrors.NotFound, "#DestinationUrl MUST has value when currently creative's campaign bid-type != CPM");
                if (campaignBidType == BidTypes.CPM && adCreative.DestinationUrl == null) adCreative.DestinationUrl = string.Empty;
                if (adCreative.MediaTypeId != ushort.Parse(dt.Rows[0]["MediaTypeID"].ToString()))
                    return ExecuteResult.Fail(SystemErrors.NotFound, "#MediaTypeId MUST same with currently ad-creative's campaign.MediaTypeId!");
                if (!string.IsNullOrEmpty(adCreative.DestinationUrl))
                {
                    if (Encoding.Default.GetByteCount(adCreative.DestinationUrl) > 1024) return ExecuteResult.Fail(SystemErrors.NotFound, "#DestinationUrl max length cannot over 1024.");
                    //if (!ValidationRegex.RegexDestinationUrl.IsMatch(adCreative.DestinationUrl)) return ExecuteResult.Fail(SystemErrors.NotFound, "#DestinationUrl cannot pass the specific regex condition.");
                    //if (adCreative.DestinationUrl.Contains("tel") && !Regex.IsMatch(adCreative.DestinationUrl, @"^\d+$")) return ExecuteResult.Fail(SystemErrors.NotFound, "#DestinationUrl incorrectly type. If you chooise use 'tel' to the head, then you MUSTN'T type any numbers.");
                }
                dt = _slaveDB.SpExecuteTable(SpName.SpGetAdGroupSimpleInfo, ParametersObject.GetAdGroupSimpleInfo, adCreative.GroupId);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, "#Sadly, we couldn't got any infomation of currently ad-creative's group.Action.");
                int adcontentId = int.Parse(dt.Rows[0]["AdContent"].ToString());
                if ((AdActionTypes)byte.Parse(dt.Rows[0]["AdActionID"].ToString()) != AdActionTypes.DownloadDirectly || adcontentId < 2 || adcontentId > 4)
                    return ExecuteResult.Fail(SystemErrors.NotFound, "#Sadly, you CANNOT create a recommand-creative because of currently adgroup's action-id or ad-content id is not expects.");
                //创建一条Campaign的信息
                dt = _masterDB.SpExecuteTable(SpName.SpAddCreativeRecommend
                                          , ParametersObject.CreateNewRecommandCreative
                                          , new object[]
                                                {
                                                      adCreative.GroupId
                                                    , adCreative.CampaignId
                                                    , accountId
                                                    , adCreative.MediaTypeId
                                                    , adCreative.Name
                                                    , adCreative.CreativeType
                                                    , adCreative.AdCreativeDeliveryType //themeDeliveryType
                                                    , adCreative.Status
                                                    , adCreative.Weight
                                                    , userId
                                                    , adCreative.DestinationUrl
                                                    , adCreative.AppUrl
                                                    , dataType
                                                    , adCreative.PackageName
                                                    , adCreative.IsAutoOpen ? 1 : 0
                                                    , adCreative.ThirdPartyTrackingUrl
                                                    , adCreative.AppInfo.AppIcon
                                                    , adCreative.AppInfo.Name
                                                    , adCreative.AppInfo.Description
                                                    , adCreative.AppInfo.Price
                                                    , adCreative.AppInfo.FileSize
                                                    , adCreative.AppInfo.Category
                                                    , adCreative.AppInfo.DownloadCount == null ? string.Empty : adCreative.AppInfo.DownloadCount.ToString()
                                                    , adCreative.AppInfo.Version
                                                    , adCreative.AppInfo.Grade
                                                    , adCreative.AppInfo.GradeCount
                                                    , adCreative.AppInfo.Points
                                                    , adCreative.AppInfo.AppId == null ? string.Empty : adCreative.AppInfo.AppId.ToString()
                                                });
                return dt.Rows.Count == 0 ? ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpAddCreativeRecommand + ":Executed count = 0") : ExecuteResult.Succeed((ulong)(long)dt.Rows[0]["CreativeID"]);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     更新一个荐计划创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">荐计划创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult UpdateRecommandCreative(uint userId, ulong accountId, RecommandAdCreative adCreative)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            if (adCreative == null) throw new ArgumentNullException("adCreative");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, adCreative.CampaignId, adCreative.GroupId, adCreative.CreativeId, userId };
                //验证AdGroupId,CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountIdAndCampaignIDAndAdGroupIDAndCreativeID, ParametersObject.IsNotExistByAccountIdAndCampaignIdAndAdGroupIdAndCreativeId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCampaignIDAndAdGroupIDAndCreativeID + ":Executed count = 0");
                //ensure that currently creative's campaign bid-type.
                dt = _slaveDB.SpExecuteTable(SpName.SpGetCampaignByCampaignId, ParametersObject.GetCampaignByCampaignId, new object[] { adCreative.CampaignId, accountId, userId });
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, "#CANNOT query out any infomation about currently creative's campaign object");
                BidTypes campaignBidType = (BidTypes)byte.Parse(dt.Rows[0]["BidType"].ToString());
                if (campaignBidType != BidTypes.CPM && string.IsNullOrEmpty(adCreative.DestinationUrl))
                    return ExecuteResult.Fail(SystemErrors.NotFound, "#DestinationUrl MUST has value when currently creative's campaign bid-type != CPM");
                //为无点击广告作支持
                if (campaignBidType == BidTypes.CPM && adCreative.DestinationUrl == null) adCreative.DestinationUrl = string.Empty;
                if (adCreative.MediaTypeId != ushort.Parse(dt.Rows[0]["MediaTypeID"].ToString()))
                    return ExecuteResult.Fail(SystemErrors.NotFound, "#MediaTypeId MUST same with currently ad-creative's campaign.MediaTypeId!");
                if (!string.IsNullOrEmpty(adCreative.DestinationUrl))
                {
                    if (Encoding.Default.GetByteCount(adCreative.DestinationUrl) > 1024) return ExecuteResult.Fail(SystemErrors.NotFound, "#DestinationUrl max length cannot over 1024.");
                    //if (!ValidationRegex.RegexDestinationUrl.IsMatch(adCreative.DestinationUrl)) return ExecuteResult.Fail(SystemErrors.NotFound, "#DestinationUrl cannot pass the specific regex condition.");
                    //if (adCreative.DestinationUrl.Contains("tel") && !Regex.IsMatch(adCreative.DestinationUrl, @"^\d+$")) return ExecuteResult.Fail(SystemErrors.NotFound, "#DestinationUrl incorrectly type. If you chooise use 'tel' to the head, then you MUSTN'T type any numbers.");
                }
                //获取数据库中的数据
                dt = _slaveDB.SpExecuteTable(SpName.SpGetRecommandCreativeByCreativeId, ParametersObject.SpGetRecommandCreativeByCreativeId, new object[] { adCreative.CreativeId, userId });
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, "#Cannot read any ad-image creative data from slave database.");

                //修改一条TextCreative的信息
                _masterDB.SpExecuteTable(SpName.SpUpdateCreativeRecommend
                                     , ParametersObject.UpdateCreativeRecommand
                                     , new object[]
                                           {
                                                 adCreative.CreativeId
                                               , adCreative.GroupId
                                               , adCreative.CampaignId
                                               , accountId
                                               , dt.Rows[0]["MediaTypeID"] //adCreative.MediaTypeId
                                               , adCreative.Name
                                               , adCreative.CreativeType
                                               , adCreative.AdCreativeDeliveryType //dt.Rows[0]["AdCreativeDeliveryType"]
                                               , adCreative.Status
                                               , dt.Rows[0]["Weight"] //adCreative.Weight
                                               , userId
                                               , adCreative.DestinationUrl
                                               , adCreative.AppUrl
                                                , dataType
                                                , adCreative.PackageName
                                                , adCreative.IsAutoOpen ? 1 : 0
                                                , adCreative.ThirdPartyTrackingUrl
                                                , adCreative.AppInfo.AppIcon
                                                , adCreative.AppInfo.Name
                                                , adCreative.AppInfo.Description
                                                , adCreative.AppInfo.Price
                                                , adCreative.AppInfo.FileSize
                                                , adCreative.AppInfo.Category
                                                , adCreative.AppInfo.DownloadCount == null ? string.Empty : adCreative.AppInfo.DownloadCount.ToString()
                                                , adCreative.AppInfo.Version
                                                , adCreative.AppInfo.Grade
                                                , adCreative.AppInfo.GradeCount
                                                , adCreative.AppInfo.Points
                                                , adCreative.AppInfo.AppId == null ? string.Empty : adCreative.AppInfo.AppId.ToString()
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
        ///     根据指定编号获取一个荐计划创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="adCreativeId">荐计划创意编号</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult GetRecommandCreativeById(uint userId, ulong adCreativeId)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (adCreativeId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal adCreative id.");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { adCreativeId, userId };
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetRecommandCreativeByCreativeId, ParametersObject.SpGetRecommandCreativeByCreativeId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpGetRecommandCreativeByCreativeId + ":Executed count = 0");
                return ExecuteResult.Succeed(_convertor.ConvertToRecommandCreative(dt.Rows[0]));
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     根据指定编号获取一个文字链创意
        /// </summary>
        /// <param name="userId">创建者</param>
        /// <param name="adCreativeId">广告创意编号</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult GetAdTextLinkCreativeById(uint userId, ulong adCreativeId)
        {
            if (adCreativeId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal adCreative id.");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { adCreativeId, userId };
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetTextLinkCreativeByCreativeID, ParametersObject.SpGetAdCreativeByCreativeId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpGetTextLinkCreativeByCreativeID + ":Executed count = 0");
                return ExecuteResult.Succeed(ConverterFactory.GetAdTextLinkCreativeConverter().PackToDomainObject(dt.Rows[0]));
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     根据指定编号获取一个Html创意
        /// </summary>
        /// <param name="userId">创建者</param>
        /// <param name="adCreativeId">广告创意编号</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult GetAdHtmlCreativeById(uint userId, ulong adCreativeId)
        {
            if (adCreativeId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal adCreative id.");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { adCreativeId, userId };
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetHtmlCreativeByAdCreativeID, ParametersObject.SpGetAdCreativeByCreativeId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpGetHtmlCreativeByAdCreativeID + ":Executed count = 0");
                return ExecuteResult.Succeed(ConverterFactory.GetAdHtmlCreativeConverter().PackToDomainObject(dt.Rows[0]));
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     根据AdGroupID获得图片尺寸列表
        /// </summary>
        /// <param name="adGroupId">AdGroupID</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult GetAppOSImageSize(ulong adGroupId)
        {
            if (adGroupId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal adGroupId.");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { adGroupId };
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetAppOSTypeImageSizeListByAdGroupID, ParametersObject.SpGetContentAndActionValueByGroupID, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpGetAppOSTypeImageSizeListByAdGroupID + ":Executed count = 0");
                AdSize[] adSize = new AdSize[dt.Rows.Count];
                for (int i = 0; i < adSize.Length; i++)
                    adSize[i] = ConverterFactory.GetAdSizeConverter().PackToDomainObject(dt.Rows[i]);
                return ExecuteResult.Succeed(adSize);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     根据指定编号获取一个图片创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="adCreativeId">图片创意编号</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult GetAdImageCreativeById(uint userId, ulong adCreativeId)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (adCreativeId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal adCreative id.");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { adCreativeId, userId };
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetImageCreativeByCreativeId, ParametersObject.SpGetImageCreativeByCreativeId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpGetImageCreativeByCreativeId + ":Executed count = 0");
                return ExecuteResult.Succeed(_convertor.ConvertToImageCreative(dt.Rows[0]));
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     获取指定广告组和条件所能对应的图片尺寸集合
        /// </summary>
        /// <param name="adGroupId">广告组编号</param>
        /// <param name="mediaTypeId">媒体编号</param>
        /// <param name="imageTypes">图片类型</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult GetImageSize(ulong adGroupId, uint mediaTypeId, ImageTypes imageTypes)
        {
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { adGroupId, mediaTypeId, imageTypes };
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetOSSameImageSizeList, ParametersObject.GetOSSameImageSizeList, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpGetOSSameImageSizeList + ":Executed count = 0");
                IList<ImageSizeSearchObject> temp = new List<ImageSizeSearchObject>();
                //fill temporary data.
                foreach (DataRow row in dt.Rows)
                {
                    ImageSizeSearchObject searchObj = new ImageSizeSearchObject();
                    searchObj.AppTypeOSId = (AppOSTypes)byte.Parse(row["AppTypeOSID"].ToString());
                    searchObj.ImageSizeCategoryId = ushort.Parse(row["ImageSizeCategoryID"].ToString());
                    searchObj.Height = ushort.Parse(row["Height"].ToString());
                    searchObj.Width = ushort.Parse(row["Width"].ToString());
                    searchObj.ImageSizeId = ushort.Parse(row["ImageSizeID"].ToString());
                    searchObj.MediaTypeId = ushort.Parse(row["MediaTypeID"].ToString());
                    searchObj.AdaptiveGroupId = ushort.Parse(row["AdaptiveGroupID"].ToString());
                    if (dt.Columns.Contains("CoreWidth"))
                        searchObj.CoreWidth = !Convert.IsDBNull(row["CoreWidth"])
                                                  ? float.Parse(row["CoreWidth"].ToString())
                                                  : 0F;
                    if (dt.Columns.Contains("CoreHeight"))
                        searchObj.CoreHeight = !Convert.IsDBNull(row["CoreHeight"])
                                                 ? float.Parse(row["CoreHeight"].ToString())
                                                 : 0F;
                    if (dt.Columns.Contains("CorePointX"))
                        searchObj.CorePointX = !Convert.IsDBNull(row["CorePointX"])
                                                 ? float.Parse(row["CorePointX"].ToString())
                                                 : 0F;
                    if (dt.Columns.Contains("CorePointY"))
                        searchObj.CorePointY = !Convert.IsDBNull(row["CorePointY"])
                                                 ? float.Parse(row["CorePointY"].ToString())
                                                 : 0F;
                    temp.Add(searchObj);
                }
                return ExecuteResult.Succeed(temp);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     创建一个新的图片创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">图片创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult CreateImageCreative(uint userId, ulong accountId, ImageAdCreative adCreative)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            if (adCreative == null) throw new ArgumentNullException("adCreative");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, adCreative.CampaignId, adCreative.GroupId, userId };
                //验证AdGroupId,CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountIdAndCampaignIdAndAdGroupId, ParametersObject.IsNotExistByAccountIdAndCampaignIdAndAdGroupId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCampaignIdAndAdGroupId + ":Executed count = 0");
                //ensure that currently creative's campaign bid-type.
                parameterValues = new object[] { adCreative.CampaignId, accountId, userId };
                dt = _slaveDB.SpExecuteTable(SpName.SpGetCampaignByCampaignId, ParametersObject.GetCampaignByCampaignId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, "#CANNOT query out any infomation about currently creative's campaign object");
                BidTypes campaignBidType = (BidTypes)byte.Parse(dt.Rows[0]["BidType"].ToString());
                if ((campaignBidType != BidTypes.CPM) && string.IsNullOrEmpty(adCreative.DestinationUrl))
                    return ExecuteResult.Fail(SystemErrors.NotFound, "#DestinationUrl MUST has value when currently creative's campaign bid-type != CPM");
                if (campaignBidType == BidTypes.CPM && adCreative.DestinationUrl == null) adCreative.DestinationUrl = string.Empty;
                if (adCreative.MediaTypeId != ushort.Parse(dt.Rows[0]["MediaTypeID"].ToString()))
                    return ExecuteResult.Fail(SystemErrors.NotFound, "#MediaTypeId MUST same with currently ad-creative's campaign.MediaTypeId!");
                if (!string.IsNullOrEmpty(adCreative.DestinationUrl))
                {
                    if (Encoding.Default.GetByteCount(adCreative.DestinationUrl) > 1024) return ExecuteResult.Fail(SystemErrors.NotFound, "#DestinationUrl max length cannot over 1024.");
                    //if (!ValidationRegex.RegexDestinationUrl.IsMatch(adCreative.DestinationUrl)) return ExecuteResult.Fail(SystemErrors.NotFound, "#DestinationUrl cannot pass the specific regex condition.");
                    //if (adCreative.DestinationUrl.Contains("tel") && !Regex.IsMatch(adCreative.DestinationUrl, @"^\d+$")) return ExecuteResult.Fail(SystemErrors.NotFound, "#DestinationUrl incorrectly type. If you chooise use 'tel' to the head, then you MUSTN'T type any numbers.");
                }
                //创建一条Campaign的信息
                dt = _masterDB.SpExecuteTable(SpName.SpAddAdImageCreative, ParametersObject.CreateNewImageCreative, new object[]
                                                {
                                                      adCreative.GroupId
                                                    , adCreative.CampaignId
                                                    , accountId
                                                    , adCreative.MediaTypeId
                                                    , adCreative.Name
                                                    , adCreative.CreativeType
                                                    , adCreative.AdCreativeDeliveryType //themeDeliveryType
                                                    , adCreative.Status
                                                    , adCreative.Weight
                                                    , userId
                                                    , adCreative.DestinationUrl
                                                    , adCreative.ImageUrl
                                                    , adCreative.Size.ImageSizeId
                                                    , adCreative.Size.Width
                                                    , adCreative.Size.Height
                                                    , adCreative.DownloadType //adCreative.Action.Id == 2 //BC *if ((actionId.Equals(2))) return true;*
                                                    , dataType
                                                    , adCreative.PackageName
                                                    , adCreative.IsAutoOpen ? 1 : 0
                                                    , adCreative.ThirdPartyTrackingUrl
                                                    , adCreative.CoreArea!=null?adCreative.CoreArea.Width:0
                                                    , adCreative.CoreArea!=null?adCreative.CoreArea.Height:0
                                                    , adCreative.CoreArea!=null?adCreative.CoreArea.CenterX:0
                                                    , adCreative.CoreArea!=null?adCreative.CoreArea.CenterY:0
                                                });
                return dt.Rows.Count == 0 ? ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpAddAdImageCreative + ":Executed count = 0") : ExecuteResult.Succeed((ulong)(long)dt.Rows[0]["CreativeID"]);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }


        /// <summary>
        ///     更新一个图片创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">图片创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult UpdateImageCreative(uint userId, ulong accountId, ImageAdCreative adCreative)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            if (adCreative == null) throw new ArgumentNullException("adCreative");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, adCreative.CampaignId, adCreative.GroupId, adCreative.CreativeId, userId };
                //验证AdGroupId,CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountIdAndCampaignIDAndAdGroupIDAndCreativeID, ParametersObject.IsNotExistByAccountIdAndCampaignIdAndAdGroupIdAndCreativeId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCampaignIDAndAdGroupIDAndCreativeID + ":Executed count = 0");
                //ensure that currently creative's campaign bid-type.
                dt = _slaveDB.SpExecuteTable(SpName.SpGetCampaignByCampaignId, ParametersObject.GetCampaignByCampaignId, new object[] { adCreative.CampaignId, accountId, userId });
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, "#CANNOT query out any infomation about currently creative's campaign object");
                BidTypes campaignBidType = (BidTypes)byte.Parse(dt.Rows[0]["BidType"].ToString());
                if (campaignBidType != BidTypes.CPM && string.IsNullOrEmpty(adCreative.DestinationUrl))
                    return ExecuteResult.Fail(SystemErrors.NotFound, "#DestinationUrl MUST has value when currently creative's campaign bid-type != CPM");
                //为无点击广告作支持
                if (campaignBidType == BidTypes.CPM && adCreative.DestinationUrl == null) adCreative.DestinationUrl = string.Empty;
                if (adCreative.MediaTypeId != ushort.Parse(dt.Rows[0]["MediaTypeID"].ToString()))
                    return ExecuteResult.Fail(SystemErrors.NotFound, "#MediaTypeId MUST same with currently ad-creative's campaign.MediaTypeId!");
                if (!string.IsNullOrEmpty(adCreative.DestinationUrl))
                {
                    if (Encoding.Default.GetByteCount(adCreative.DestinationUrl) > 1024) return ExecuteResult.Fail(SystemErrors.NotFound, "#DestinationUrl max length cannot over 1024.");
                    //if (!ValidationRegex.RegexDestinationUrl.IsMatch(adCreative.DestinationUrl)) return ExecuteResult.Fail(SystemErrors.NotFound, "#DestinationUrl cannot pass the specific regex condition.");
                    //if (adCreative.DestinationUrl.Contains("tel") && !Regex.IsMatch(adCreative.DestinationUrl, @"^\d+$")) return ExecuteResult.Fail(SystemErrors.NotFound, "#DestinationUrl incorrectly type. If you chooise use 'tel' to the head, then you MUSTN'T type any numbers.");
                }
                //获取数据库中的数据
                dt = _slaveDB.SpExecuteTable(SpName.SpGetImageCreativeByCreativeId, ParametersObject.SpGetImageCreativeByCreativeId, new object[] { adCreative.CreativeId, userId });
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, "#Cannot read any ad-image creative data from slave database.");
                //获取数据库中的数据
                DataTable otherDt = _slaveDB.SpExecuteTable(SpName.SpGetContentAndActionValueByGroupID, ParametersObject.SpGetContentAndActionValueByGroupID, new object[] { adCreative.GroupId });
                if (otherDt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, "#Cannot read any adgroup data from given ad-image creative in slave database.");
                //reads action & content data from data table.
                int adActionId = int.Parse(otherDt.Rows[0]["AdActionID"].ToString()), adContentId = int.Parse(otherDt.Rows[0]["AdContent"].ToString());
                bool originalDownloadType = byte.Parse(dt.Rows[0]["DownloadType"].ToString()) != 0x00;
                #region Check whether can update 'DownloadType' field value. (From BC logical)

                if (!(adContentId.Equals(5) && adActionId.Equals(2)) && !((adContentId.Equals(2) || adContentId.Equals(3) || adContentId.Equals(4)) && (adActionId.Equals(2))) && originalDownloadType != adCreative.DownloadType)
                    return ExecuteResult.Fail(SystemErrors.BusinessObjIsNotSupported, "#DownloadType field value cannot be update when current creative's group infomation(content id & action id) accords some conditions.");

                #endregion

                //修改一条TextCreative的信息
                _masterDB.SpExecuteTable(SpName.SpUpdateImageCreative, ParametersObject.UpdateAdImageCreative, new object[]
                                             {
                                                 adCreative.CreativeId
                                                 , adCreative.GroupId
                                                 , adCreative.CampaignId
                                                 , accountId
                                                 , dt.Rows[0]["MediaTypeID"] //adCreative.MediaTypeId
                                                 , adCreative.Name
                                                 , adCreative.AdCreativeDeliveryType
                                                 //dt.Rows[0]["AdCreativeDeliveryType"]
                                                 , adCreative.Status
                                                 , dt.Rows[0]["Weight"] //adCreative.Weight
                                                 , userId
                                                 , adCreative.DestinationUrl
                                                 , adCreative.ImageUrl
                                                 , adCreative.Size.ImageSizeId
                                                 , adCreative.Size.Width
                                                 , adCreative.Size.Height
                                                 , adCreative.DownloadType
                                                 //adCreative.Action.Id == 2 //BC *if ((actionId.Equals(2))) return true;*
                                                 , dataType
                                                 , adCreative.PackageName
                                                 , adCreative.IsAutoOpen ? 1 : 0
                                                 , adCreative.ThirdPartyTrackingUrl
                                                 , adCreative.CoreArea!=null?adCreative.CoreArea.Width:0
                                                 , adCreative.CoreArea!=null?adCreative.CoreArea.Height:0
                                                 , adCreative.CoreArea!=null?adCreative.CoreArea.CenterX:0
                                                 , adCreative.CoreArea!=null?adCreative.CoreArea.CenterY:0
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
        ///     更新一个文字链创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">文字链创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult UpdateTextLinkCreative(uint userId, ulong accountId, TextLinkAdCreative adCreative)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            if (adCreative == null) throw new ArgumentNullException("adCreative");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, adCreative.CampaignId, adCreative.GroupId, adCreative.CreativeId, userId };
                //验证AdGroupId,CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountIdAndCampaignIDAndAdGroupIDAndCreativeID, ParametersObject.IsNotExistByAccountIdAndCampaignIdAndAdGroupIdAndCreativeId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCampaignIDAndAdGroupIDAndCreativeID + ":Executed count = 0");
                //获取数据库中的数据
                DataTable otherDt = _slaveDB.SpExecuteTable(SpName.SpGetContentAndActionValueByGroupID, ParametersObject.SpGetContentAndActionValueByGroupID, new object[] { adCreative.GroupId });
                if (otherDt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, "#Cannot read any adgroup data from given ad-textlink creative in slave database.");
                //获取数据库中的数据
                dt = _slaveDB.SpExecuteTable(SpName.SpGetTextLinkCreativeByCreativeID, ParametersObject.SpGetAdCreativeByCreativeId, new object[] { adCreative.CreativeId, userId });
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, "#Cannot read any ad-textlink creative data from slave database.");
                //修改一条TextCreative的信息
                _masterDB.SpExecuteTable(SpName.SpUpdateTextLinkCreative, ParametersObject.UpdateAdTextLinkCreative, new object[]
                                             {
                                                 adCreative.CreativeId
                                                 , adCreative.GroupId
                                                 , adCreative.Title
                                                 , adCreative.DestinationUrl
                                                 , adCreative.CampaignId
                                                 , accountId
                                                 , dt.Rows[0]["MediaTypeID"] //adCreative.MediaTypeId
                                                 , adCreative.Name
                                                 , adCreative.AdCreativeDeliveryType//dt.Rows[0]["AdCreativeDeliveryType"]
                                                 , adCreative.ThirdPartyTrackingUrl
                                                 , adCreative.Status
                                                 , dt.Rows[0]["Weight"] //adCreative.Weight
                                                 , adCreative.PackageName
                                                 , adCreative.IsAutoOpen ? 1 : 0
                                                 , userId
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
        ///     更新一个Html创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">Html创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult UpdateHtmlCreative(uint userId, ulong accountId, HtmlAdCreative adCreative)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            if (adCreative == null) throw new ArgumentNullException("adCreative");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, adCreative.CampaignId, adCreative.GroupId, adCreative.CreativeId, userId };
                //验证AdGroupId,CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountIdAndCampaignIDAndAdGroupIDAndCreativeID, ParametersObject.IsNotExistByAccountIdAndCampaignIdAndAdGroupIdAndCreativeId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCampaignIDAndAdGroupIDAndCreativeID + ":Executed count = 0");
                //获取数据库中的数据
                DataTable otherDt = _slaveDB.SpExecuteTable(SpName.SpGetContentAndActionValueByGroupID, ParametersObject.SpGetContentAndActionValueByGroupID, new object[] { adCreative.GroupId });
                if (otherDt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, "#Cannot read any adgroup data from given ad-Html creative in slave database.");
                //获取数据库中的数据
                dt = _slaveDB.SpExecuteTable(SpName.SpGetHtmlCreativeByAdCreativeID, ParametersObject.SpGetAdCreativeByCreativeId, new object[] { adCreative.CreativeId, userId });
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, "#Cannot read any ad-Html creative data from slave database.");
                //修改一条TextCreative的信息
                _masterDB.SpExecuteTable(SpName.SpUpdateHtmlCreative, ParametersObject.UpdateAdHtmlCreative, new object[]
                                             {
                                                 adCreative.CreativeId
                                                 , adCreative.GroupId
                                                 , adCreative.DestinationUrl
                                                 , adCreative.Size.ImageSizeId
                                                 , adCreative.HtmlUrl
                                                 , adCreative.CampaignId
                                                 , accountId
                                                 , dt.Rows[0]["MediaTypeID"] //adCreative.MediaTypeId
                                                 , adCreative.Name
                                                 , adCreative.AdCreativeDeliveryType//dt.Rows[0]["AdCreativeDeliveryType"]
                                                 , adCreative.ThirdPartyTrackingUrl
                                                 , adCreative.Status
                                                 , dt.Rows[0]["Weight"] //adCreative.Weight
                                                 , adCreative.PackageName
                                                 , adCreative.IsAutoOpen ? 1 : 0
                                                 , userId
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
        ///     创建一个新的视频创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">视频创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult CreateVideoCreative(uint userId, ulong accountId, VideoAdCreative adCreative)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            if (adCreative == null) throw new ArgumentNullException("adCreative");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, adCreative.CampaignId, adCreative.GroupId, userId };
                //验证AdGroupId,CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountIdAndCampaignIdAndAdGroupId, ParametersObject.IsNotExistByAccountIdAndCampaignIdAndAdGroupId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCampaignIdAndAdGroupId + ":Executed count = 0");
                //创建一条Campaign的信息
                dt = _masterDB.SpExecuteTable(SpName.SpAddAdCreativeVideo
                                          , ParametersObject.CreateNewVideoCreative
                                          , new object[]
                                                {
                                                      adCreative.GroupId
                                                    , adCreative.CampaignId
                                                    , accountId
                                                    , adCreative.MediaTypeId
                                                    , adCreative.Name
                                                    , adCreative.CreativeType
                                                    , adCreative.AdCreativeDeliveryType //themeDeliveryType
                                                    , adCreative.Status
                                                    , adCreative.Weight
                                                    , userId
                                                    , adCreative.VideoUrl
                                                    , adCreative.VideoPreviewImageUrl
                                                    , adCreative.Size.ImageSizeId
                                                    , dataType
                                                    , adCreative.PackageName
                                                    , adCreative.IsAutoOpen ? 1 : 0
                                                    , adCreative.ThirdPartyTrackingUrl
                                                });
                return dt.Rows.Count == 0 ? ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpAddAdCreativeVideo + ":Executed count = 0") : ExecuteResult.Succeed((ulong)(long)dt.Rows[0]["CreativeID"]);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     更新一个视频创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">视频创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult UpdateVideoCreative(uint userId, ulong accountId, VideoAdCreative adCreative)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            if (adCreative == null) throw new ArgumentNullException("adCreative");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, adCreative.CampaignId, adCreative.GroupId, adCreative.CreativeId, userId };
                //验证CampaignId、AccountId、CreativeID从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountIdAndCampaignIDAndAdGroupIDAndCreativeID, ParametersObject.IsNotExistByAccountIdAndCampaignIdAndAdGroupIdAndCreativeId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCampaignIDAndAdGroupIDAndCreativeID + ":Executed count = 0");
                //获取数据库中的数据
                dt = _slaveDB.SpExecuteTable(SpName.SpGetVideoCreativeByCreativeId, new[] { "CreativeID", "Creator" }, new object[] { adCreative.CreativeId, userId });
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, "#Cannot read any ad-video creative data from slave database.");
                //修改一条VideoCreative的信息
                _masterDB.SpExecuteTable(SpName.SpUpdateVideoCreative
                                     , ParametersObject.UpdateAdVideoCreative
                                     , new object[]
                                           {
                                                 adCreative.CreativeId
                                               , adCreative.GroupId
                                               , adCreative.CampaignId
                                               , accountId
                                               , dt.Rows[0]["MediaTypeID"] //adCreative.MediaTypeId
                                               , adCreative.Name
                                               , adCreative.AdCreativeDeliveryType //dt.Rows[0]["AdCreativeDeliveryType"]
                                               , adCreative.Status
                                               , dt.Rows[0]["Weight"] //adCreative.Weight
                                               , userId
                                               , adCreative.VideoUrl
                                               , adCreative.VideoPreviewImageUrl
                                                , adCreative.Size.ImageSizeId
                                                , dataType
                                                , adCreative.PackageName
                                                , adCreative.IsAutoOpen ? 1 : 0
                                                , adCreative.ThirdPartyTrackingUrl
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
        ///     根据指定编号获取一个视频创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="adCreativeId">广告创意编号</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult GetAdVideoCreativeById(uint userId, ulong adCreativeId)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (adCreativeId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal adCreative id.");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { adCreativeId, userId };
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetVideoCreativeByCreativeId, new[] { "CreativeID", "Creator" }, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpGetVideoCreativeByCreativeId + ":Executed count = 0");
                return ExecuteResult.Succeed(_convertor.ConvertToVideoCreative(dt.Rows[0]));
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="adgroupId">广告组编号</param>
        /// <param name="mediaTypeId">媒体类型编号</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult GetImageSizeByGroupIdAndMediaTypeId(ulong adgroupId, uint mediaTypeId)
        {
            if (mediaTypeId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { adgroupId, mediaTypeId };
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetAppOSTypeImageSizeMapByAdGroupID, ParametersObject.GetAppOSTypeImageSizeMapByAdGroupID,
                                                       parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpGetAppOSTypeImageSizeMapByAdGroupID + ":Executed count = 0");
                AdSizeCategory[] adsizes = new AdSizeCategory[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                    adsizes[i] = ConverterFactory.GetAdSizeCategoryConverter().PackToDomainObject(dt.Rows[i]);
                return ExecuteResult.Succeed(adsizes);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     批量修改创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="creativeIds">创意编号数组</param>
        /// <param name="data">批量更新的数据实体</param>
        /// <param name="batchEditType">批量更新创意的枚举类型</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult UpdateBatchCreativesByIds(uint userId, ulong accountId, ulong[] creativeIds, BatchCreative data, BatchEditType batchEditType)
        {
            if (creativeIds == null || creativeIds.Length == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal creativeIds");
            try
            {
                StringBuilder sCreative = new StringBuilder();
                for (int i = 0; i < creativeIds.Length; i++)
                {
                    if (i < creativeIds.Length - 1)
                        sCreative.Append(creativeIds[i]).Append(',');
                    else
                        sCreative.Append(creativeIds[i]);
                }
                string creativeString = sCreative.ToString();
                //获取参数的值
                object[] parameterValues = new object[] { accountId, creativeString };
                //验证CreativeId和AccountId从属关系
                uint selectCount = _slaveDB.SpExecuteScalar<uint>(SpName.SpIsNotExistByAccountIdAndCreativeId,
                                                        ParametersObject.IsNotExistByAccountIdAndCreativeId,
                                                        parameterValues);
                if (selectCount != creativeIds.Length) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCreativeId + ":Executed count = 0 #Paramate [AccountId] is " + accountId + " [CreativeId] is " + creativeString);
                parameterValues = new object[] { creativeString };
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsSameGroupForCreativesIds, ParametersObject.IsSameGroupForCreatives, parameterValues);
                if (dt == null || dt.Rows.Count == 0 || dt.Rows.Count > 1)
                    return ExecuteResult.Fail(SystemErrors.BusinessObjError, SpName.SpIsSameGroupForCreativesIds + ":Creatives has more than one groupid");
                //设置参数的值
                parameterValues = new object[] { data.DestinationUrl, data.TrackUrl, batchEditType, creativeString, data.VideoUrl, selectCount, data.PackName, data.IsAutoOpen, userId };
                _masterDB.SpExecuteTable(SpName.SpUpdateBatchCreativeByIds, ParametersObject.UpdateBatchCreativesByIds, parameterValues);
                return ExecuteResult.Succeed(true);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }


        /// <summary>
        ///     批量修改新文字创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="creativeIds">创意编号数组</param>
        /// <param name="data">批量更新的数据实体</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult UpdateBatchNewTextCreativeByIds(uint userId, ulong accountId, ulong[] creativeIds, BatchNewTextCreative data)
        {
            if (creativeIds == null || creativeIds.Length == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal creativeIds");
            try
            {
                StringBuilder sCreative = new StringBuilder();
                for (int i = 0; i < creativeIds.Length; i++)
                {
                    if (i < creativeIds.Length - 1)
                        sCreative.Append(creativeIds[i]).Append(',');
                    else
                        sCreative.Append(creativeIds[i]);
                }
                string creativeString = sCreative.ToString();
                //获取参数的值
                object[] parameterValues = new object[] { accountId, creativeString };
                //验证CreativeId和AccountId从属关系
                uint selectCount = _slaveDB.SpExecuteScalar<uint>(SpName.SpIsNotExistByAccountIdAndCreativeId,
                                                        ParametersObject.IsNotExistByAccountIdAndCreativeId,
                                                        parameterValues);
                if (selectCount != creativeIds.Length) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCreativeId + ":Executed count = 0 #Paramate [AccountId] is " + accountId + " [CreativeId] is " + creativeString);
                parameterValues = new object[] { creativeString };
                //验证创意是否属于同一个广告组
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsSameGroupForCreativesIds, ParametersObject.IsSameGroupForCreatives, parameterValues);
                if (dt == null || dt.Rows.Count == 0 || dt.Rows.Count > 1)
                    return ExecuteResult.Fail(SystemErrors.BusinessObjError, SpName.SpIsSameGroupForCreativesIds + ":Creatives has more than one groupid");
                //设置参数的值
                parameterValues = new object[] { data.TrackUrl, data.LogoImageUrl, data.CustomElement.ToString(), data.DestinationUrl, data.IsSuspension, creativeString, data.PackName, data.IsAutoOpen, userId };
                _masterDB.SpExecuteTable(SpName.SpUpdateBatchNewTextCreativeByIds, ParametersObject.SpUpdateBatchNewTextCreatives, parameterValues);
                return ExecuteResult.Succeed(true);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     根据IDs更改物料状态
        /// </summary>
        /// <param name="accountId">账户id</param>
        /// <param name="creativeIds">主键IDs</param>
        /// <param name="status">状态</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult UpdateStatusByIds(ulong accountId, ulong[] creativeIds, AdStatus status)
        {
            if (creativeIds == null || creativeIds.Length == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal creativeIds");
            try
            {
                object[] parameterValues;
                StringBuilder sCreative = new StringBuilder();
                foreach (ulong creativeId in creativeIds) sCreative.Append(creativeId).Append(',');
                string creativeString = sCreative.ToString().TrimEnd(',');
                //获取参数的值
                parameterValues = new object[] { accountId, creativeString };
                //验证CampaignId和AccountId从属关系
                uint selectCount = _slaveDB.SpExecuteScalar<uint>(SpName.SpIsNotExistByAccountIdAndCreativeId,
                                                        ParametersObject.IsNotExistByAccountIdAndCreativeId,
                                                        parameterValues);
                if (selectCount != creativeIds.Length) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCreativeId + ":Executed count = 0 #Paramate [AccountId] is " + accountId + " [CreativeId] is " + creativeString);
                //获取参数的值
                parameterValues = new object[] { creativeString, status, 0 };
                _masterDB.SpExecuteTable(SpName.SpUpdateCreativesStatus, ParametersObject.SpUpdateCreativesStatus,
                                                       parameterValues);
                return ExecuteResult.Succeed(true);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     根据广告组和广告创意编号获取下面所存在的所有创意数据列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adgroupId">广告组编号</param>
        /// <returns>返回获取到的创意数据列表</returns>
        public IExecuteResult<AdCreativeSimpleData[]> GetAdCreativeSimpleList(uint userId, ulong accountId, ulong adgroupId)
        {
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, adgroupId, userId };
                DataTable table = _slaveDB.SpExecuteTable(SpName.SpGetAdCreativeSimpleDataList, ParametersObject.GetAdCreativeSimpleDataList, parameterValues);
                if (table.Rows.Count == 0) return ExecuteResult<AdCreativeSimpleData[]>.Succeed(null);
                AdCreativeSimpleData[] data = new AdCreativeSimpleData[table.Rows.Count];
                for (int i = 0; i < table.Rows.Count; i++)
                    data[i] = ConverterFactory.GetAdCreativeSimpeDataConverter().ConvertToNetworkObject(table.Rows[i]);
                return ExecuteResult<AdCreativeSimpleData[]>.Succeed(data);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult<AdCreativeSimpleData[]>.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     获得主题数量
        /// </summary>
        /// <param name="creativeIds">创意列表</param>
        /// <returns>执行结果</returns>
        public IExecuteResult<short> GetThemeCountByCreativeIds(string creativeIds)
        {
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { creativeIds };
                short returnCount = _slaveDB.SpExecuteScalar<short>(SpName.SpGetThemeCountByCreativeIds, new[] { "CreativeIds" }, parameterValues);
                if (returnCount > 0) return ExecuteResult<short>.Succeed(returnCount);
                return ExecuteResult<short>.Fail(SystemErrors.Unknown, "#ThemeCount: The ThemeCount is Smaller than 1  ");
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult<short>.Fail(SystemErrors.Unknown, ex.Message);
            };
        }


        /// <summary>
        ///     根据主题ID获得模板尺寸列表
        /// </summary>
        /// <param name="themeId">主题ID</param>
        /// <returns>执行的结果</returns>
        public IExecuteResult<TemplateSizeData[]> GetTemplateSizeByThemeId(ushort themeId)
        {
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { themeId };
                DataTable table = _slaveDB.SpExecuteTable(SpName.SpGetTemplateSizeByThemeId, new[] { "ThemeID" }, parameterValues);
                if (table.Rows.Count == 0) return ExecuteResult<TemplateSizeData[]>.Fail(SystemErrors.Unknown, "#SpGetTemplateSizeByThemeId :The RowCount is 0");
                TemplateSizeData[] data = new TemplateSizeData[table.Rows.Count];
                for (int i = 0; i < table.Rows.Count; i++)
                    data[i] = AdCreativeORMConvertor.Instance.ConvertToTemplateSizeCreative(table.Rows[i]);
                return ExecuteResult<TemplateSizeData[]>.Succeed(data);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult<TemplateSizeData[]>.Fail(SystemErrors.Unknown, ex.Message);
            };
        }

        /// <summary>
        ///     获取主题信息
        /// </summary>
        /// <param name="adGroupId">广告组ID</param>
        /// <param name="authority">权限</param>
        /// <param name="dataTypeData">数据类型</param>
        /// <returns>执行结果</returns>
        public IExecuteResult<AdCreativeThemeData[]> GetThemeByCondition(ulong adGroupId, ushort authority, ushort dataTypeData)
        {
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { dataTypeData, authority, adGroupId };
                DataTable table = _slaveDB.SpExecuteTable(SpName.SpGetThemeByCondition, new[] { "DataType", "Authority", "AdGroupID" }, parameterValues);
                if (table.Rows.Count == 0) return ExecuteResult<AdCreativeThemeData[]>.Fail(SystemErrors.Unknown, "#SpGetThemeByCondition :The RowCount is 0");
                AdCreativeThemeData[] data = new AdCreativeThemeData[table.Rows.Count];
                for (int i = 0; i < table.Rows.Count; i++)
                    data[i] = AdCreativeORMConvertor.Instance.ConvertToAdCreativeTheme(table.Rows[i]);
                return ExecuteResult<AdCreativeThemeData[]>.Succeed(data);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult<AdCreativeThemeData[]>.Fail(SystemErrors.Unknown, ex.Message);
            }
            ;
        }

        /// <summary>
        ///     获得文字创意Json数据
        /// </summary>
        /// <param name="adTextCreativeData">文字创意</param>
        /// <returns>执行结果</returns>
        public IExecuteResult<AdTextCreativeTemplateData> GetTextCreativeJsonData(AdTextCreativeData adTextCreativeData)
        {
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { adTextCreativeData.CreativeId, adTextCreativeData.GroupId, adTextCreativeData.Title,
                 adTextCreativeData.DestinationUrl, adTextCreativeData.DisplayUrl, adTextCreativeData.Desc1,  adTextCreativeData.Desc2, 
                 adTextCreativeData.LogoImageUrl, adTextCreativeData.CreativeType, adTextCreativeData.Size, adTextCreativeData.TemplateId,
                 adTextCreativeData.CustomElement,adTextCreativeData.IsSuspension};
                DataTable table = _slaveDB.SpExecuteTable(SpName.SpGetTextCreativeJsonData, ParametersObject.GetTextCreativeJsonData, parameterValues);
                if (table.Rows.Count == 0) return ExecuteResult<AdTextCreativeTemplateData>.Fail(SystemErrors.Unknown, "#SpGetTextCreativeJsonData :The RowCount is 0");
                AdTextCreativeTemplateData data = new AdTextCreativeTemplateData();
                data.Data = table.Rows[0]["DATA"].ToString();
                data.Template = table.Rows[0]["Template"].ToString();
                return ExecuteResult<AdTextCreativeTemplateData>.Succeed(data);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult<AdTextCreativeTemplateData>.Fail(SystemErrors.Unknown, ex.Message);
            };
        }

        /// <summary>
        ///     根据创意编号获取模板尺寸
        /// </summary>
        /// <param name="adTextCreativeId">文字创意编号</param>
        /// <returns>执行结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult<TemplateSizeData[]> GetTemplateSizeByCreativeId(ulong[] adTextCreativeId)
        {
            if (adTextCreativeId == null) throw new ArgumentNullException("adTextCreativeId");
            try
            {
                //获取单个模板尺寸
                if (adTextCreativeId.Length == 1)
                {
                    StringBuilder textCreativeId = new StringBuilder();
                    textCreativeId = textCreativeId.Append(adTextCreativeId[0]);
                    DataTable table = _slaveDB.SpExecuteTable(SpName.SpGetTemplateSizeByCreativeID,
                                                              new[] { "AdCreativeID" }, new object[] { textCreativeId });
                    if (table.Rows.Count == 0)
                        return ExecuteResult<TemplateSizeData[]>.Fail(SystemErrors.Unknown,
                                                                      "#GetTemplateSizeByCreativeID : The RowCount is 0");
                    TemplateSizeData[] templateSizeData = new TemplateSizeData[1];
                    templateSizeData[0] = _convertor.ConvertToTemplateSizeCreative(table.Rows[0]);
                    return ExecuteResult<TemplateSizeData[]>.Succeed(templateSizeData);
                }
                //获取多个模板尺寸
                else
                {
                    StringBuilder textCreativeIds = new StringBuilder();
                    for (int i = 0; i < adTextCreativeId.Length; i++)
                    {
                        textCreativeIds.Append(adTextCreativeId[i]);
                        if (i != adTextCreativeId.Length - 1) textCreativeIds.Append(',');
                    }
                    DataTable table = _slaveDB.SpExecuteTable(SpName.SpGetTemplateSizeByCreativeIDs,
                                                              new[] { "AdCreativeIDs" }, new object[] { textCreativeIds });
                    if (table.Rows.Count != adTextCreativeId.Length)
                        return ExecuteResult<TemplateSizeData[]>.Fail(SystemErrors.Unknown,
                                                                      "#GetTemplateSizeByCreativeIDs : The RowCount != adTextCreativeId.Length");
                    TemplateSizeData[] templateSizeData = new TemplateSizeData[adTextCreativeId.Length];
                    for (int i = 0; i < adTextCreativeId.Length; i++)
                    {
                        templateSizeData[i] = _convertor.ConvertToTemplateSizeCreative(table.Rows[i]);
                    }
                    return ExecuteResult<TemplateSizeData[]>.Succeed(templateSizeData);
                }
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult<TemplateSizeData[]>.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     根据文字创意编号获取指定的带Value自定义元素
        /// </summary>
        /// <param name="adTextCreativeId">文字创意编号</param>
        /// <returns>执行结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult<CustomElementWithValueData[]> GetCusElementByAdCreativeId(ulong adTextCreativeId)
        {
            if (adTextCreativeId == 0) return ExecuteResult<CustomElementWithValueData[]>.Fail(SystemErrors.Malformed, "#Illegal adTextCreative Id.");
            try
            {
                //获取参数的值
                DataTable table = _slaveDB.SpExecuteTable(SpName.SpGetCusElementByAdCreativeID, new[] { "AdCreativeID" }, new object[] { adTextCreativeId });
                if (table.Rows.Count == 0) return ExecuteResult<CustomElementWithValueData[]>.Fail(SystemErrors.Unknown, "#SpGetCusElementByAdCreativeID :The RowCount is 0");
                CustomElementWithValueData[] datas = new CustomElementWithValueData[table.Rows.Count];
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    datas[i] = _convertor.ConvertToCustomElementWithValue(table.Rows[i]);
                }
                return ExecuteResult<CustomElementWithValueData[]>.Succeed(datas);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult<CustomElementWithValueData[]>.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     根据主题编号获取指定的自定义元素
        /// </summary>
        /// <param name="themeId">主题编号</param>
        /// <returns>执行结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult<CustomElementData[]> GetCusElementByThemeId(ushort themeId)
        {
            if (themeId == 0) return ExecuteResult<CustomElementData[]>.Fail(SystemErrors.Malformed, "#Illegal themeId Id.");
            try
            {
                //获取参数的值
                DataTable table = _slaveDB.SpExecuteTable(SpName.SpGetCusElementByThemeID, new[] { "ThemeID" }, new object[] { themeId });
                if (table.Rows.Count == 0) return ExecuteResult<CustomElementData[]>.Fail(SystemErrors.Unknown, "#SpGetCusElementByThemeID :The RowCount is 0");
                CustomElementData[] datas = new CustomElementData[table.Rows.Count];
                for (int i = 0; i < table.Rows.Count; i++)
                    datas[i] = _convertor.ConvertToCustomElement(table.Rows[i]);
                return ExecuteResult<CustomElementData[]>.Succeed(datas);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult<CustomElementData[]>.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        #region PushCreative

        /// <summary>
        ///     创建一个新的推送创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">推送创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult CreatePushCreative(uint userId, ulong accountId, PushAdCreative adCreative)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            if (adCreative == null) throw new ArgumentNullException("adCreative");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, adCreative.CampaignId, adCreative.GroupId, userId };
                //验证AdGroupId,CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountIdAndCampaignIdAndAdGroupId, ParametersObject.IsNotExistByAccountIdAndCampaignIdAndAdGroupId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCampaignIdAndAdGroupId + ":Executed count = 0");
                //创建一条PushCreative的信息
                dt = _masterDB.SpExecuteTable(SpName.SpAddCreativePush
                                          , ParametersObject.CreateNewPushCreative
                                          , new object[]
                                                {
                                                      adCreative.GroupId
                                                    , adCreative.CampaignId
                                                    , accountId
                                                    , adCreative.MediaTypeId
                                                    , adCreative.Name ?? string.Empty
                                                    , adCreative.CreativeType
                                                    , adCreative.AdCreativeDeliveryType //themeDeliveryType
                                                    , adCreative.Status
                                                    , adCreative.Weight
                                                    , userId
                                                    , adCreative.AdType
                                                    , adCreative.TargetUrl ?? string.Empty
                                                    , adCreative.Monitor ?? string.Empty
                                                    , adCreative.Content ?? string.Empty
                                                    , adCreative.ExpireDate
                                                    , adCreative.Ext ?? string.Empty
                                                    , adCreative.Size.ImageSizeId
                                                    , adCreative.GenerateAdData() ?? string.Empty
                                                    , dataType                                                   
                                                    , adCreative.PackageName
                                                    , adCreative.IsAutoOpen ? 1 : 0
                                                    , adCreative.ThirdPartyTrackingUrl
                                                });
                return dt.Rows.Count == 0 ? ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpAddCreativePush + ":Executed count = 0") : ExecuteResult.Succeed((ulong)(long)dt.Rows[0]["CreativeID"]);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     更新一个推送创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">推送创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        public IExecuteResult UpdatePushCreative(uint userId, ulong accountId, PushAdCreative adCreative)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            if (adCreative == null) throw new ArgumentNullException("adCreative");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, adCreative.CampaignId, adCreative.GroupId, adCreative.CreativeId, userId };
                //验证CampaignId、AccountId、CreativeID从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpIsNotExistByAccountIdAndCampaignIDAndAdGroupIDAndCreativeID, ParametersObject.IsNotExistByAccountIdAndCampaignIdAndAdGroupIdAndCreativeId, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpIsNotExistByAccountIdAndCampaignIDAndAdGroupIDAndCreativeID + ":Executed count = 0");
                //获取数据库中的数据
                dt = _slaveDB.SpExecuteTable(SpName.SpGetPushCreativeByCreativeId, new[] { "CreativeID", "Creator" }, new object[] { adCreative.CreativeId, userId });
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, "#Cannot read any ad-Push creative data from slave database.");
                //修改一条PushCreative的信息
                _masterDB.SpExecuteTable(SpName.SpUpdatePushCreative
                                     , ParametersObject.UpdateAdPushCreative
                                     , new object[]
                                           {
                                                 adCreative.CreativeId
                                               , adCreative.GroupId
                                               , adCreative.CampaignId
                                               , accountId
                                               , dt.Rows[0]["MediaTypeID"] //adCreative.MediaTypeId
                                               , adCreative.Name ?? string.Empty
                                               , dt.Rows[0]["AdCreativeDeliveryType"] //adCreative.AdCreativeDeliveryType
                                               , adCreative.Status
                                               , dt.Rows[0]["Weight"] //adCreative.Weight
                                               , userId
                                               , adCreative.AdType
                                               , adCreative.Content ?? string.Empty
                                               , adCreative.TargetUrl ?? string.Empty
                                               , adCreative.Monitor ?? string.Empty
                                               , adCreative.ExpireDate
                                               , adCreative.Ext ?? string.Empty
                                               , adCreative.GenerateAdData() ?? string.Empty    
                                               , dataType
                                               , adCreative.Size.ImageSizeId
                                               , adCreative.PackageName
                                               , adCreative.IsAutoOpen ? 1 : 0
                                               , adCreative.ThirdPartyTrackingUrl
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
        ///     根据指定编号获取一个推送创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="adCreativeId">广告创意编号</param>
        /// <returns>返回执行后的结果</returns>
        public IExecuteResult GetAdPushCreativeById(uint userId, ulong adCreativeId)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (adCreativeId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal adCreative id.");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { adCreativeId, userId };
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetPushCreativeByCreativeId, new[] { "CreativeID", "Creator" }, parameterValues);
                if (dt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpGetPushCreativeByCreativeId + ":Executed count = 0");
                return ExecuteResult.Succeed(ConverterFactory.GetAdPushCreativeConverter().PackToDomainObject(dt.Rows[0]));
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        #endregion

        /// <summary>
        ///     静态注入db实例
        /// </summary>
        /// <param name="masterDB">主DB</param>
        /// <param name="slaveDB">从DB</param>
        /// <param name="convertor">对象转换器</param>
        public static void Inject(Database masterDB, Database slaveDB, AdCreativeORMConvertor convertor)
        {
            if (null == masterDB) throw new ArgumentNullException("masterDB");
            if (null == convertor) throw new ArgumentNullException("convertor");
            _masterDB = masterDB;
            _slaveDB = slaveDB;
            _convertor = convertor;
        }

        #endregion


    }
}