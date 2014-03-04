using System;
using System.Data;
using MobiSage.AdsAPI.Domain.Enums;
using MobiSage.AdsAPI.Domain.ProtocolMetadata;
using MySql.Data.Types;

namespace MobiSage.AdsAPI.Domain.Repository
{
#if TDD
    public class CampaignSimpleORMConvertor : MarshalByRefObject, IORMConvertor<CampaignSimpleData>
#else
    public class CampaignSimpleORMConvertor : IORMConvertor<AdGroup>
#endif
    {
        #region Members

        public static readonly IORMConvertor<CampaignSimpleData> Instance = new CampaignSimpleORMConvertor();

        #endregion

        #region Implementation of IORMConvertor<CampaignSimpleData>

        /// <summary>
        ///     将关系型数据转化成领域对象
        /// </summary>
        /// <param name="dataRow">关系型数据对象</param>
        /// <returns>领域对象实例</returns>
        public CampaignSimpleData ConvertToDomain(DataRow dataRow)
        {
            if (dataRow == null) throw new ArgumentNullException("dataRow");
            if (Convert.IsDBNull(dataRow["StartDate"])) throw new ArgumentNullException();

            CampaignSimpleData campaignSimpleData = new CampaignSimpleData();
            campaignSimpleData.Status = (AdStatus)byte.Parse(dataRow["Status"].ToString());
            campaignSimpleData.DailyBudget = decimal.Parse(dataRow["DailyBudget"].ToString());
            campaignSimpleData.RemainingBudget = decimal.Parse(dataRow["RemainingBudget"].ToString());
            campaignSimpleData.MediaTypeId = uint.Parse(dataRow["MediaTypeId"].ToString());
            campaignSimpleData.Name = (string)dataRow["Name"];
            campaignSimpleData.AccountId = ulong.Parse(dataRow["AccountId"].ToString());
            campaignSimpleData.CampaignId = ulong.Parse(dataRow["CampaignID"].ToString());
            campaignSimpleData.DeliveryRemainingAd = dataRow["DeliveryRemainingAd"].ToString() == "1";
            campaignSimpleData.BidType = (BidTypes) byte.Parse(dataRow["BidType"].ToString());
            campaignSimpleData.StartDate = ((MySqlDateTime)dataRow["StartDate"]).GetDateTime();
            campaignSimpleData.EndDate = Convert.IsDBNull(dataRow["EndDate"]) ? (DateTime?)null : ((MySqlDateTime)dataRow["EndDate"]).GetDateTime();
            campaignSimpleData.CreateDate = ((MySqlDateTime)dataRow["CreateDate"]).GetDateTime();
            campaignSimpleData.WaterMark = byte.Parse(dataRow["WaterMark"].ToString()) == 0x01;
            campaignSimpleData.AdSource = (AdSourceTypes)byte.Parse(dataRow["AdSource"].ToString());

            return campaignSimpleData;
 
        }

        #endregion
    }
}