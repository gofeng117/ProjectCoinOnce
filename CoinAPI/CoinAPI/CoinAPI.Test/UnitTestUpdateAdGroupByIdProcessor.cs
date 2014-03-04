//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using KJFramework.Net.Transaction;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using MobiSage.AdsAPI.ABS.Components.AdvertismentComponent;
//using MobiSage.AdsAPI.ABS.Components.AdvertismentComponent.Processors;
//using MobiSage.AdsAPI.ABS.ProtocolStack;
//using MobiSage.AdsAPI.CommonFramework.Core;
//using MobiSage.AdsAPI.CommonFramework.DbAccess;
//using MobiSage.AdsAPI.CommonFramework.Enums;
//using MobiSage.AdsAPI.CommonFramework.Metadata;
//using Global = MobiSage.AdsAPI.ABS.Components.AdvertismentComponent.Global;

//namespace MobiSage.AdsAPI.ABS.Test
//{
//    [TestClass]
//    public class UnitTestUpdateAdGroupByIdProcessor
//    {
//        private static bool firstFlg;
//        private readonly AdGroup adGroup;
//        private readonly BusinessMessageTransaction bs;

//        public UnitTestUpdateAdGroupByIdProcessor()
//        {
//            bs = new BusinessMessageTransaction { };
//            adGroup = new AdGroup();

//            if (firstFlg)
//                return;

//            //注册processors
//            SystemWorker.Instance.Initialize(ServiceRoles.ABS);
//            //配置数据库连接字符串
//            Global.DbMySql =
//                new Database(SystemWorker.Instance.ConfigurationProxy.GetField(ServiceRoles.ABS, Global.MySql),
//                             Global.CommandTimeOut);
//            Global.DbMySqlReadOnly =
//                new Database(SystemWorker.Instance.ConfigurationProxy.GetField(ServiceRoles.ABS, Global.MySqlReadOnly),
//                             Global.CommandTimeOut);
//            Global.DbMySqlAdConversions =
//                new Database(
//                    SystemWorker.Instance.ConfigurationProxy.GetField(ServiceRoles.ABS, Global.MySqlAdConversions),
//                    Global.CommandTimeOut);
//            Global.DbOaInfoExcutor =
//                new Database(SystemWorker.Instance.ConfigurationProxy.GetField(ServiceRoles.ABS, Global.OaInfoExcutor),
//                             Global.CommandTimeOut);

//            var deviceCategory =
//                SystemWorker.Instance.ConfigurationProxy.GetField(ServiceRoles.ABS,
//                                                                  Global.DeviceCategoryIphoneAndTouchAndIpad).Split('|');
//            string[] deviceCategoryValue = deviceCategory[1].Split(',');
//            IList<string> deviceCategoryList = deviceCategoryValue.ToList();

//            Global.DeviceCategory.Add(deviceCategory[0], deviceCategoryList);
//            deviceCategory =
//                SystemWorker.Instance.ConfigurationProxy.GetField(ServiceRoles.ABS, Global.DeviceCategoryIphoneAndTouch)
//                    .Split('|');
//            deviceCategoryValue = deviceCategory[1].Split(',');
//            deviceCategoryList = deviceCategoryValue.ToList();
//            Global.DeviceCategory.Add(deviceCategory[0], deviceCategoryList);

//            deviceCategory = SystemWorker.Instance.ConfigurationProxy.GetField(ServiceRoles.ABS,
//                                                                               Global.
//                                                                                   DeviceCategoryIpad).Split('|');
//            deviceCategoryValue = deviceCategory[1].Split(',');
//            deviceCategoryList = deviceCategoryValue.ToList();
//            Global.DeviceCategory.Add(deviceCategory[0], deviceCategoryList);
//            deviceCategory = SystemWorker.Instance.ConfigurationProxy.GetField(ServiceRoles.ABS,
//                                                                               Global.
//                                                                                   DeviceCategoryAndroid).Split('|');
//            deviceCategoryValue = deviceCategory[1].Split(',');
//            deviceCategoryList = deviceCategoryValue.ToList();
//            Global.DeviceCategory.Add(deviceCategory[0], deviceCategoryList);

