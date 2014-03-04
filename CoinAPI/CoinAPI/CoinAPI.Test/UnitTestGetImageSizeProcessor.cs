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

namespace MobiSage.AdsAPI.ABS.Test
{
    public class UnitTestGetImageSizeProcessor
    {
        #region Members

        private GetImageSizeProcessor _processor = new GetImageSizeProcessor();

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
            transaction.Request = new GetImageSizesRequestMessage { AdGroupId = 15142, MediaTypeId = 1, ImageType = ImageTypes.Single };
            _processor.Process(transaction);
            GetImageSizesResponseMessage rspMsg =  (GetImageSizesResponseMessage) transaction.Response;
            Assert.IsNotNull(rspMsg);
            Assert.IsNotNull(rspMsg.Data);
            Assert.IsTrue(rspMsg.ErrorId == 0);
        }

        [Test]
        public void GetImageSizeTest2()
        {
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new GetImageSizesRequestMessage { AdGroupId = 15142, MediaTypeId = 1, ImageType = ImageTypes.Single };
            _processor.Process(transaction);
            GetImageSizesResponseMessage rspMsg = (GetImageSizesResponseMessage)transaction.Response;
            Assert.IsNotNull(rspMsg);
            Assert.IsNotNull(rspMsg.Data);
            Assert.IsTrue(rspMsg.ErrorId == 0);

            ImageSizeCollection collection = new ImageSizeCollection(rspMsg.Data.Select(value => ConverterFactory.GetAppTypeLevelConverter().ConvertToDomainObject(value)).ToArray());
            IList<AdSize> result = collection.GetSizeGroupByMaxSize(48, 320);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        #endregion
    }
}