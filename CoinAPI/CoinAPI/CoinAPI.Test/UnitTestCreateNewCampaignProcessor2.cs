using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobiSage.AdsAPI.ABS.Components.AdComp.Processors;
using MobiSage.AdsAPI.ABS.ProtocolStack;
using MobiSage.AdsAPI.CommonFramework.Transactions;
using MobiSage.AdsAPI.Domain.Business.Targeting;
using MobiSage.AdsAPI.Domain.Enums;
using MobiSage.AdsAPI.Domain.Midiator.Converters;
using MobiSage.AdsAPI.Domain.ProtocolMetadata;
using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.CommonFramework.DbAccess;
using MobiSage.AdsAPI.CommonFramework.Transactions;
using MobiSage.AdsAPI.Domain.Service;
using NUnit.Framework;
using Newtonsoft.Json;

namespace MobiSage.AdsAPI.ABS.Test
{
    public class UnitTestCreateNewCampaignProcessor2
    {
        private Campaign campaign;
        private CampaignData campaignData;
        //private GetCampaignByIdProcessor _processor = new GetCampaignByIdProcessor();

        #region Setup.
        [SetUp]
        public void Initialize()
        {
            DatabaseManager.Inject(new Database("Host=172.16.18.203;Database=AdAPI_Executor;User Id=root;password=1qaz;Charset=utf8;Convert Zero Datetime=true;Allow Zero Datetime=true;", 120), new Database("Host=172.16.18.203;Database=AdAPI_Executor;User Id=root;password=1qaz;Charset=utf8;Convert Zero Datetime=true;Allow Zero Datetime=true;", 120));
            ServiceManager.Instance.Initialize();
            Components.AdComp.Global.CampaignService = new CampaignService();
            Components.AdComp.Global.AdGroupService = new AdGroupService();
            Components.AdComp.Global.AdCreativeService = new AdCreativeService();
        }
        #endregion

        #region Methods
        [Test]
        public void CreateCampaign()
        {
            GeoTargeting currentGeo = new GeoTargeting("512,513");
            DateTime startTime = new DateTime(2013, 12, 30, 14, 00, 0, 0);
            DateTime endTime = new DateTime(2014, 1, 1, 00, 00, 0, 0);
            campaign = Campaign.Create(5709, 0, 1, currentGeo, BidTypes.CPC, true, startTime,AdSourceTypes.AdHub);
            campaign.EndDate = endTime;
            campaign.Name = "7777";
            campaign.Status= AdStatus.Launching;
            campaign.IsMoreDaysBudget = 0;
            //campaign.MoreDayBudget.Initialize("('2013-06-04','3'),('2013-06-05','3'),('2013-06-06','3')");
            campaign.DailyBudget = 5;
            campaign.AdUrl = "http://iphone.myzaker.com/zaker/ad/adsage.php?u={udid}&t={timestamp}";
            campaign.AppleStore = "";

            //campaignData = new CampaignData {AccountId = 5661};

           campaignData = ConverterFactory.GetCampaignConverter().ConvertToNetworkObject(campaign);

            CreateNewCampaignProcessor processor = new CreateNewCampaignProcessor();
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new CreateNewCampaignRequestMessage{UserId = 4696, Data = campaignData};
            processor.Process(transaction);

            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)transaction.Response;

        }

        [Test]
        public void UpdateCampaign()
        {
            
            GeoTargeting currentGeo = new GeoTargeting("512,513");
            DateTime startTime = new DateTime(2013, 5, 24, 12, 00, 0, 0);
            DateTime endTime = new DateTime(2013, 5, 29, 12, 00, 0, 0);
            campaign = Campaign.Create(5661, 13985, 1, currentGeo, BidTypes.CPC, true, startTime, AdSourceTypes.MobiSage);
            campaign.EndDate = endTime;
            campaign.Name = "111";
            campaign.Status = AdStatus.Delete;
            campaign.IsMoreDaysBudget = 1;
            campaign.MoreDayBudget.Initialize(null);
            campaign.MoreDayBudget.Initialize("('2012-05-25 00:00:00','66.0000'),('2012-05-26 00:00:00','66.0000'),('2012-05-27 00:00:00','66.0000'),('2012-05-28 00:00:00','66.0000'),('2012-05-29 00:00:00','66.0000')");
            campaign.DailyBudget = 5;
            campaign.AdUrl = "http://iphone.myzaker.com/zaker/ad/adsage.php?u={udid}&t={timestamp}";
            campaign.AppleStore = "";

            campaignData = ConverterFactory.GetCampaignConverter().ConvertToNetworkObject(campaign);

            UpdateCampaignByIdProcessor processor = new UpdateCampaignByIdProcessor();
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new UpdateCampaignByIdRequestMessage { UserId = 4696, Data = campaignData };
            processor.Process(transaction);

            UpdateCampaignByIdResponseMessage rsqMsg = (UpdateCampaignByIdResponseMessage)transaction.Response;
        }


