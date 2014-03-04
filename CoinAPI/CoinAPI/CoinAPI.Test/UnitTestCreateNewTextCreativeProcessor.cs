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
using MobiSage.AdsAPI.Domain.Business.Actions;


namespace MobiSage.AdsAPI.ABS.Test
{
    internal class UnitTestCreateNewTextCreativeProcessor
    {
        private TextAdCreative _textAdCreative;
        private AdTextCreativeData _adTextCreativeData                            ;

        #region Setup.

        [SetUp]
        public void Initialize()
        {
            DatabaseManager.Inject(
                new Database(
                    "Host=172.16.18.203;Database=AdAPI_Executor;User Id=root;password=1qaz;Charset=utf8;Convert Zero Datetime=true;Allow Zero Datetime=true;",
                    120),
                new Database(
                    "Host=172.16.18.203;Database=AdAPI_Executor;User Id=root;password=1qaz;Charset=utf8;Convert Zero Datetime=true;Allow Zero Datetime=true;",
                    120));
            ServiceManager.Instance.Initialize();
            Components.AdComp.Global.AdCreativeService = new AdCreativeService();
            Components.AdComp.Global.AdGroupService = new AdGroupService();
        }

        #endregion

        #region method

        [Test]
        public void CreateNewTextCreative()
        {
            TextAdCreative creative = TextAdCreative.Create(14788UL, 16165UL, 0UL, 100UL, new AdAction(3U));
            creative.Name = "12";
            creative.Action.Id = 2;
            creative.Status = AdStatus.Launching;
            creative.Size.ImageSizeId = 30;
            creative.LogoImageUrl = "http://172.16.18.6/conversion/15135/c7751dd234cd4414b8e9dc704c4e2cc7.png";
            creative.Title = "12";
            creative.Desc1 = "12";
            creative.Desc2 = "12";
            creative.DisplayUrl = "www.baidu.com";
            creative.DestinationUrl = "www.google.com";
            creative.CustomElement.AddElementItem(1, "123");
            creative.CustomElement.AddElementItem(5, "123");
            creative.CustomElement.AddElementItem(8, "123");

            _adTextCreativeData = ConverterFactory.GetAdTextCreativeConverter().ConvertToNetworkObject(creative);

            CreateNewAdTextCreativeProcessor processor = new CreateNewAdTextCreativeProcessor();
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new CreateNewAdTextCreativeRequestMessage { UserId = 9062, AccountId = 5715, Data = _adTextCreativeData };
            processor.Process(transaction);

            CreateNewAdTextCreativeResponseMessage rspMsg = (CreateNewAdTextCreativeResponseMessage)transaction.Response;
        }

        [Test]
        public void UpdateTextCreative()
        {
            TextAdCreative creative = TextAdCreative.Create(15073UL, 16244UL, 29834UL, 100UL, new AdAction(0U));
            creative.Name = "12update";
            creative.Action.Id = 2;
            creative.Status = AdStatus.Launching;
            creative.Size.ImageSizeId = 30;
            creative.LogoImageUrl = "http://172.16.18.6/conversion/15135/c7751dd234cd4414b8e9dc704c4e2cc7.png";
            creative.Title = "12";
            creative.Desc1 = "12";
            creative.Desc2 = "12";
            creative.DisplayUrl = "www.baidu.com.cn";
            creative.DestinationUrl = "www.google.com.cn";

            _adTextCreativeData = ConverterFactory.GetAdTextCreativeConverter().ConvertToNetworkObject(creative);

            UpdateAdTextCreativeByIdProcessor processor = new UpdateAdTextCreativeByIdProcessor();
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new UpdateAdTextCreativeByIdRequestMessage { UserId = 9083, AccountId = 6179, Data = _adTextCreativeData };
            processor.Process(transaction);

            UpdateAdTextCreativeByIdResponseMessage rspMsg = (UpdateAdTextCreativeByIdResponseMessage)transaction.Response;
        }

        [Test]
        public void GetTextCreativeById()
        {
            GetAdTextCreativeByIdProcessor processor = new GetAdTextCreativeByIdProcessor();
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new GetAdTextCreativeByIdRequestMessage { UserId = 9083, AccountId = 6179, AdCreativeId = 29834 };
            processor.Process(transaction);
            GetAdTextCreativeByIdResponseMessage rsqMsg = (GetAdTextCreativeByIdResponseMessage)transaction.Response;
        }

        #endregion
    }
}
