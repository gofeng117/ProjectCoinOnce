using System;
using System.Data;
using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.Domain.Business.Targeting;
using MobiSage.AdsAPI.Domain.Enums;
using MySql.Data.Types;

namespace MobiSage.AdsAPI.Domain.Repository
{
#if TDD
    public class CampaignORMConvertor : MarshalByRefObject, IORMConvertor<Campaign>
#else
    public class CampaignORMConvertor : IORMConvertor
#endif
    {
        #region Members

        public static readonly IORMConvertor<Campaign> Instance = new CampaignORMConvertor();

        #endregion

        #region Methods

        /// <summary>
        /// 将关系型数据转化成领域对象
        /// </summary>
        /// <param name="dataRow">关系型数据对象</param>
        /// <returns>领域对象实例</returns>
        public Campaign ConvertToDomain(DataRow dataRow)
        {
            if (null == dataRow) throw new ArgumentNullException("dataRow");
            if (Convert.IsDBNull(dataRow["StartDate"])) throw new ArgumentNullException();
            Campaign campaign = Campaign.Create(ulong.Parse(dataRow["AccountID"].ToString()),
                                               ulong.Parse(dataRow["CampaignID"].ToString()),
                                               uint.Parse(dataRow["MediaTypeID"].ToString()),
                                               new GeoTargeting((string)dataRow["GeoIDs"]),
                                               (BidTypes)byte.Parse(dataRow["BidType"].ToString()),
                                               byte.Parse(dataRow["WaterMark"].ToString()) == 0x01,
                                               ((MySqlDateTime)dataRow["StartDate"]).GetDateTime(),
                                               (AdSourceTypes)byte.Parse(dataRow["AdSource"].ToString()));
            campaign.Status = (AdStatus)byte.Parse(dataRow["Status"].ToString());
            campaign.DailyBudget = decimal.Parse(dataRow["DailyBudget"].ToString());
            campaign.RemainingBudget = decimal.Parse(dataRow["RemainingBudget"].ToString());
            campaign.EndDate = Convert.IsDBNull(dataRow["EndDate"]) ? (DateTime?)null : ((MySqlDateTime)dataRow["EndDate"]).GetDateTime();
            campaign.Name = (string)dataRow["Name"];
            campaign.ActivationType = dataRow["IsRealTimeDocking"].ToString() == "1";
            campaign.IsAutoActivation = dataRow["IsAutoDocking"].ToString() == "1";
            campaign.IsAutoTransform = dataRow["DockingWay"].ToString() == "1";
            switch (campaign.IsMoreDaysBudget)
            {
                case 1:
                    campaign.IsConfine = true;
                    break;
                default:
                    campaign.IsConfine = campaign.DailyBudget > 0;
                    break;
            }
            campaign.IsMoreDaysBudget = int.Parse(dataRow["IsDaysBudget"].ToString());
            if (campaign.IsMoreDaysBudget == 1)
            {
                campaign.MoreDayBudget.Initialize((string) dataRow["MoreDaysBudget"]);
            }
            if ((!System.Convert.IsDBNull(dataRow["UdidType"])))
            {
                campaign.UdidType = (string) dataRow["UdidType"];
            }
            else
            {
                campaign.UdidType = null;
            }
            if ((!System.Convert.IsDBNull(dataRow["AdvertiserMonitorURL"])))
            {
                campaign.AdUrl = (string) dataRow["AdvertiserMonitorURL"];
            }
            else
            {
                campaign.AdUrl = null;
            }
            if ((!System.Convert.IsDBNull(dataRow["BundleID"])))
            {
                campaign.AppleStore = (string) dataRow["BundleID"];
            }
            else
            {
                campaign.AppleStore = null;
            }
            campaign.DeliveryRemainingAd = dataRow["DeliveryRemainingAd"].ToString() == "1";
            campaign.CreateDate = ((MySqlDateTime)dataRow["CreateDate"]).GetDateTime();
            return campaign;
        }

        #endregion
    }
}
