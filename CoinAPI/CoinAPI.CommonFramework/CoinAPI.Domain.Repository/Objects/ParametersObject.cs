namespace MobiSage.AdsAPI.Domain.Repository.Objects
{
#if TDD
    public class ParametersObject
#else
    internal class ParametersObject
#endif
    {
        #region UpdateCampaignByIdProcessor

        /// <summary>
        ///     获取具有指定Id的Campaign信息的存储过程参数对象
        /// </summary>
        public static readonly string[] GetCampaignByCampaignId = { "CampaignID", "AccountID", "Creator" };

        /// <summary>
        ///     根据指定账户下所有Campaign信息的存储过程参数对象
        /// </summary>
        public static readonly string[] GetCampaigns = { "AccountID", "Creator" };

        /// <summary>
        ///     获取更新Campaign信息的存储过程参数对象
        /// </summary>
        public static readonly string[] UpdateCampaignById =
            {
                "CampaignID"
                , "AccountId"
                , "MediaTypeID"
                , "Name"
                , "DailyBudget"
                , "BidType"
                , "StartDate"
                , "EndDate"
                , "DeliveryRemainingAd"
                , "ModifyBy"
                , "LocationListString"
                , "WaterMark"
                , "Status"
                , "MoreDaysBudget"
                , "IsMoreDaysBudget"
                , "IsAutoTransform"
                , "IsConfine"
                , "IsAutoActivation"
                , "ActivationType"
                , "AdUrl"
                , "UdidType"
                , "AppleStore"
            };

        #endregion

        #region CreateNewCampaignProcessor

        /// <summary>
        ///     获取创建Campaign信息的存储过程参数对象
        /// </summary>
        public static readonly string[] CreateNewCampaign =
            {
                "AccountId"
                , "MediaTypeID"
                , "Name"
                , "DailyBudget"
                , "BidType"
                , "StartDate"
                , "EndDate"
                , "DeliveryRemainingAd"
                , "Status"
                , "Creator"
                , "LocationListString"
                , "WaterMark"
                , "MoreDaysBudget"
                , "IsMoreDaysBudget"
                , "IsAutoTransform"
                , "IsConfine"
                , "IsAutoActivation"
                , "ActivationType"
                , "AdUrl"
                , "UdidType"
                , "AppleStore"
                , "AdSource"
            };

        #endregion

        #region CreateNewAdCreativeProcessor

        /// <summary>
        ///     获取检查Account，Campaign，AdGroup是否存在的存储过程参数对象
        /// </summary>
        public static readonly string[] IsNotExistByAccountIdAndCampaignIdAndAdGroupId =
            {
                "AccountId", "CampaignId",
                "AdGroupId", "Creator"
            };

        /// <summary>
        ///     获取创建AdCreative信息的存储过程参数对象
        /// </summary>
        public static readonly string[] CreateNewAdCreative =
            {
                "AdGroupID"
                , "CampaignID"
                , "AccountID"
                , "MediaTypeID"
                , "AdCreativeName"
                , "AdCreativeType"
                , "ThemeDeliveryType"
                , "Status"
                , "Weight"
                , "Creator"
                , "Title"
                , "DestinationURL"
                , "DisplayURL"
                , "Desc1"
                , "Desc2"
                , "ImageURL"
                , "DataType"
                , "Size"
                , "CusEleValue"
                , "IsSuspension"
                , "TemplateID"
                , "PackageName"
                , "IsAutoOpen"
                , "TrackingURL"
            };

        /// <summary>
        ///     获取创建AdImageCreative信息的存储过程参数对象
        /// </summary>
        public static readonly string[] CreateNewImageCreative =
            {
                "AdGroupID"
                , "CampaignID"
                , "AccountID"
                , "MediaTypeID"
                , "AdCreativeName"
                , "AdCreativeType"
                , "ThemeDeliveryType"
                , "Status"
                , "Weight"
                , "Creator"
                , "DestinationURL"
                , "ImageURL"
                , "ImageSize"
                , "ImageWidth"
                , "ImageHeight"
                , "DownloadType"
                , "DataType"
                , "PackageName"
                , "IsAutoOpen"
                , "TrackingURL"
                , "CoreWidth"
                , "CoreHeight"
                , "CorePointX"
                , "CorePointY"
            };

        #endregion

        #region UpdateAdCreativeProcessor

        /// <summary>
        ///     获取检查Creative，Campaign，Account，AdGroup是否存在的存储过程参数对象
        /// </summary>
        public static readonly string[] CheckAndGetCreative =
            {
                "CreativeID", "CampaignId", "AccountId", "AdGroupId", "Creator"
            };

        #endregion

        #region UpdateAdCreativeProcessor

        /// <summary>
        ///     获取检查Campaign，Account，AdGroup是否存在的存储过程参数对象
        /// </summary>
        public static readonly string[] CheckAndGetCreatives =
            {
                "CampaignId", "AccountId", "AdGroupId", "Creator"
            };

        /// <summary>
        ///     获取创建AdCreative信息的存储过程参数对象
        /// </summary>
        public static readonly string[] UpdateAdTextCreative =
            {
                "CreativeID"
                , "AdGroupID"
                , "CampaignID"
                , "AccountID"
                , "MediaTypeID"
                , "AdCreativeName"
                , "ThemeDeliveryType"
                , "Status"
                , "Weight"
                , "ModifyBy"
                , "Title"
                , "DestinationURL"
                , "DisplayURL"
                , "Desc1"
                , "Desc2"
                , "ImageURL"
                , "DataType"
                , "Size"
                , "CusEleValue"
                , "IsSuspension"
                , "PackageName"
                , "IsAutoOpen"
                , "TrackingURL"
            };

        #endregion

        #region CreateNewAdGroupProcessor

        /// <summary>
        ///     获取检查Account，Campaign，AdGroup是否存在的存储过程参数对象
        /// </summary>
        public static readonly string[] IsNotExistByAccountIdAndCampaignId =
            {
                "AccountId", "CampaignId", "Creator"
            };

        /// <summary>
        ///     获取检查Account，Campaigns，AdGroup是否存在的存储过程参数对象
        /// </summary>
        public static readonly string[] IsNotExistByAccountIdAndCampaignIds =
            {
                "AccountId", "CampaignIds", "Creator"
            };

        /// <summary>
        ///     获取创建AdGroup信息的存储过程参数对象
        /// </summary>
        public static readonly string[] CreateNewAdGroup =
            {
                "CampaignID"
                , "AccountID"
                , "MediaTypeID"
                , "Name"
                , "BidPrice"
                , "AcBidPrice"
                , "Status"
                , "DeliveryMode"
                , "Weight"
                , "Creator"
                , "MaxClicksEveryDayByDevice"
                , "MaxImpressionsEveryDayByDevice"
                , "MaxClicksSevenDaysByDevice"
                , "MaxImpressionsSevenDaysByDevice"
                , "MaxClicksEveryDay"
                , "MaxImpressionsEveryDay"
                , "MaxClicksPutIn"
                , "MaxImpressionsPutIn"
                , "ITunesAppID"
                , "AdActionID"
                , "AdContent"
                , "WeekTimePeriodIdsStr"
                , "ThreepartyPlatformID"
                , "DeviceCategory"
                , "DeviceNetwork"
                , "AppCategory"
                , "SDKVersion"
                , "OSVersion"
                , "Operator"
                , "Apps"
                , "ITunesAppCategoryID"
                , "CPAPrice"
                , "MaxImpressions"
                , "MaxClicks"
                , "MaxPerUserImpressions"
                , "PublishIds"
                , "ForwardTags"
                , "BackwardTags"
                , "ITunesBundleId"
                , "DisplayType"
            };

        #endregion

        #region UpdateAdGroupProcessor

        /// <summary>
        ///     获取检查Account，Campaign，AdGroup是否存在的存储过程参数对象
        /// </summary>
        public static readonly string[] CheckAndGetAdGroup =
            {
                "CampaignId", "AccountId", "AdGroupId", "Creator"
            };

        public static readonly string[] GetAdGroupById =
            {
                "AccountId", "AdGroupId", "Creator"
            };

        public static readonly string[] GetAdGroupSimpleById =
            {
                "AdGroupId", "Creator"
            };

        public static readonly string[] GetAdGroups =
            {
                "CampaignId", "AccountId", "Creator"
            };

        public static readonly string[] UpdateGroupStatusById =
            {
                "AdGroupId", "Status"
            };

        public static readonly string[] UpdateGroupsStatus =
            {
                "GroupIds", "Status", "ModifyBy"
            };

        public static readonly string[] UpdateCreativeStatusById =
            {
                "AdCreativeId", "Status"
            };

        public static readonly string[] GetAppByPublishId = { "PublishID" };
        public static readonly string[] GetAccountRelation = { "AccountID" };
        public static readonly string[] GetCreativeTextByAdCreativeID = { "AdCreativeID" };

        /// <summary>
        ///     获取更新AdGroup信息的存储过程参数对象
        /// </summary>
        public static readonly string[] UpdateAdGroup =
            {
                "AdGroupID"
                , "CampaignID"
                , "AccountID"
                , "MediaTypeID"
                , "Name"
                , "BidPrice"
                , "AcBidPrice"
                , "Status"
                , "DeliveryMode"
                , "Weight"
                , "AdActionID"
                , "AdContent"
                , "WeekTimePeriodIdsStr"
                , "ThreepartyPlatformID"
                , "MaxClicksEveryDayByDevice"
                , "MaxImpressionsEveryDayByDevice"
                , "MaxClicksSevenDaysByDevice"
                , "MaxImpressionsSevenDaysByDevice"
                , "MaxClicksEveryDay"
                , "MaxImpressionsEveryDay"
                , "MaxClicksPutIn"
                , "MaxImpressionsPutIn"
                , "ITunesAppID"
                , "ModifyBy"
                , "DeviceCategory"
                , "DeviceNetwork"
                , "AppCategory"
                , "SDKVersion"
                , "OSVersion"
                , "Operator"
                , "Apps"
                , "ITunesAppCategoryID"
                , "CPAPrice"
                , "MaxImpressions"
                , "MaxClicks"
                , "MaxPerUserImpressions"
                , "PublishIds"
                , "ForwardTags"
                , "BackwardTags"
                , "ITunesBundleId"
                , "DisplayType"
            };

        public static readonly string[] GetAccountTypeFromAccountDetail = new[] { "AccountID" };
        public static readonly string[] GetOSSameImageSizeList = new[] { "GroupId", "MediaTypeId", "Type" };
        public static readonly string[] SpGetImageCreativeByCreativeId = new[] { "CreativeId", "Creator" };

        public static readonly string[] IsNotExistByAccountIdAndCampaignIdAndAdGroupIdAndCreativeId =
            {
                "AccountId", "CampaignId", "AdGroupId", "CreativeId", "Creator"
            };

        public static readonly string[] UpdateAdImageCreative =
            {
                "CreativeID",
                "AdGroupID",
                "CampaignID",
                "AccountID",
                "MediaTypeID",
                "AdCreativeName",
                "ThemeDeliveryType",
                "Status",
                "Weight",
                "ModifyBy",
                "DestinationURL",
                "ImageURL",
                "ImageSize",
                "ImageWidth",
                "ImageHeight",
                "DownloadType",
                "DataType",
                "PackageName",
                "IsAutoOpen",
                "TrackingURL",
                "CoreWidth",
                "CoreHeight",
                "CorePointX",
                "CorePointY"
            };

        #region AdVideoCreative

        /// <summary>
        ///     获取创建AdVideoCreative信息的存储过程参数对象
        /// </summary>
        public static readonly string[] CreateNewVideoCreative =
            {
                "AdGroupID",
                "CampaignID",
                "AccountID",
                "MediaTypeID",
                "AdCreativeName",
                "AdCreativeType",
                "ThemeDeliveryType",
                "Status",
                "Weight",
                "Creator",
                "DestinationURL",
                "ImageURL",
                "ImageSize",
                "DataType",
                "PackageName",
                "IsAutoOpen",
                "TrackingURL"
            };

        /// <summary>
        ///     获取检查Creative，Campaign，Account，AdGroup是否存在的存储过程参数对象
        /// </summary>
        public static readonly string[] SpGetVideoCreativeByCreativeId =
            {
                "CreativeID", "CampaignId", "AccountId", "AdGroupId", "Creator"
            };

        /// <summary>
        ///     获取创建AdVideoCreative信息的存储过程参数对象
        /// </summary>
        public static readonly string[] UpdateAdVideoCreative =
            {
                "CreativeID"
                , "AdGroupID"
                , "CampaignID"
                , "AccountID"
                , "MediaTypeID"
                , "AdCreativeName"
                , "ThemeDeliveryType"
                , "Status"
                , "Weight"
                , "ModifyBy"
                , "DestinationURL"
                , "ImageURL"
                , "ImageSize"
                , "DataType"
                , "PackageName"
                , "IsAutoOpen"
                , "TrackingURL"
            };

        #endregion

        /// <summary>
        ///     获取查询图片尺寸信息的存储过程参数对象
        /// </summary>
        public static readonly string[] GetAppOSTypeImageSizeMapByAdGroupID =
            {
                "AdGroupID",
                "MediaTypeID"
            };

        #endregion

        #region AddCreativeTextLinkProcessor

        /// <summary>
        ///     获取创建AdTextLink信息的存储过程参数对象
        /// </summary>
        public static readonly string[] CreateNewAdTextLink =
            {

                "AdGroupID"
                , "CampaignID"
                , "AccountID"
                , "MediaTypeID"
                , "AdCreativeName"
                , "AdCreativeType"
                , "ThemeDeliveryType"
                , "Status"
                , "Weight"
                , "Creator"
                , "DestinationURL"
                , "DataType"
                , "PackageName"
                , "IsAutoOpen"
                , "TrackingURL"
                , "Title"
            };

        #endregion

        #region Others

        public static readonly string[] SpGetContentAndActionValueByGroupID = new[] { "AdGroupId" };

        /// <summary>
        ///     获取检查CreativeID，Creator是否存在的存储过程参数对象
        /// </summary>
        public static readonly string[] SpGetAdCreativeByCreativeId =
            {
                "CreativeID", "Creator"
            };

        // 添加Account方向的存储过程参数
        public static readonly string[] SpAddAccount = new[] { "AccountID", "Creator", "SourceId" };
        public static readonly string[] SpAddCustomerRelationship = new[] { "accountid", "customerid", "userid" };
        public static readonly string[] SpUpdateAccountType = new[] { "AccountID", "AccountTypeID", "ModifyBy" };
        public static readonly string[] GetUserTagsByCampaignId = new[] { "campaignId" };
        public static readonly string[] GetUserTagsByAdGroupId = new[] { "adgroupId" };
        public static readonly string[] CheckRunStatusByAdGroupIds = new[] { "adgroupIds" };
        public static readonly string[] CheckRunStatusByCampaignId = new[] { "campaignId" };
        public static readonly string[] GetTagIdByCampaignId = new[] { "campaignId" };

        #endregion

        public static readonly string[] UpdateAdTextLinkCreative =
            {
                "AdCreativeID"
                , "AdGroupID"
                , "Title"
                , "DestinationURL"
                , "CampaignID"
                , "AccountID"
                , "MediaTypeID"
                , "AdCreativeName"
                , "ThemeDeliveryType"
                , "TrackingURL"
                , "Status"
                , "Weight"
                , "PackageName"
                , "IsAutoOpen"
                , "ModifyBy"
            };


        public static readonly string[] CreateNewAdHtml =
            {
                "AdGroupID"
                , "CampaignID"
                , "AccountID"
                , "MediaTypeID"
                , "AdCreativeName"
                , "AdCreativeType"
                , "ThemeDeliveryType"
                , "Status"
                , "Weight"
                , "Creator"
                , "DestinationURL"
                , "DataType"
                , "PackageName"
                , "IsAutoOpen"
                , "TrackingURL"
                , "Size"
                , "HtmlPackageURL"
            };


        public static readonly string[] UpdateAdHtmlCreative =
            {
                "AdCreativeID"
                , "AdGroupID"
                , "DestinationURL"
                , "Size"
                , "HtmlPackageURL"
                , "CampaignID"
                , "AccountID"
                , "MediaTypeID"
                , "AdCreativeName"
                , "ThemeDeliveryType"
                , "TrackingURL"
                , "Status"
                , "Weight"
                , "PackageName"
                , "IsAutoOpen"
                , "ModifyBy"
            };


        public static string[] SpAddLog =
            {
                "ClassName",
                "CreateTime",
                "ServiceName",
                "ProcessInfo",
                "ProcessLevel",
                "ProcessDetail",
                "ProcessTrace"
            };

        public static readonly string[] CreateNewRecommandCreative =
            {
                "AdGroupID"
                , "CampaignID"
                , "AccountID"
                , "MediaTypeID"
                , "AdCreativeName"
                , "AdCreativeType"
                , "ThemeDeliveryType"
                , "Status"
                , "Weight"
                , "Creator"
                , "DestinationURL"
                , "AppURL"
                , "DataType"
                , "PackageName"
                , "IsAutoOpen"
                , "TrackingURL"
                , "AppIcon"
                , "AppName"
                , "AppDesc"
                , "AppPrice"
                , "AppFileSize"
                , "AppCategory"
                , "DownloadNumber"
                , "AppVersion"
                , "AppRate"
                , "AppRateNumber"
                , "Points"
                , "AppID"
            };

        public static readonly string[] GetAdGroupSimpleInfo =
            {
                "AdGroupId"
            };

        public static readonly string[] SpGetRecommandCreativeByCreativeId = new[] { "CreativeId", "Creator" };

        public static readonly string[] UpdateCreativeRecommand = new[]
            {
                "CreativeID"
                , "AdGroupID"
                , "CampaignID"
                , "AccountID"
                , "MediaTypeID"
                , "AdCreativeName"
                , "AdCreativeType"
                , "ThemeDeliveryType"
                , "Status"
                , "Weight"
                , "ModifyBy"
                , "DestinationURL"
                , "AppURL"
                , "DataType"
                , "PackageName"
                , "IsAutoOpen"
                , "TrackingURL"
                , "AppIcon"
                , "AppName"
                , "AppDesc"
                , "AppPrice"
                , "AppFileSize"
                , "AppCategory"
                , "DownloadNumber"
                , "AppVersion"
                , "AppRate"
                , "AppRateNumber"
                , "Points"
                , "AppID"
            };

        public static readonly string[] GetAdCreativeSimpleDataList = { "AccountID", "AdGroupID", "Creator" };

        public static readonly string[] SpBuildEmailLog;
        public static readonly string[] SpBuildQualityRingLog;

        // todo:Jason_Refactor 必须命名成存储过程的变量名减去【p_】，对于这种逻辑...我和我的小伙伴们都惊呆了...
        public static readonly string[] SpUpdateCreativesStatus = { "CreativeIDs", "Status", "ModifyBy" };


        public static readonly string[] GetTextCreativeJsonData =
            {
                "CreativeId", "GroupId", "Title", "DestinationUrl",
                "DisplayUrl", "Desc1", "Desc2", "LogoImageUrl",
                "CreativeType", "Size", "TemplateId", "CustomElement", "IsSuspension"
            };

        #region PushCreative

        /// <summary>
        ///     获取创建PushAdCreative信息的存储过程参数对象
        /// </summary>
        public static readonly string[] CreateNewPushCreative =
            {
                "AdGroupID"
                , "CampaignID"
                , "AccountID"
                , "MediaTypeID"
                , "AdCreativeName"
                , "AdCreativeType"
                , "ThemeDeliveryType"
                , "Status"
                , "Weight"
                , "Creator"
                , "AdType"
                , "Targeturl"
                , "Monitor"
                , "Content"
                , "ExpireDate"
                , "Ext"
                , "SizeID"
                , "Data"
                , "DataType"
                , "PackageName"
                , "IsAutoOpen"
                , "TrackingURL"
            };

        /// <summary>
        ///     获取创建PushAdCreative信息的存储过程参数对象
        /// </summary>
        public static readonly string[] UpdateAdPushCreative =
            {
                "CreativeID"
                , "AdGroupID"
                , "CampaignID"
                , "AccountID"
                , "MediaTypeID"
                , "AdCreativeName"
                , "ThemeDeliveryType"
                , "Status"
                , "Weight"
                , "ModifyBy"
                , "AdType"
                , "Content"
                , "Targeturl"
                , "Monitor"
                , "ExpireDate"
                , "Ext"
                , "Data"
                , "DataType"
                , "SizeID"
                , "PackageName"
                , "IsAutoOpen"
                , "TrackingURL"
            };


        #endregion

        public static readonly string[] IsNotExistByAccountIdAndCreativeId = { "AccountId", "CreativeId" };

        public static readonly string[] IsNotExistByAccountIdAndGroupId = { "AccountId", "AdGroupId" };

        public static readonly string[] UpdateBatchCreativesByIds = { "DestinationURL", "TrackingURL", "BatchEditType", "CreativeIds", "VideoUrl", "Count", "PackName", "IsAutoOpen", "ModifyBy" };

        public static readonly string[] IsSameGroupForCreatives = { "CreativeIds" };

        public static readonly string[] SpUpdateBatchNewTextCreatives = { "TrackingURL", "LogoImageURL", "CusEleValue", "DestinationURL", "IsSuspension", "CreativeIds", "PackName", "IsAutoOpen", "ModifyBy" };

        public static readonly string[] AddQualityRingLog = { "FatherTID", "ChildTID", "SendIP", "ReceiveIP", "SendServiceType", "ReceiveServiceType", "RequestMessage", "SendDateTime", "ReceiveDateTime", "ExecuteTime" };
        
        public static readonly string[] SpBuildQualityRingDetailLog = { "FatherTID" };
    }
}
