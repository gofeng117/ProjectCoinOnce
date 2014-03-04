using System.Collections.Generic;
using System.Linq;
using MobiSage.AdsAPI.ABS.Components.AdComp.Processors;
using MobiSage.AdsAPI.ABS.ProtocolStack;
using MobiSage.AdsAPI.CommonFramework.DbAccess;
using MobiSage.AdsAPI.CommonFramework.Transactions;
using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.Domain.Business.Sizes;
using MobiSage.AdsAPI.Domain.Enums;
using MobiSage.AdsAPI.Domain.Midiator.Converters;
using MobiSage.AdsAPI.Domain.Service;
using NUnit.Framework;
using MobiSage.AdsAPI.Domain.ProtocolMetadata;

namespace MobiSage.AdsAPI.ABS.Test
{
    public class UnitTestGetImageSizeByIdProcessor
    {
        #region Members

        private GetImageSizeByMediaTypeIdProcessor _processor = new GetImageSizeByMediaTypeIdProcessor();

        #endregion

        #region Setup.

        [SetUp]
        public void Initialize()
        {
            DatabaseManager.Inject(new Database("Host=172.16.18.203;Database=AdAPI_Executor;User Id=root;password=1qaz;Charset=utf8;Convert Zero Datetime=true;Allow Zero Datetime=true;", 120), new Database("Host=172.16.18.203;Database=AdAPI_Executor;User Id=root;password=1qaz;Charset=utf8;Convert Zero Datetime=true;Allow Zero Datetime=true;", 120));
            ServiceManager.Instance.Initialize();
            Components.AdComp.Global.AdCreativeService = new AdCreativeService();
        }

        #endregion

        #region Methods

        [Test]
        public void GetImageSizeTest()
        {
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new GetImageSizesByMediaTypeIdRequestMessage { AdGroupId = 15142, MediaTypeId = 1 };
            _processor.Process(transaction);
            GetImageSizesByMediaTypeIdResponseMessage rspMsg = (GetImageSizesByMediaTypeIdResponseMessage)transaction.Response;
            Assert.IsNotNull(rspMsg);
            Assert.IsNotNull(rspMsg.Data);
            Assert.IsTrue(rspMsg.ErrorId == 0);
        }

        [Test]
        public void UpdateBatchCreatives()
        {
            UpdateBatchAdCreativesByIdsProcessor _processor = new UpdateBatchAdCreativesByIdsProcessor();
            ulong[] cids = { 30220, 30216, 30140 };
            BatchCreativeData batchCreative = new BatchCreativeData 
            { 
                DestinationUrl = "http://mws.adsage.com/mobisage/Test/1/52/20.png", 
                TrackUrl = "http://www.tracing-update-url.com" 
            };
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new UpdateBatchAdCreativesByIdsRequestMessage {UserId = 9310,AccountId = 6341, BatchEditType = BatchEditType.Mix, CreativeIds = cids, Data = batchCreative };
            _processor.Process(transaction);
            UpdateBatchAdCreativesByIdsResponseMessage rspMsg = (UpdateBatchAdCreativesByIdsResponseMessage)transaction.Response;
            Assert.IsNotNull(rspMsg);
            Assert.IsNotNull(rspMsg.ErrorId);
            Assert.IsTrue(rspMsg.ErrorId == 0);
        }

        [Test]
        public void UpdateBatchNewTextCreatives()
        {
            UpdateBatchAdNewTextCreativesByIdsProcessor _processor = new UpdateBatchAdNewTextCreativesByIdsProcessor();
            ulong[] cids = { 30220, 30216 };
            BatchNewTextCreativeData batchCreative = new BatchNewTextCreativeData { DestinationUrl = "http://mws.adsage.com/mobisage/Test/1/52/201112011109091689.png", TrackUrl = "http://www.tracing-update-url3333333333333333.com", CustomElement = "2^:^新文字2^#^3^:^1的份上^#^4^:^5" };
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new UpdateBatchNewTextCreativesByIdsRequestMessage { AccountId = 6341, CreativeIds = cids, Data = batchCreative };
            _processor.Process(transaction);
            UpdateBatchNewTextCreativesByIdsResponseMessage rspMsg = (UpdateBatchNewTextCreativesByIdsResponseMessage)transaction.Response;
            Assert.IsNotNull(rspMsg);
            Assert.IsNotNull(rspMsg.ErrorId);
            Assert.IsTrue(rspMsg.ErrorId == 0);
        }

        [Test]
        public void GetImageSizeTest2()
        {
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new GetImageSizesByMediaTypeIdRequestMessage { AdGroupId = 15142, MediaTypeId = 1 };
            _processor.Process(transaction);
            GetImageSizesByMediaTypeIdResponseMessage rspMsg = (GetImageSizesByMediaTypeIdResponseMessage)transaction.Response;
            Assert.IsNotNull(rspMsg);
            Assert.IsNotNull(rspMsg.Data);
            Assert.IsTrue(rspMsg.ErrorId == 0);

            List<AdSizeCategory> collection = new List<AdSizeCategory>(rspMsg.Data.Select(value => ConverterFactory.GetAdSizeCategoryConverter().ConvertToDomainObject(value)).ToList());
            IList<AdSizeCategory> result = collection;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        #endregion
    }
}