//            //权重初始化
//            Global.AdGroupWeight = SystemWorker.Instance.ConfigurationProxy.GetField(ServiceRoles.ABS,
//                                                                                     Global.AdGroupWeightConfigKey);
//            Global.AdCreativeWeight = SystemWorker.Instance.ConfigurationProxy.GetField(ServiceRoles.ABS,
//                                                                                     Global.AdCreativeWeightConfigKey);
//            //开发者单价初始化
//            Global.AdGroupAcBidPriceIos =
//                double.Parse(SystemWorker.Instance.ConfigurationProxy.GetField(ServiceRoles.ABS,
//                                                                               Global.AdGroupAcBidPriceIosConfigKey));
//            Global.AdGroupAcBidPriceAndroid =
//                double.Parse(SystemWorker.Instance.ConfigurationProxy.GetField(ServiceRoles.ABS,
//                                                                               Global.AdGroupAcBidPriceAndroidConfigKey));

//            firstFlg = true;

//        }

//        /// <summary>
//        /// 正确的分支
//        /// </summary>
//        [TestMethod]
//        public void TestMethod1()
//        {
//            adGroup.GroupID = 14011;
//            adGroup.CampaignID = 12867;
//            adGroup.Name = "22222222222OUPj";
//            adGroup.BidPrice = 5.0m;
//            adGroup.MaxImpressionsEveryDay = 0;
//            adGroup.DeviceCategory = "2,3,4";
//            adGroup.AdContentID = 2;
//            UpdateAdGroupByIdRequestMessage ResMsg = new UpdateAdGroupByIdRequestMessage();

//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 2087;
//            ResMsg.AdGroup = adGroup;
//            bs.Request = ResMsg;
//            UpdateAdGroupByIdProcessor updateAdGroupByIdProcessor = new UpdateAdGroupByIdProcessor();

//            try
//            {
//                updateAdGroupByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdGroupByIdResponseMessage rspMsg = (UpdateAdGroupByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.Ok);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>AdGroup不能为null</remarks>
//        [TestMethod]
//        public void TestMethod2()
//        {
            
//            UpdateAdGroupByIdRequestMessage ResMsg = new UpdateAdGroupByIdRequestMessage();
//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 2087;
//            bs.Request = ResMsg;
//            UpdateAdGroupByIdProcessor updateAdGroupByIdProcessor = new UpdateAdGroupByIdProcessor();

//            try
//            {
//                updateAdGroupByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdGroupByIdResponseMessage rspMsg = (UpdateAdGroupByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjError);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>UserId大于0</remarks>
//        [TestMethod]
//        public void TestMethod3()
//        {
//            adGroup.GroupID = 13853;
//            adGroup.CampaignID = 12735;
//            adGroup.Name = "sp调用测试——UpdateAdGroup";
//            adGroup.BidPrice = 55;
//            adGroup.MaxImpressionsEveryDay = 66;
//            adGroup.ITunesAppID = 77;
//            adGroup.DeviceCategory = "88";
//            adGroup.AdContentID = 2;
//            UpdateAdGroupByIdRequestMessage ResMsg = new UpdateAdGroupByIdRequestMessage();

//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 0;
//            ResMsg.AdGroup = adGroup;
//            bs.Request = ResMsg;
//            UpdateAdGroupByIdProcessor updateAdGroupByIdProcessor = new UpdateAdGroupByIdProcessor();

//            try
//            {
//                updateAdGroupByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdGroupByIdResponseMessage rspMsg = (UpdateAdGroupByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjError);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>AccountId=0</remarks>
//        [TestMethod]
//        public void TestMethod4()
//        {
//            adGroup.GroupID = 13853;
//            adGroup.CampaignID = 12735;
//            adGroup.Name = "sp调用测试——UpdateAdGroup";
//            adGroup.BidPrice = 55;
//            adGroup.MaxImpressionsEveryDay = 66;
//            adGroup.ITunesAppID = 77;
//            adGroup.DeviceCategory = "88";
//            adGroup.AdContentID = 2;
//            UpdateAdGroupByIdRequestMessage ResMsg = new UpdateAdGroupByIdRequestMessage();

//            ResMsg.AccountId = 0;
//            ResMsg.UserId = 2087;
//            ResMsg.AdGroup = adGroup;
//            bs.Request = ResMsg;
//            UpdateAdGroupByIdProcessor updateAdGroupByIdProcessor = new UpdateAdGroupByIdProcessor();

