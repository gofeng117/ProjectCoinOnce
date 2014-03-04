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
    internal class UnitTestCreateNewVideoCreativeProcessor
    {
        private VideoAdCreative _videoAdCreative;
        private AdVideoCreativeData _adVideoCreativeData;

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
        }

        #endregion

        #region method

        [Test]
        public void CreateNewVideoCreative()
        {
            VideoAdCreative creative = VideoAdCreative.Create(13337UL, 15135UL, 0UL, 100UL, new AdAction(0U));
            creative.Name = "12";
            creative.Action.Id = 2;
            creative.Status = AdStatus.Launching;
            creative.VideoPreviewImageUrl = "http://172.16.18.6/conversion/15135/c7751dd234cd4414b8e9dc704c4e2cc7.png";
            creative.VideoUrl = "http://172.16.18.6/conversion/15135/201306030429304055.mp4";
            //creative.DestinationUrl = creative.VideoUrl;
            creative.Size.ImageSizeId = 109;

            _adVideoCreativeData = ConverterFactory.GetAdVideoCreativeConverter().ConvertToNetworkObject(creative);

            CreateNewAdVideoCreativeProcessor processor = new CreateNewAdVideoCreativeProcessor();
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new CreateNewAdVideoCreativeRequestMessage{ UserId = 8973, AccountId = 5658, Data = _adVideoCreativeData };
            processor.Process(transaction);

            CreateNewAdVideoCreativeResponseMessage rspMsg = (CreateNewAdVideoCreativeResponseMessage)transaction.Response;
        }

        [Test]
        public void UpdateVideoCreative()
        {
            VideoAdCreative creative = VideoAdCreative.Create(13337UL, 15135UL, 28157UL, 100UL, new AdAction(0U));
            creative.Name = "update12";
            creative.Action.Id = 2;
            creative.Status = AdStatus.Launching;
            creative.VideoPreviewImageUrl = "http://172.16.18.6/conversion/15135/c7751dd234cd4414b8e9dc704c4e2cc7.png";
            creative.VideoUrl = "http://172.16.18.6/conversion/15135/201306030429304055.mp4";
            creative.DestinationUrl = creative.VideoUrl;
            creative.Size.ImageSizeId = 109;

            _adVideoCreativeData = ConverterFactory.GetAdVideoCreativeConverter().ConvertToNetworkObject(creative);

            UpdateAdVideoCreativeByIdProcessor processor = new UpdateAdVideoCreativeByIdProcessor();
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new UpdateAdVideoCreativeByIdRequestMessage { UserId = 8973, AccountId = 5658, Data = _adVideoCreativeData };
            processor.Process(transaction);

            UpdateAdVideoCreativeByIdResponseMessage rspMsg = (UpdateAdVideoCreativeByIdResponseMessage)transaction.Response;
        }

        [Test]
        public void GetAdVideoById()
        {
            GetAdVideoCreativeByIdProcessor processor = new GetAdVideoCreativeByIdProcessor();
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new GetAdVideoCreativeByIdRequestMessage { AdCreativeId = 28157UL, AccountId = 5661, UserId = 4696 };
            processor.Process(transaction);

            GetAdVideoCreativeByIdResponseMessage rsqMsg = (GetAdVideoCreativeByIdResponseMessage)transaction.Response;
        }

        #endregion
    }
}
