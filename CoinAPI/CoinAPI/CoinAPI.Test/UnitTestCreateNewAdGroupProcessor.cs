//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
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
//    public class UnitTestCreateNewAdGroupProcessor
//    {
//        private static bool firstFlg;
//        private BusinessMessageTransaction bs;
//        private AdGroup adGroup;

//        public UnitTestCreateNewAdGroupProcessor()
//        {
//            adGroup = new AdGroup();
//            bs = new BusinessMessageTransaction { };

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
//        /// <remarks>错误类型SystemErrors.Ok</remarks>
//        [TestMethod]
//        public void TestMethod1_1()
//        {
//            adGroup.CampaignID = 12735;
//            adGroup.BidPrice = 2m;
//            adGroup.AdContentID = 2;
//            adGroup.DeviceCategory = "2,3,4,5,6";
//            adGroup.Name = "AdGroupApiTest";
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage
//                             {AccountId = 3659, UserId = 2087, CampaignId = 12735, AdGroup = adGroup};
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.Ok, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 正确的分支
//        /// </summary>
//        /// <remarks>错误类型SystemErrors.Ok</remarks>
//        [TestMethod]
//        public void TestMethod1_2()
//        {
//            adGroup.CampaignID = 12735;
//            adGroup.BidPrice = 2.78m;
//            adGroup.AdContentID = 2;
//            adGroup.DeviceCategory = "2,3,4,5,6";
//            adGroup.Name = "AdGroupApiTest";
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage { AccountId = 3659, UserId = 2087, CampaignId = 12735, AdGroup = adGroup };
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.Ok, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：AdGroup等于null
//        /// 错误类型SystemErrors.BusinessObjError
//        /// </remarks>
//        [TestMethod]
//        public void TestMethod2()
//        {
//            adGroup.CampaignID = 12735;
//            adGroup.BidPrice = 0.78m;
//            adGroup.AdContentID = 2;
//            adGroup.DeviceCategory = "2,3,4,5,6";
//            adGroup.Name = "AdGroupApiTest";
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage
//                             {AccountId = 3659, UserId = 2087, CampaignId = 12735, AdGroup = null};
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            //Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：AccountId等于0
//        /// 错误类型SystemErrors.BusinessObjError
//        /// </remarks>
//        [TestMethod]
//        public void TestMethod3()
//        {
//            adGroup.CampaignID = 12735;
//            adGroup.BidPrice = 0.78m;
//            adGroup.AdContentID = 2;
//            adGroup.DeviceCategory = "2,3,4,5,6";
//            adGroup.Name = "AdGroupApiTest";
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage
//                             {AccountId = 0, UserId = 2087, CampaignId = 12735, AdGroup = adGroup};
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：UserId等于0
//        /// 错误类型SystemErrors.BusinessObjError
//        /// </remarks>
//        [TestMethod]
//        public void TestMethod4()
//        {
//            adGroup.CampaignID = 12735;
//            adGroup.BidPrice = 0.78m;
//            adGroup.AdContentID = 2;
//            adGroup.DeviceCategory = "2,3,4,5,6";
//            adGroup.Name = "AdGroupApiTest";
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage
//                             {AccountId = 3659, UserId = 0, CampaignId = 12735, AdGroup = adGroup};
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：CampaignId等于0
//        /// 错误类型SystemErrors.BusinessObjError
//        /// </remarks>
//        [TestMethod]
//        public void TestMethod5()
//        {
//            adGroup.CampaignID = 0;
//            adGroup.BidPrice = 0.78m;
//            adGroup.AdContentID = 2;
//            adGroup.DeviceCategory = "2,3,4,5,6";
//            adGroup.Name = "AdGroupApiTest";
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage
//                             {AccountId = 3659, UserId = 2087, CampaignId = 0, AdGroup = adGroup};
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：Name等于空
//        /// 错误类型SystemErrors.BusinessObjError
//        /// </remarks>
//        [TestMethod]
//        public void TestMethod6_1()
//        {
//            adGroup.CampaignID = 12735;
//            adGroup.BidPrice = 0.78m;
//            adGroup.AdContentID = 2;
//            adGroup.DeviceCategory = "2,3,4,5,6";
//            adGroup.Name = "";
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage
//                             {AccountId = 3659, UserId = 2087, CampaignId = 12735, AdGroup = adGroup};
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：Name大于200个字符
//        /// 错误类型SystemErrors.BusinessObjError
//        /// </remarks>
//        [TestMethod]
//        public void TestMethod6_2()
//        {
//            adGroup.CampaignID = 12735;
//            adGroup.BidPrice = 0.78m;
//            adGroup.AdContentID = 2;
//            adGroup.DeviceCategory = "2,3,4,5,6";
//            adGroup.Name = new string('正', 101);
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage
//                             {AccountId = 3659, UserId = 2087, CampaignId = 12735, AdGroup = adGroup};
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：BidPrice等于null
//        /// 错误类型SystemErrors.BusinessObjError
//        /// </remarks>
//        [TestMethod]
//        public void TestMethod7_1()
//        {
//            adGroup.CampaignID = 12735;
//            adGroup.BidPrice = -1;
//            adGroup.AdContentID = 2;
//            adGroup.DeviceCategory = "2,3,4,5,6";
//            adGroup.Name = "AdGroupApiTest";
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage
//                             {AccountId = 3659, UserId = 2087, CampaignId = 12735, AdGroup = adGroup};
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：BidPrice小数位4位
//        /// 错误类型SystemErrors.BusinessObjError
//        /// </remarks>
//        [TestMethod]
//        public void TestMethod7_2()
//        {
//            adGroup.CampaignID = 12735;
//            adGroup.BidPrice = 0.7893m;
//            adGroup.AdContentID = 2;
//            adGroup.DeviceCategory = "2,3,4,5,6";
//            adGroup.Name = "AdGroupApiTest";
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage { AccountId = 3659, UserId = 2087, CampaignId = 12735, AdGroup = adGroup };
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：AdContentID等于0
//        /// 错误类型SystemErrors.BusinessObjIsNotSupported
//        /// </remarks>
//        [TestMethod]
//        public void TestMethod8_1()
//        {
//            adGroup.CampaignID = 12735;
//            adGroup.BidPrice = 0.78m;
//            adGroup.DeviceCategory = "2,3,4,5,6";
//            adGroup.Name = "AdGroupApiTest";
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage
//                             {AccountId = 3659, UserId = 2087, CampaignId = 12735, AdGroup = adGroup};
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjIsNotSupported, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：AdContentID等于5,ITunesAppID不可使用
//        /// 错误类型SystemErrors.BusinessObjIsNotSupported
//        /// </remarks>
//        [TestMethod]
//        public void TestMethod8_2()
//        {
//            adGroup.CampaignID = 12735;
//            adGroup.BidPrice = 0.78m;
//            adGroup.AdContentID = 5;
//            adGroup.DeviceCategory = "2,3,4,5,6";
//            adGroup.Name = "AdGroupApiTest";
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage { AccountId = 3659, UserId = 2087, CampaignId = 12735, AdGroup = adGroup };
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjIsNotSupported, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：AdContentID等于2,DeviceCategory的范围在2,3,4,5,6,7之内
//        /// 错误类型SystemErrors.BusinessObjIsNotSupported
//        /// </remarks>
//        [TestMethod]
//        public void TestMethod8_3()
//        {
//            adGroup.CampaignID = 12735;
//            adGroup.BidPrice = 0.78m;
//            adGroup.AdContentID = 2;
//            adGroup.DeviceCategory = "2,3,4,5,6,7,8";
//            adGroup.Name = "AdGroupApiTest";
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage
//                             {AccountId = 3659, UserId = 2087, CampaignId = 12735, AdGroup = adGroup};
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjIsNotSupported, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：AdContentID等于3,DeviceCategory的范围在2,4,5,7之内
//        /// 错误类型SystemErrors.BusinessObjIsNotSupported
//        /// </remarks>
//        [TestMethod]
//        public void TestMethod8_4()
//        {
//            adGroup.CampaignID = 12735;
//            adGroup.BidPrice = 0.78m;
//            adGroup.AdContentID = 3;
//            adGroup.DeviceCategory = "2,3,4,5,6,7,8";
//            adGroup.Name = "AdGroupApiTest";
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage { AccountId = 3659, UserId = 2087, CampaignId = 12735, AdGroup = adGroup };
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjIsNotSupported, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：AdContentID等于4,DeviceCategory的范围在3,6之内
//        /// 错误类型SystemErrors.BusinessObjIsNotSupported
//        /// </remarks>
//        [TestMethod]
//        public void TestMethod8_5()
//        {
//            adGroup.CampaignID = 12735;
//            adGroup.BidPrice = 0.78m;
//            adGroup.AdContentID = 4;
//            adGroup.DeviceCategory = "2,3,4,5,6,7,8";
//            adGroup.Name = "AdGroupApiTest";
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage { AccountId = 3659, UserId = 2087, CampaignId = 12735, AdGroup = adGroup };
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjIsNotSupported, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：AdContentID等于5,DeviceCategory的范围在8之内
//        /// 错误类型SystemErrors.BusinessObjIsNotSupported
//        /// </remarks>
//        [TestMethod]
//        public void TestMethod8_6()
//        {
//            adGroup.CampaignID = 12735;
//            adGroup.BidPrice = 0.78m;
//            adGroup.AdContentID = 5;
//            adGroup.DeviceCategory = "2,3,4,5,6,7,8";
//            adGroup.Name = "AdGroupApiTest";
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage { AccountId = 3659, UserId = 2087, CampaignId = 12735, AdGroup = adGroup };
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjIsNotSupported, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：AdContentID等于6
//        /// 错误类型SystemErrors.BusinessObjIsNotSupported
//        /// </remarks>
//        [TestMethod]
//        public void TestMethod8_7()
//        {
//            adGroup.CampaignID = 12735;
//            adGroup.BidPrice = 0.78m;
//            adGroup.AdContentID = 6;
//            adGroup.DeviceCategory = "2,3,4,5,6";
//            adGroup.Name = "AdGroupApiTest";
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage
//                             {AccountId = 3659, UserId = 2087, CampaignId = 12735, AdGroup = adGroup};
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjIsNotSupported, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：DeviceCategory等于null
//        /// 错误类型SystemErrors.BusinessObjError
//        /// </remarks>
//        [TestMethod]
//        public void TestMethod9()
//        {
//            adGroup.CampaignID = 12735;
//            adGroup.BidPrice = 0.78m;
//            adGroup.AdContentID = 3;
//            adGroup.Name = "AdGroupApiTest";
//            adGroup.ITunesAppID = 23456789;
//            bs.Request = new CreateNewAdGroupRequestMessage
//                             {AccountId = 3659, UserId = 2087, CampaignId = 12735, AdGroup = adGroup};
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：CampaignId不属于当前AccountId，授权失败
//        /// 错误类型SystemErrors.AuthoriztionFail
//        /// </remarks>
//        [TestMethod]
//        public void TestMethod10()
//        {
//            adGroup.CampaignID = 127;
//            adGroup.BidPrice = 0.78m;
//            adGroup.AdContentID = 2;
//            adGroup.Name = "AdGroupApiTest";
//            adGroup.ITunesAppID = 23456789;
//            adGroup.DeviceCategory = "2,3,4,5,6";
//            bs.Request = new CreateNewAdGroupRequestMessage { AccountId = 3659, UserId = 2087, CampaignId = 127, AdGroup = adGroup };
//            CreateNewAdGroupProcessor createNewCampaignProcessor = new CreateNewAdGroupProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdGroupResponseMessage rspMsg = (CreateNewAdGroupResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.AuthoriztionFail, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//    }
//}
