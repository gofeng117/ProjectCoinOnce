using System;
using MobiSage.AdsAPI.CommonFramework.Enums;
using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.Domain.Business.Targeting;
using MobiSage.AdsAPI.Domain.Enums;
using MobiSage.AdsAPI.Domain.Midiator.Converters;
using MobiSage.AdsAPI.Domain.Results;
using MobiSage.AdsAPI.ProxyLib;
using NUnit.Framework;

namespace MobiSage.AdsAPI.ABS.NUintTest
{
    public class BASTest
    {
        #region Members

        private static AdsAPIProxy _proxy;
        private string token;

        #endregion

        #region Methods

        [SetUp]
        public void Setup()
        {
            ServiceInit.Init(ServiceRoles.APIProxy);
            Exception exception = null;
            _proxy = new AdsAPIProxy();
            bool executeResult = _proxy.Login("luohu", "000000");
            token = _proxy.GetUCUser().Token;
            if (!executeResult)
                exception = new Exception("Login failed.");
            if (exception != null) throw exception;
        }

        [NUnit.Framework.Test]
        public void AddGroup_Create_SucceedTest()
        {
            Exception exception = null;
            IExecuteResult executeResult;
            
            #region Construct Para

            DeviceTargeting target = new DeviceTargeting("1,2,3", null, "1,1,10", null, null);
            var fren = new FrequencyCapTargeting();
            AdGroup group = AdGroup.Create(0, 14379UL, new PricePolicy(11M), 100UL, new AdTargeting(target,
                       fren,
                        new TimeSpanTargeting()),
                        new DeviceNetwork("3,4"));
            group.BidPrice = 11M;
            group.ActionType = AdActionTypes.OpenWebInProgram;
            group.DeviceCategory = "2,3,4,5,6,7,8";
            group.Name = "fdf";
            group.NetworkTypes = new DeviceNetwork("3,4");
            group.Status = AdStatus.Launching;
            group.CPAPrice = 12;
            group.MaxClicks = 1;
            group.MaxImpressions = 2;
            group.MaxPerUserImpressions = 3;
            group.ITunesAppId = null;
            group.MediaTypeId = 1;
            group.ITunesAppCategoryId = 0U;
            group.ContentType = new AdContentType(7U, null);
            group.Targetings.TagTargeting.Add(UserTagDirectionTypes.Forward, 3);
            group.Targetings.TagTargeting.Add(UserTagDirectionTypes.Forward, 4);
            group.Targetings.TagTargeting.Add(UserTagDirectionTypes.Backward, 2);

            #endregion

            executeResult = _proxy.CreateAdGroup(5713, @group);
            if (executeResult.State != ExecuteResults.Succeed)
            {
                Console.WriteLine(executeResult.Error);
                exception = new Exception(executeResult.Error);
            }
            if (exception != null) throw exception;
            Console.WriteLine("#New AdGroup Id: " + executeResult.GetResult<ulong>());
        }

        [NUnit.Framework.Test]
        public void AddGroup_Update_SucceedTest()
        {
            Exception exception = null;
            IExecuteResult executeResult = null;
            DeviceTargeting target = new DeviceTargeting("", null, "1,1,10", null, null);
            var fren = new FrequencyCapTargeting();
            AdGroup group = AdGroup.Create(15386UL, 14379UL, new PricePolicy(11M), 100UL, new AdTargeting(target,
                       fren,
                        new TimeSpanTargeting()),
                        new DeviceNetwork("3,4"));
            group.BidPrice = 11M;
            group.ActionType = AdActionTypes.RedirectToWebPage;
            group.DeviceCategory = "2,3,4,5";
            group.Name = "更新过后的Group Name";
            group.NetworkTypes = new DeviceNetwork("3,4");
            group.Status = AdStatus.Audit;
            group.CPAPrice = 12;
            group.MaxClicks = 7;
            group.MaxImpressions =62;
            group.MaxPerUserImpressions = 5;
            group.ITunesAppId = null;
            group.MediaTypeId = 1;
            group.ITunesAppCategoryId = 1U;
            group.ContentType = new AdContentType(7U, null);
            group.Targetings.TagTargeting.Remove(UserTagDirectionTypes.Backward, 2);

            executeResult = _proxy.UpdateAdGroup(5713, @group);
            if (executeResult.State != ExecuteResults.Succeed)
            {
                Console.WriteLine(executeResult.Error);
                exception = new Exception(executeResult.Error);
            }
            if (exception != null) throw exception;
            Console.WriteLine("#Update AdGroup Result: " + executeResult.GetResult<bool>());
        }
        
        [NUnit.Framework.Test]
        public void AddGroup_GetById_SucceedTest()
        {
            Exception exception = null;
            IExecuteResult executeResult = null;
            executeResult = _proxy.GetAdGroupById(5658UL, 15013UL);
            if (executeResult.State != ExecuteResults.Succeed)
            {
                Console.WriteLine(executeResult.Error);
                exception = new Exception(executeResult.Error);
            }
            if (exception != null) throw exception;
            Console.WriteLine(ConverterFactory.GetAdGroupConverter().ConvertToNetworkObject(executeResult.GetResult<AdGroup>()));
        }

