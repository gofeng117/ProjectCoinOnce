using System;
using System.Data;
using MobiSage.AdsAPI.Domain.Enums;
using MobiSage.AdsAPI.Domain.ProtocolMetadata;
using MySql.Data.Types;

namespace MobiSage.AdsAPI.Domain.Repository
{
#if TDD
    public class AdGroupSimpleORMConvertor : MarshalByRefObject, IORMConvertor<AdGroupSimpleData>
#else
    public class AdGroupORMConvertor : IORMConvertor<AdGroup>
#endif
    {
        #region Members

        public static readonly IORMConvertor<AdGroupSimpleData> Instance = new AdGroupSimpleORMConvertor();

        #endregion

        #region Implementation of IORMConvertor<AdGroupSimpleData>

        /// <summary>
        ///     将关系型数据转化成领域对象
        /// </summary>
        /// <param name="dataRow">关系型数据对象</param>
        /// <returns>领域对象实例</returns>
        public AdGroupSimpleData ConvertToDomain(DataRow dataRow)
        {
            if (dataRow == null) throw new ArgumentNullException("dataRow");


            AdGroupSimpleData adGroupSimple = new AdGroupSimpleData();
            adGroupSimple.AdGroupId = (ulong) (long) dataRow["GroupID"];
            adGroupSimple.CampaignId = (ulong) (long) dataRow["CampaignID"];
            adGroupSimple.Weight = (ulong) (long) dataRow["Weight"];
            adGroupSimple.MaxClicksEveryDay = (ulong) (long) dataRow["MaxClicksEveryDay"];
            adGroupSimple.MaxClicksEveryDayByDevice = (ulong) (long) dataRow["MaxClicksEveryDayByDevice"];
            adGroupSimple.MaxClicksSevenDaysByDevice = (ulong) (long) dataRow["MaxClicksSevenDaysByDevice"];
            adGroupSimple.MaxImpressionsEveryDay = (ulong) (long) dataRow["MaxImpressionsEveryDay"];
            adGroupSimple.MaxImpressionsEveryDayByDevice = (ulong) (long) dataRow["MaxImpressionsEveryDayByDevice"];
            adGroupSimple.MaxImpressionsSevenDaysByDevice = (ulong) (long) dataRow["MaxImpressionsSevenDaysByDevice"];
            adGroupSimple.MaxClicksPutIn = (ulong) (long) dataRow["MaxClicksPutIn"];
            adGroupSimple.MaxImpressionsPutIn = (ulong) (long) dataRow["MaxImpressionsPutIn"];
            adGroupSimple.Name = (string)dataRow["Name"];
            adGroupSimple.MediaTypeId = ushort.Parse(dataRow["MediaTypeID"].ToString());
            adGroupSimple.ITunesAppCategoryId = Convert.IsDBNull(dataRow["ITunesAppCategoryID"]) ? 0U : uint.Parse(dataRow["ITunesAppCategoryID"].ToString());
            adGroupSimple.Status = (AdStatus)byte.Parse(dataRow["AdStatusID"].ToString());
            adGroupSimple.BidPrice = (decimal)dataRow["BidPrice"];
            adGroupSimple.MaxClicks = (uint)(int)dataRow["MaxClicks"];
            adGroupSimple.MaxImpressions = (uint)(int)dataRow["MaxImpressions"];
            adGroupSimple.MaxPerUserImpressions = (uint)(int)dataRow["MaxPerUserImpressions"];
            adGroupSimple.CPAPrice = (decimal)dataRow["CPAPrice"];
            adGroupSimple.DeliveryMode = (DeliveryModes)byte.Parse(dataRow["DeliveryMode"].ToString());
            adGroupSimple.ITunesAppId = Convert.IsDBNull(dataRow["ITunesAppID"]) ? (ulong?)null : ulong.Parse(dataRow["ITunesAppID"].ToString());
            adGroupSimple.ActionType = (AdActionTypes)byte.Parse(dataRow["AdActionID"].ToString());
            adGroupSimple.ContentId = uint.Parse(dataRow["AdContentID"].ToString());
            adGroupSimple.ITunesBundleId = Convert.IsDBNull(dataRow["ITunesBundleId"]) ? null : dataRow["ITunesBundleId"].ToString();
            adGroupSimple.CreateDate = ((MySqlDateTime)dataRow["CreateDate"]).GetDateTime();
            adGroupSimple.DevPrice = (decimal)dataRow["AcBidPrice"];
            adGroupSimple.DisplayType = (DisplayTypes)byte.Parse(dataRow["DisplayType"].ToString());
            adGroupSimple.AffiliateId = ushort.Parse(dataRow["AffiliateID"].ToString());
            adGroupSimple.BusinessId = ushort.Parse(dataRow["BusinessID"].ToString());
            return adGroupSimple;
        }


        #endregion
    }
}