//            try
//            {
//                updateAdGroupByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdGroupByIdResponseMessage rspMsg = (UpdateAdGroupByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjError);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>CampaignID等于0</remarks>
//        [TestMethod]
//        public void TestMethod5()
//        {
//            adGroup.GroupID = 13853;
//            adGroup.CampaignID = 0;
//            adGroup.Name = "sp调用测试——UpdateAdGroup";
//            adGroup.BidPrice = 55;
//            adGroup.MaxImpressionsEveryDay = 66;
//            adGroup.ITunesAppID = 77;
//            adGroup.DeviceCategory = "88";
//            adGroup.AdContentID = 2;
//            UpdateAdGroupByIdRequestMessage ResMsg = new UpdateAdGroupByIdRequestMessage();

//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 2087;
//            ResMsg.AdGroup = adGroup;
//            bs.Request = ResMsg;
//            UpdateAdGroupByIdProcessor updateAdGroupByIdProcessor = new UpdateAdGroupByIdProcessor();

//            try
//            {
//                updateAdGroupByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdGroupByIdResponseMessage rspMsg = (UpdateAdGroupByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjError);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>Name不能为空</remarks>
//        [TestMethod]
//        public void TestMethod6()
//        {
//            adGroup.GroupID = 13853;
//            adGroup.CampaignID = 12735;
//            adGroup.Name = "";
//            adGroup.BidPrice = 55;
//            adGroup.MaxImpressionsEveryDay = 66;
//            adGroup.ITunesAppID = 77;
//            adGroup.DeviceCategory = "88";
//            adGroup.AdContentID = 2;
//            UpdateAdGroupByIdRequestMessage ResMsg = new UpdateAdGroupByIdRequestMessage();

//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 2087;
//            ResMsg.AdGroup = adGroup;
//            bs.Request = ResMsg;
//            UpdateAdGroupByIdProcessor updateAdGroupByIdProcessor = new UpdateAdGroupByIdProcessor();

//            try
//            {
//                updateAdGroupByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdGroupByIdResponseMessage rspMsg = (UpdateAdGroupByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjError);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>Name有200字符的最大长度限制</remarks>
//        [TestMethod]
//        public void TestMethod7()
//        {
//            adGroup.GroupID = 13853;
//            adGroup.CampaignID = 12735;
//            adGroup.Name = "调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试"
//                                   + "调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试"
//                                   + "调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试"
//                                   + "调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试"
//                                   + "调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试"
//                                   + "调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试";
//            adGroup.BidPrice = 55;
//            adGroup.MaxImpressionsEveryDay = 66;
//            adGroup.ITunesAppID = 77;
//            adGroup.DeviceCategory = "88";
//            adGroup.AdContentID = 2;
//            UpdateAdGroupByIdRequestMessage ResMsg = new UpdateAdGroupByIdRequestMessage();

//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 2087;
//            ResMsg.AdGroup = adGroup;
//            bs.Request = ResMsg;
//            UpdateAdGroupByIdProcessor updateAdGroupByIdProcessor = new UpdateAdGroupByIdProcessor();

//            try
//            {
//                updateAdGroupByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdGroupByIdResponseMessage rspMsg = (UpdateAdGroupByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjError);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>AdContentID只开放2,3,4,5</remarks>
//        [TestMethod]
//        public void TestMethod8()
//        {
//            adGroup.GroupID = 13853;
//            adGroup.CampaignID = 12735;
//            adGroup.Name = "sp调用测试——UpdateAdGroup";
//            adGroup.BidPrice = 55;
//            adGroup.MaxImpressionsEveryDay = 66;
//            adGroup.ITunesAppID = 77;
//            adGroup.DeviceCategory = "88";
//            adGroup.AdContentID = 0;
//            UpdateAdGroupByIdRequestMessage ResMsg = new UpdateAdGroupByIdRequestMessage();

//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 2087;
//            ResMsg.AdGroup = adGroup;
//            bs.Request = ResMsg;
//            UpdateAdGroupByIdProcessor updateAdGroupByIdProcessor = new UpdateAdGroupByIdProcessor();

//            try
//            {
//                updateAdGroupByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdGroupByIdResponseMessage rspMsg = (UpdateAdGroupByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjIsNotSupported);
//        }

//    }
//}