        [Test]
        public void GetCampaignById()
        {
            GetCampaignByIdProcessor processor = new GetCampaignByIdProcessor();
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new GetCampaignByIdRequestMessage { CampaignId = 15415, AccountId = 5709, UserId = 4696 };
            processor.Process(transaction);

            GetCampaignByIdResponseMessage rsqMsg = (GetCampaignByIdResponseMessage) transaction.Response;
        }

        [Test]
        public void Test()
        {
            IList<MoreDayBudgetItem> list = new List<MoreDayBudgetItem>(new[] { new MoreDayBudgetItem(), new MoreDayBudgetItem(), new MoreDayBudgetItem(), new MoreDayBudgetItem() });
            MoreDayBudgetItem[] item = new[] { new MoreDayBudgetItem(), new MoreDayBudgetItem(), new MoreDayBudgetItem(), new MoreDayBudgetItem() };
            Console.WriteLine(JsonConvert.SerializeObject(list));

            Console.WriteLine("Fast Json.");
            Console.WriteLine(fastJSON.JSON.Instance.ToJSON(list));
            Console.ReadLine();
        }

        [Test]
        public void Testjson()
        {
            string jsonString = "{ date: '05/20/2013', value: 2 },{ date: '05/21/2013', value: 2 }";
            List<MoreDayBudgetItem> items = JsonConvert.DeserializeObject<List<MoreDayBudgetItem>>(jsonString);

            Console.WriteLine(items);

        }

        [Test]
        public void UpdateCampaignStatus()
        {
            ulong[] campaginId = { 15075, 15048, 14589, 14426, 14424, 14422, 14420, 14419 };
            UpdateCampaignStatusProcessor processor = new UpdateCampaignStatusProcessor();
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new UpdateCampaignStatusRequestMessage { AccountId = 5730, CampaignId = campaginId, UserId = 4696 };
            processor.Process(transaction);
            UpdateCampaignStatusResponseMessage rsqMsg = (UpdateCampaignStatusResponseMessage)transaction.Response;
        }

        [Test]
        public void UpdateGroupStatus()
        {
            ulong[] GroupId = { 16001,16000,15926,15925,15921,15840,15839,15798,15730 };
            UpdateStatusByIdProcessor processor = new UpdateStatusByIdProcessor();
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new UpdateStatusByIdsRequestMessage { AccountId = 6179, Ids = GroupId, GoodsType = AdGoodsTypes.Group, Status = AdStatus.Pause };
            processor.Process(transaction);
            UpdateStatusByIdsResponseMessage rsqMsg = (UpdateStatusByIdsResponseMessage)transaction.Response;
        }

        [Test]
        public void GetCampaginAndAdGroupSimpleData()
        {
            GetCampaignAndAdGroupSimpleByIdProcessor processor = new GetCampaignAndAdGroupSimpleByIdProcessor();
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new GetCampaignAndAdGroupSimpleByIdRequestMessage { CampaignId = 14159,  UserId = 1234 ,GetGoodsType = GetGoodsTypes.Campaign};
            processor.Process(transaction);
            GetCampaignAndAdGroupSimpleByIdResponseMessage rsqMsg = (GetCampaignAndAdGroupSimpleByIdResponseMessage)transaction.Response;
        }

        [Test]
        public void GetImagesize()
        {
            GetImageSizeProcessor processor = new GetImageSizeProcessor();
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new GetImageSizesRequestMessage() { AdGroupId = 16393, MediaTypeId = 1,ImageType = ImageTypes.VerticalScreen};
            processor.Process(transaction);
            GetImageSizesResponseMessage rsqMsg = (GetImageSizesResponseMessage)transaction.Response;
        }

        #endregion
    }
}