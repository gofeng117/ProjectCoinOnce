using System;
using MobiSage.AdsAPI.ABS.Components.AdComp.Processors;
using MobiSage.AdsAPI.ABS.ProtocolStack;
using MobiSage.AdsAPI.CommonFramework.Transactions;
using MobiSage.AdsAPI.Domain.Enums;
using MobiSage.AdsAPI.Domain.Midiator.Converters;
using MobiSage.AdsAPI.Domain.ProtocolMetadata;
using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.CommonFramework.DbAccess;
using MobiSage.AdsAPI.Domain.Service;
using NUnit.Framework;
using MobiSage.AdsAPI.Domain.Business.Actions;


namespace MobiSage.AdsAPI.ABS.Test
{
    internal class UnitTestCreateNewPushCreativeProcessor
    {
        private PushAdCreative _pushAdCreative;
        private AdPushCreativeData _adPushCreativeData;

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
        public void CreateNewPushCreative()
        {
            PushAdCreative creative = PushAdCreative.Create(15491UL, 16690UL, 0UL, 100UL, new AdAction(0U));
            creative.Name = "PushCreative测试";
            creative.AdType = AdTypes.Text;
            creative.TargetUrl = "http://www.adsage.com?channel=apn";
            creative.Content = "{\"title\":\"标题\", \"text1\":\"第一行文字\",\"text2\" : \"第二行文字\" , \"icon\":\"http://xxx.png\", \"displayurl\":\"http://www.adsage.com\",}";
            DateTime dateTime = new DateTime(2013,8,31,23,59,59);
            creative.ExpireDate = dateTime;
            creative.Monitor = "{\"imp\" : [\"http://trc.adsage.com/i.gif\",\"http://trc.adsage.com/i2.gif\"],\"click\" : [\"http://trc.adsage.com/i.gif\",\"http://trc.adsage.com/i2.gif\"],\"imp_lpg\":[],\"cli_lpg\":[],\"down\":[],\"install\":[]}";
            creative.Status = AdStatus.Launching;
            creative.Ext = "{\"template\" : \"http://xxx.xml\",\"displaytype\" : 1,\"cache_item\" : [\"ext.template\", \"content.icon\"], \"layouttype\" : 2,\"preload\" : 1,\"packagename\" : \"com.soqu.demo\",\"isautoopen\" : 1,\"impfilter\" : 1,\"priority\":210,}";
            creative.Size.ImageSizeId = 0;

            _adPushCreativeData = ConverterFactory.GetAdPushCreativeConverter().ConvertToNetworkObject(creative);

            CreateNewAdPushCreativeProcessor processor = new CreateNewAdPushCreativeProcessor();
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new CreateNewAdPushCreativeRequestMessage { UserId = 9064, AccountId = 6340, Data = _adPushCreativeData };
            processor.Process(transaction);

            CreateNewAdPushCreativeResponseMessage rspMsg = (CreateNewAdPushCreativeResponseMessage)transaction.Response;
        }

        [Test]
        public void UpdatePushCreative()
        {
            PushAdCreative creative = PushAdCreative.Create(15491UL, 16690UL, 30654UL, 100UL, new AdAction(0U));
            creative.Name = "PushCreative测试1111111111111";
            creative.AdType = AdTypes.Text;
            creative.TargetUrl = "http://www.adsage.com?channel=apn";
            creative.Content = "{\"title\":\"标题\", \"text1\":\"第一行文字\",\"text2\" : \"第二行文字\" , \"icon\":\"http://xxx.png\", \"displayurl\":\"http://www.adsage.com\",}";
            DateTime dateTime = new DateTime(2013, 8, 31, 23, 59, 59);
            creative.ExpireDate = dateTime;
            creative.Monitor = "{\"imp\" : [\"http://trc.adsage.com/i.gif\",\"http://trc.adsage.com/i2.gif\"],\"click\" : [\"http://trc.adsage.com/i.gif\",\"http://trc.adsage.com/i2.gif\"],\"imp_lpg\":[],\"cli_lpg\":[],\"down\":[],\"install\":[]}";
            creative.Status = AdStatus.Launching;
            creative.Ext = "{\"template\" : \"http://xxx.xml\",\"displaytype\" : 1,\"cache_item\" : [\"ext.template\", \"content.icon\"], \"layouttype\" : 2,\"preload\" : 1,\"packagename\" : \"com.soqu.demo\",\"isautoopen\" : 1,\"impfilter\" : 1,\"priority\":210,}";
            creative.Size.ImageSizeId = 0;

            _adPushCreativeData = ConverterFactory.GetAdPushCreativeConverter().ConvertToNetworkObject(creative);

            UpdateAdPushCreativeByIdProcessor processor = new UpdateAdPushCreativeByIdProcessor();
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new UpdateAdPushCreativeByIdRequestMessage { UserId = 9083, AccountId = 6340, Data = _adPushCreativeData };
            processor.Process(transaction);

            UpdateAdPushCreativeByIdResponseMessage rspMsg = (UpdateAdPushCreativeByIdResponseMessage)transaction.Response;
        }

        [Test]
        public void GetPushCreativeById()
        {
            GetAdPushCreativeByIdProcessor processor = new GetAdPushCreativeByIdProcessor();
            FakedBusinessTransaction transaction = new FakedBusinessTransaction();
            transaction.Request = new GetAdPushCreativeByIdRequestMessage {AdCreativeId = 29688,AccountId = 6179,UserId = 9083};
            processor.Process(transaction);

            GetAdPushCreativeByIdResponseMessage rspMsg = (GetAdPushCreativeByIdResponseMessage)transaction.Response;
        }

        #endregion
    }
}