        [NUnit.Framework.Test]
        public void Campaign_Create_SucceedTest()
        {
            Exception exception = null;
            IExecuteResult executeResult;

            #region Construct Para

            Campaign campaign = Campaign.Create(5658UL, 0U, 3, new GeoTargeting("1,2,3,4,5,6,7,8"), BidTypes.CPA, true, new DateTime(2013, 4, 10),AdSourceTypes.MobiSage);
            campaign.Name = "AdsAPI创建的Campain";
            campaign.DailyBudget = 139;
            campaign.Status = AdStatus.Launching;

            #endregion

            executeResult = _proxy.CreateCampaign(5658UL, campaign);
            if (executeResult.State != ExecuteResults.Succeed)
            {
                Console.WriteLine(executeResult.Error);
                exception = new Exception(executeResult.Error);
            }
            if (exception != null) throw exception;
            Console.WriteLine("#New Campaign Id: " + executeResult.GetResult<ulong>());
        }

        [NUnit.Framework.Test]
        public void Campaign_Update_SucceedTest()
        {
            Exception exception = null;
            IExecuteResult executeResult = null;

            #region Construct Para

            Campaign campaign = Campaign.Create(5658UL, 0U, 3, new GeoTargeting("1,2,3,4,5,6,7,8"), BidTypes.CPA, true, new DateTime(2013, 4, 10),AdSourceTypes.MobiSage);
            campaign.Name = "AdsAPI创建的Campain";
            campaign.DailyBudget = 139;
            campaign.Status = AdStatus.Launching;

            #endregion

            //create campaign.
            executeResult = _proxy.CreateCampaign(5658UL, campaign);
            if (executeResult.State != ExecuteResults.Succeed)
            {
                Console.WriteLine(executeResult.Error);
                exception = new Exception(executeResult.Error);
            }
            if (exception != null) throw exception;
            Console.WriteLine("#New Campaign Id: " + executeResult.GetResult<ulong>());
            campaign = Campaign.Create(5658UL, executeResult.GetResult<ulong>(), 3, new GeoTargeting("1,2,3,4,5,6,7,8,0,123,123,"), BidTypes.CPA, true, new DateTime(2013, 4, 10),AdSourceTypes.MobiSage);
            campaign.Name = "AdsAPI更新的Campain";
            campaign.DailyBudget = 678;
            campaign.EndDate = new DateTime(2013, 4, 26);
            campaign.Status = AdStatus.Pause;
            //create campaign.
            executeResult = _proxy.UpdateCampaign(5658UL, campaign);
            if (executeResult.State != ExecuteResults.Succeed)
            {
                Console.WriteLine(executeResult.Error);
                exception = new Exception(executeResult.Error);
            }
            if (exception != null) throw exception;
            Console.WriteLine("#Update AdGroup Result: " + executeResult.GetResult<bool>());
        }

        [NUnit.Framework.Test]
        public void Campaign_GetById_SucceedTest()
        {
            Exception exception = null;
            IExecuteResult executeResult;

            #region Construct Para

            Campaign campaign = Campaign.Create(5658UL, 0U, 1, new GeoTargeting("1,2,3,4,5,6,7,8"), BidTypes.CPA, true, new DateTime(2013, 4, 10),AdSourceTypes.MobiSage);
            campaign.Name = "AdsAPI创建的Campain";
            campaign.DailyBudget = 139;
            campaign.Status = AdStatus.Launching;

            #endregion

            executeResult = _proxy.CreateCampaign(5658UL, campaign);
            if (executeResult.State != ExecuteResults.Succeed)
            {
                Console.WriteLine(executeResult.Error);
                exception = new Exception(executeResult.Error);
            }
            if (exception != null) throw exception;
            Console.WriteLine("#New Campaign Id: " + executeResult.GetResult<ulong>());
            Console.WriteLine("Get campaign by id...");
            executeResult = _proxy.GetCampaignById(5658UL, executeResult.GetResult<ulong>());
            if (executeResult.State != ExecuteResults.Succeed)
            {
                Console.WriteLine(executeResult.Error);
                exception = new Exception(executeResult.Error);
            }
            if (exception != null) throw exception;
            Console.WriteLine(ConverterFactory.GetCampaignConverter().ConvertToNetworkObject(executeResult.GetResult<Campaign>()));
        }

        [NUnit.Framework.Test]
        public void Campaign_GetCampaigns_SucceedTest()
        {
            Exception exception = null;
            IExecuteResult executeResult;

            executeResult = _proxy.GetCampaigns(5658UL);
            if (executeResult.State != ExecuteResults.Succeed)
            {
                Console.WriteLine(executeResult.Error);
                exception = new Exception(executeResult.Error);
            }
            if (exception != null) throw exception;
            Campaign[] campaigns = executeResult.GetResult<Campaign[]>();
            if (campaigns != null)
            {
                Console.WriteLine("#Get Campaigns Count: " + campaigns.Length);
                foreach (Campaign campaign in executeResult.GetResult<Campaign[]>())
                    Console.WriteLine(ConverterFactory.GetCampaignConverter().ConvertToNetworkObject(campaign));
            }
        }

        [Test]
        public void Logout_Success()
        {
            Console.WriteLine(_proxy.Logout());
        }

        #endregion
    }
}
