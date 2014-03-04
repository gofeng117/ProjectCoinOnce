namespace MobiSage.AdsAPI.Domain.Repository.Objects
{
#if TDD
    internal class SpName
#else
    internal class SpName
#endif
    {
        /// <summary>
        ///     获取具有指定Id的Campaign信息的存储过程名称
        /// </summary>
        public const string SpGetCampaignByCampaignId = "spGetCampaignByCampaignID";

        /// <summary>
        ///     获取指定账户下所有Campaign信息的存储过程名称
        /// </summary>
        public const string SpGetCampaigns = "spGetCampaigns";

        /// <summary>
        ///     创建一个新的Campaign处理器的存储过程名称
        /// </summary>
        public const string SpAddCampaign = "spAddCampaign";

        /// <summary>
        ///     创建一个新的Campaign处理器的存储过程名称
        /// </summary>
        public const string SpUpdateCampaign = "spUpdateCampaign";

        /// <summary>
        ///     创建一个新的Campaign处理器的存储过程名称
        /// </summary>
        public const string SpGetStartDateByCampaignId = "spGetStartDateByCampaignId";

        /// <summary>
        ///     创建一个新的AdCreativeText处理器的存储过程名称
        /// </summary>
        public const string SpAddAdCreativeText = "spAddCreativeText";

        /// <summary>
        ///     检查Account，Campaign，AdGroup是否存在的存储过程名称
        /// </summary>
        public const string SpIsNotExistByAccountIdAndCampaignIdAndAdGroupId =
            "spIsNotExistByAccountIdAndCampaignIDAndAdGroupID";

        /// <summary>
        ///     获取具有指定Id的Campaign信息的存储过程名称
        /// </summary>
        public const string SpGetTextCreativeByCreativeId = "SpGetTextCreativeByCreativeID";

        /// <summary>
        ///     根据指定账户广告组下的所有文字创意信息
        /// </summary>
        public const string SpGetTextCreatives = "SpGetTextCreatives";

        /// <summary>
        ///     更新一个新的Creative处理器的存储过程名称
        /// </summary>
        public const string SpUpdateTextCreative = "spUpdateTextCreative";

        /// <summary>
        ///     检查Account是否存在的存储过程名称
        /// </summary>
        public const string SpIsNotExistByAccountId =
            "spIsNotExistByAccountId";

        /// <summary>
        ///     检查Account，Campaign是否存在的存储过程名称
        /// </summary>
        public const string SpIsNotExistByAccountIdAndCampaignId =
            "spIsNotExistByAccountIdAndCampaignID";

        /// <summary>
        ///     检查Account，Campaigns是否存在的存储过程名称
        /// </summary>
        public const string SpIsNotExistByAccountIdAndCampaignIds =
            "spIsNotExistByAccountIdAndCampaignIDs";

        /// <summary>
        ///     创建一个新的AdGroup处理器的存储过程名称
        /// </summary>
        public const string SpAddAdGroup = "spAddAdGroup";

        /// <summary>
        ///     创建一个新的SpGetAdGroupByAdGroupId处理器的存储过程名称
        /// </summary>
        public const string SpGetAdGroupByAdGroupId = "SpGetAdGroupByAdGroupId";

        /// <summary>
        ///     获得SpGetCampaignSimpleByCampaignId的存储过程名称
        /// </summary>
        public const string SpGetCampaignSimpleByCampaignId = "spGetCampaignSimpleByCampaignId";

        /// <summary>
        ///     获得SpGetAdGroupSimpleByAdGroupId的存储过程名称
        /// </summary>
        public const string SpGetAdGroupSimpleByAdGroupId = "spGetAdGroupSimpleByAdGroupId";

        /// <summary>
        ///     根据指定发布者编号获取APP信息
        /// </summary>
        public const string SpGetAppByPublishId = "spGetAppByPublishID";

        /// <summary>
        ///     获取指定账户下所有的AdGroup信息
        /// </summary>
        public const string SpGetAdGroups = "spGetAdGroups";

        /// <summary>
        ///     根据id修改广告组状态
        /// </summary>
        public const string SpUpdateGroupStatusById = "spUpdateGroupStatusById";

        /// <summary>
        ///     根据id修改广告组状态
        /// </summary>
        public const string SpUpdateGroupsStatus = "spUpdateGroupsStatus";

        /// <summary>
        ///     根据id修改创意状态
        /// </summary>
        public const string SpUpdateCreativeStatusById = "spUpdateCreativeStatusById";

        /// <summary>
        ///     创建一个新的SpUpdateAdGroup处理器的存储过程名称
        /// </summary>
        public const string SpUpdateAdGroup = "SpUpdateAdGroup";

        /// <summary>
        ///     创建一个新的AdCreativeVideo处理器的存储过程名称
        /// </summary>
        public const string SpAddAdCreativeVideo = "spAddCreativeVideo";

        /// <summary>
        ///     创建一个新的SpGetVideoCreativeByCreativeId处理器的存储过程名称
        /// </summary>
        public const string SpGetVideoCreativeByCreativeId = "spGetVideoCreativeByCreativeID";

        /// <summary>
        ///     更新一个新的SpUpdateVideoCreative处理器的存储过程名称
        /// </summary>
        public const string SpUpdateVideoCreative = "spUpdateVideoCreative";

        /// <summary>
        ///     更新一个新的SpUpdateCampaignStatusById处理器的存储过程名称
        /// </summary>
        public const string SpUpdateCampaignStatusById = "spUpdateCampaignStatusById";

        /// <summary>
        ///     批量更新的SpUpdateCampaignStatus处理器的存储过程名称
        /// </summary>
        public const string SpUpdateCampaignsStatus = "spUpdateCampaignsStatus";

        /// <summary>
        ///     创建一个新的AdCreativePush处理器的存储过程名称
        /// </summary>
        public const string SpAddCreativePush = "spAddCreativePush";

        /// <summary>
        ///     更新一个新的SpUpdatePushCreative处理器的存储过程名称
        /// </summary>
        public const string SpUpdatePushCreative = "spUpdatePushCreative";

        /// <summary>
        ///     创建一个新的SpGetPushCreativeByCreativeId处理器的存储过程名称
        /// </summary>
        public const string SpGetPushCreativeByCreativeId = "spGetPushCreativeByCreativeID";

        /// <summary>
        ///     检查目标账户类型
        /// </summary>
        public const string SpGetAccountTypeFromAccountDetail = "spGetAccountTypeFromAccountDetail";

        public const string SpGetAccountRelation = "spGetAccountRelation";
        public const string SpGetOSSameImageSizeList = "spGetOSSameImageSizeList";

        /// <summary>
        ///     创建一个新的AdCreativeText处理器的存储过程名称
        /// </summary>
        public const string SpAddAdImageCreative = "spAddCreativeImage";

        public const string SpGetImageCreativeByCreativeId = "spGetImageCreativeByCreativeID";

        public const string SpIsNotExistByAccountIdAndCampaignIDAndAdGroupIDAndCreativeID =
            "spIsNotExistByAccountIdAndCampaignIDAndAdGroupIDAndCreativeID";

        public const string SpUpdateImageCreative = "spUpdateImageCreative";
        public const string SpGetContentAndActionValueByGroupID = "spGetContentAndActionValueByGroupID";

        ///<summary>
        ///     检查图片的尺寸是否合法
        /// </summary>
        public const string SpGetAppOSTypeImageSizeMapByAdGroupID = "spGetAppOSTypeImageSizeMapByAdGroupID";

        /// <summary>
        /// 添加BC的Account
        /// </summary>
        public const string SpAddAccount = "spAddAccount";

        /// <summary>
        /// 添加BC的Account关系
        /// </summary>
        public const string SpOA_AddCustomerRelationship = "spOA_AddCustomerRelationship";

        public const string SpGetUserTagsByCampaignId = "spGetUserTagsByCampaignId";
        public const string SpGetUserTagsByAdGroupId = "spGetUserTagsByAdGroupId";
        public const string SpCheckRunStatusByAdGroupIds = "spCheckRunStatusByAdGroupIds";
        public const string SpCheckRunStatusByCampaignId = "spCheckRunStatusByCampaignId";
        public const string SpGetTagIdByCampaignId = "spGetTagIdByCampaignId";

        /// <summary>
        /// 获得OSType的ImageSize信息
        /// </summary>
        public const string SpGetAppOSTypeImageSizeListByAdGroupID = "spGetAppOSTypeImageSizeListByAdGroupID";

        /// <summary>
        ///     添加文字链创意信息
        /// </summary>
        public const string SpAddCreativeTextLink = "spAddCreativeTextLink";

        /// <summary>
        ///     获得文字链创意（第二个存储过程） 
        /// </summary>
        public const string SpGetTextLinkCreativeByCreativeID = "spGetTextLinkCreativeByCreativeID";

        /// <summary>
        ///     更新文字链信息
        /// </summary>
        public const string SpUpdateTextLinkCreative = "spUpdateTextLinkCreative";

        /// <summary>
        ///     添加html信息
        /// </summary>
        public const string SpAddCreativeHtml = "spAddCreativeHtml";

        /// <summary>
        ///     获得html的创意信息
        /// </summary>
        public const string SpGetHtmlCreativeByAdCreativeID = "spGetHtmlCreativeByAdCreativeID";

        /// <summary>
        ///     更新html创意信息
        /// </summary>
        public const string SpUpdateHtmlCreative = "spUpdateHtmlCreative";

        public const string SpAddCreativeRecommand = "spAddCreativeRecommand";

        /// <summary>
        ///     新荐计划添加的存储过程
        /// </summary>
        public const string SpAddCreativeRecommend = "spAddCreativeRecommend";
        public const string SpAddLog = "spAddLog";

        /// <summary>
        ///     修改账户类型
        /// </summary>
        public const string SpUpdateAccountType = "spUpdateAccountType";

        public const string SpGetRecommandCreativeByCreativeId = "spGetRecommandCreativeByCreativeId";
        public static string SpGetAdGroupSimpleInfo = "spGetAdGroupSimpleInfo";
        public static string SpUpdateCreativeRecommand = "spUpdateCreativeRecommand";
        /// <summary>
        ///     新荐计划编辑的存储过程
        /// </summary>
        public static string SpUpdateCreativeRecommend = "spUpdateCreativeRecommend";

        public const string SpBuildEmailLog = "spBuildEmailLog";
        public static string SpGetAdCreativeSimpleDataList = "spGetAdCreativeList";

        public const string SpUpdateCreativesStatus = "spUpdateCreativesStatus";

        public const string SpGetThemeCountByCreativeIds = "spGetThemeCountByCreativeIds";

        public const string SpGetTemplateSizeByThemeId = "spGetTemplateSizeByThemeId";

        public const string SpGetThemeByCondition = "spGetThemeByCondition";

        public const string SpGetTextCreativeJsonData = "spGetTextCreativeJsonData";

        public const string SpGetTemplateSizeByCreativeID = "spGetTemplateSizeByCreativeID";

        public const string SpGetTemplateSizeByCreativeIDs = "spGetTemplateSizeByCreativeIDs";

        public const string SpGetCusElementByAdCreativeID = "spGetCusElementByAdCreativeID";

        public const string SpGetCusElementByThemeID = "spGetCusElementByThemeID";

        public const string SpIsNotExistByAccountIdAndCreativeId = "spIsNotExistByAccountIdAndCreativeID";

        public const string SpIsNotExistByAccountIdAndGroupId = "spIsNotExistByAccountIdAndGroupID";
        public const string SpUpdateBatchCreativeByIds = "spUpdateBatchCreativeByIds";
        public const string SpIsSameGroupForCreativesIds = "spIsSameGroupForCreativesIds";
        public const string SpUpdateBatchNewTextCreativeByIds = "spUpdateBatchNewTextCreatives";
        public const string SpAddQualityRingLog = "spAddQualityRingLog";

        public const string SpBuildQualityRingLog = "spBuildQualityRingLog";

        public const string SpBuildQualityRingDetailLog = "spBuildQualityRingDetailLog";
    }
}