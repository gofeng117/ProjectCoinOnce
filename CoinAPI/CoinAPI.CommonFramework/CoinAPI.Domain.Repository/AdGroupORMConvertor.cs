using System;
using System.Data;
using System.Linq;
using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.Domain.Business.Targeting;
using MobiSage.AdsAPI.Domain.Enums;
using MySql.Data.Types;

namespace MobiSage.AdsAPI.Domain.Repository
{
#if TDD
    public class AdGroupORMConvertor : MarshalByRefObject, IORMConvertor<AdGroup>
#else
    public class AdGroupORMConvertor : IORMConvertor<AdGroup>
#endif
    {
        #region Members

        public static readonly IORMConvertor<AdGroup> Instance = new AdGroupORMConvertor();

        #endregion

        #region Implementation of IORMConvertor<AdGroup>

        /// <summary>
        ///     将关系型数据转化成领域对象
        /// </summary>
        /// <param name="dataRow">关系型数据对象</param>
        /// <returns>领域对象实例</returns>
        public AdGroup ConvertToDomain(DataRow dataRow)
        {
            if (dataRow == null) throw new ArgumentNullException("dataRow");
            string forwardStr = Convert.IsDBNull(dataRow["ForwardTags"]) ? null : dataRow["ForwardTags"].ToString();
            string backwardStr = Convert.IsDBNull(dataRow["BackwardTags"]) ? null : dataRow["BackwardTags"].ToString();
            ushort[] forwardArr = string.IsNullOrEmpty(forwardStr) ? null : forwardStr.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(ushort.Parse).ToArray();
            ushort[] backwardArr = string.IsNullOrEmpty(backwardStr) ? null : backwardStr.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(ushort.Parse).ToArray();
            string appCategories = Convert.IsDBNull(dataRow["AppCategorys"]) ? string.Empty : (string) dataRow["AppCategorys"];
            string extraAppCategories = Convert.IsDBNull(dataRow["ExtraAppCategory"]) ? string.Empty : (string)dataRow["ExtraAppCategory"];
            AdGroup adGroup = AdGroup.Create((ulong)(long)dataRow["GroupID"], (ulong)(long)dataRow["CampaignID"],
                                             new PricePolicy((decimal)dataRow["AcBidPrice"]), (ulong)(long)dataRow["Weight"],
                                             new AdTargeting(new DeviceTargeting(Convert.IsDBNull(dataRow["OpTypes"]) ? null : (string)dataRow["OpTypes"],
                                                                                Convert.IsDBNull(dataRow["OSVersions"]) ? null : (string)dataRow["OSVersions"],
                                                                                Convert.IsDBNull(dataRow["SDKVersions"]) ? null : (string)dataRow["SDKVersions"],
                                                                                Convert.IsDBNull(dataRow["Apps"]) ? null : (string)dataRow["Apps"],
                                                                                (appCategories/* + "," + extraAppCategories*/)),
                                                             new FrequencyCapTargeting
                                                             {
                                                                 MaxClicksEveryDay = (ulong)(long)dataRow["MaxClicksEveryDay"],
                                                                 MaxClicksEveryDayByDevice = (ulong)(long)dataRow["MaxClicksEveryDayByDevice"],
                                                                 MaxClicksSevenDaysByDevice = (ulong)(long)dataRow["MaxClicksSevenDaysByDevice"],
                                                                 MaxImpressionsEveryDay = (ulong)(long)dataRow["MaxImpressionsEveryDay"],
                                                                 MaxImpressionsEveryDayByDevice = (ulong)(long)dataRow["MaxImpressionsEveryDayByDevice"],
                                                                 MaxImpressionsSevenDaysByDevice = (ulong)(long)dataRow["MaxImpressionsSevenDaysByDevice"],
                                                                 MaxClicksPutIn = (ulong)(long)dataRow["MaxClicksPutIn"],
                                                                 MaxImpressionsPutIn = (ulong)(long)dataRow["MaxImpressionsPutIn"]
                                                             },
                                                             new TimeSpanTargeting((Convert.IsDBNull(dataRow["WeekTimePeriodIDs"]) ? null : (string)dataRow["WeekTimePeriodIDs"])),
                                                             new UserTagTargeting(forwardArr, backwardArr)),
                                                             (DisplayTypes)byte.Parse(dataRow["DisplayType"].ToString()));
            adGroup.Name = (string)dataRow["Name"];
            adGroup.MediaTypeId = ushort.Parse(dataRow["MediaTypeID"].ToString());
            adGroup.ITunesAppCategoryId = Convert.IsDBNull(dataRow["ITunesAppCategoryID"]) ? 0U : uint.Parse(dataRow["ITunesAppCategoryID"].ToString());
            adGroup.Status = (AdStatus)byte.Parse(dataRow["AdStatusID"].ToString());
            adGroup.BidPrice = (decimal)dataRow["BidPrice"];
            adGroup.MaxClicks = (uint)(int)dataRow["MaxClicks"];
            adGroup.MaxImpressions = (uint)(int)dataRow["MaxImpressions"];
            adGroup.MaxPerUserImpressions = (uint)(int)dataRow["MaxPerUserImpressions"];
            adGroup.CPAPrice = (decimal)dataRow["CPAPrice"];
            adGroup.DeliveryMode = (DeliveryModes)byte.Parse(dataRow["DeliveryMode"].ToString());
            adGroup.ITunesAppId = Convert.IsDBNull(dataRow["ITunesAppID"]) ? (ulong?)null : ulong.Parse(dataRow["ITunesAppID"].ToString());
            adGroup.ActionType = (AdActionTypes)byte.Parse(dataRow["AdActionID"].ToString());
            adGroup.ContentType = new AdContentType(uint.Parse(dataRow["AdContentID"].ToString()), null);
            adGroup.DeviceCategory = (string)dataRow["DeviceCategory"];
            adGroup.NetworkTypes = Convert.IsDBNull(dataRow["DeviceNetworks"]) ? new DeviceNetwork() : new DeviceNetwork((string)dataRow["DeviceNetworks"]);
            adGroup.ThirdpartyPlatformId = (short)(Convert.IsDBNull(dataRow["ThirdPartPlatformID"]) ? -1 : short.Parse(dataRow["ThirdPartPlatformID"].ToString()));
            adGroup.PublishId = Convert.IsDBNull(dataRow["PublishID"]) ? null : dataRow["PublishID"].ToString();
            adGroup.ITunesBundleId = Convert.IsDBNull(dataRow["ITunesBundleId"]) ? null : dataRow["ITunesBundleId"].ToString();
            adGroup.CreateDate = ((MySqlDateTime)dataRow["CreateDate"]).GetDateTime();
            adGroup.ModifyDate = ((MySqlDateTime)dataRow["ModifyDate"]).GetDateTime();
            return adGroup;
        }


        #endregion
    }
}