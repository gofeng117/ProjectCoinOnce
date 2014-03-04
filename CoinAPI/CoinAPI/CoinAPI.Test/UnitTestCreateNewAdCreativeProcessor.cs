//using System.Diagnostics;
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
//    public class UnitTestCreateNewAdCreativeProcessor
//    {
//        private static bool firstFlg;
//        private BusinessMessageTransaction bs;
//        private AdTextCreative adTextCreative;

//        public UnitTestCreateNewAdCreativeProcessor()
//        {
//            bs = new BusinessMessageTransaction { };
//            adTextCreative = new AdTextCreative();

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
//            //广告创意图片Url初始化
//            Global.AdCreativeImageUrl = SystemWorker.Instance.ConfigurationProxy.GetField(ServiceRoles.ABS,
//                                                                                     Global.AdCreativeImageUrlConfigKey);

//            firstFlg = true;
//        }

//        /// <summary>
//        /// 正确的分支
//        /// </summary>
//        /// <remarks>错误类型SystemErrors.Ok</remarks>
//        [TestMethod]
//        public void TestMethod1()
//        {
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.CreativeTypeID = 1;
//            adTextCreative.Name = "AdCreativeApiTest";
//            adTextCreative.DestinationURL = "http://www.baidu.com";
//            adTextCreative.Title = "test 123";
//            bs.Request = new CreateNewAdTextCreativeRequestMessage
//                             {AccountId = 3659, UserId = 2087, AdTextCreative = adTextCreative, AdGroupId = 13853};
//            CreateNewAdTextCreativeProcessor createNewCampaignProcessor = new CreateNewAdTextCreativeProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdTextCreativeResponseMessage rspMsg = (CreateNewAdTextCreativeResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.Ok, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：AdTextCreative等于null
//        /// 错误类型SystemErrors.BusinessObjError</remarks>
//        [TestMethod]
//        public void TestMethod2()
//        {
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.CreativeTypeID = 1;
//            adTextCreative.Name = "AdCreativeApiTest";
//            adTextCreative.DestinationURL = "http://www.baidu.com";
//            adTextCreative.Title = "test 123";
//            bs.Request = new CreateNewAdTextCreativeRequestMessage
//                             {AccountId = 3659, UserId = 2087, AdTextCreative = null, AdGroupId = 13853};
//            CreateNewAdTextCreativeProcessor createNewCampaignProcessor = new CreateNewAdTextCreativeProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdTextCreativeResponseMessage rspMsg = (CreateNewAdTextCreativeResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：UserId等于0
//        /// 错误类型SystemErrors.BusinessObjError</remarks>
//        [TestMethod]
//        public void TestMethod3()
//        {
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.CreativeTypeID = 1;
//            adTextCreative.Name = "AdCreativeApiTest";
//            adTextCreative.DestinationURL = "http://www.baidu.com";
//            adTextCreative.Title = "test 123";
//            bs.Request = new CreateNewAdTextCreativeRequestMessage
//                             {AccountId = 3659, UserId = 0, AdTextCreative = adTextCreative, AdGroupId = 13853};
//            CreateNewAdTextCreativeProcessor createNewCampaignProcessor = new CreateNewAdTextCreativeProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdTextCreativeResponseMessage rspMsg = (CreateNewAdTextCreativeResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：AccountId等于0
//        /// 错误类型SystemErrors.BusinessObjError</remarks>
//        [TestMethod]
//        public void TestMethod4()
//        {
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.CreativeTypeID = 1;
//            adTextCreative.Name = "AdCreativeApiTest";
//            adTextCreative.DestinationURL = "http://www.baidu.com";
//            adTextCreative.Title = "test 123";
//            bs.Request = new CreateNewAdTextCreativeRequestMessage
//                             {AccountId = 0, UserId = 2087, AdTextCreative = adTextCreative, AdGroupId = 13853};
//            CreateNewAdTextCreativeProcessor createNewCampaignProcessor = new CreateNewAdTextCreativeProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdTextCreativeResponseMessage rspMsg = (CreateNewAdTextCreativeResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：AdGroupId等于0
//        /// 错误类型SystemErrors.BusinessObjError</remarks>
//        [TestMethod]
//        public void TestMethod5()
//        {
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.CreativeTypeID = 1;
//            adTextCreative.Name = "AdCreativeApiTest";
//            adTextCreative.DestinationURL = "http://www.baidu.com";
//            adTextCreative.Title = "test 123";
//            bs.Request = new CreateNewAdTextCreativeRequestMessage
//                             {AccountId = 3659, UserId = 2087, AdTextCreative = adTextCreative, AdGroupId = 0};
//            CreateNewAdTextCreativeProcessor createNewCampaignProcessor = new CreateNewAdTextCreativeProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdTextCreativeResponseMessage rspMsg = (CreateNewAdTextCreativeResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：CampaignID等于0
//        /// 错误类型SystemErrors.BusinessObjError</remarks>
//        [TestMethod]
//        public void TestMethod6()
//        {
//            adTextCreative.CampaignID = 0;
//            adTextCreative.CreativeTypeID = 1;
//            adTextCreative.Name = "AdCreativeApiTest";
//            adTextCreative.DestinationURL = "http://www.baidu.com";
//            adTextCreative.Title = "test 123";
//            bs.Request = new CreateNewAdTextCreativeRequestMessage
//                             {AccountId = 3659, UserId = 2087, AdTextCreative = adTextCreative, AdGroupId = 13853};
//            CreateNewAdTextCreativeProcessor createNewCampaignProcessor = new CreateNewAdTextCreativeProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdTextCreativeResponseMessage rspMsg = (CreateNewAdTextCreativeResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：DestinationURL等于空
//        /// 错误类型SystemErrors.BusinessObjError</remarks>
//        [TestMethod]
//        public void TestMethod7_1()
//        {
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.CreativeTypeID = 1;
//            adTextCreative.Name = "AdCreativeApiTest";
//            adTextCreative.DestinationURL = "";
//            adTextCreative.Title = "test 123";
//            bs.Request = new CreateNewAdTextCreativeRequestMessage
//                             {AccountId = 3659, UserId = 2087, AdTextCreative = adTextCreative, AdGroupId = 13853};
//            CreateNewAdTextCreativeProcessor createNewCampaignProcessor = new CreateNewAdTextCreativeProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdTextCreativeResponseMessage rspMsg = (CreateNewAdTextCreativeResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：DestinationURL大于1024个字符
//        /// 错误类型SystemErrors.BusinessObjError</remarks>
//        [TestMethod]
//        public void TestMethod7_2()
//        {
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.CreativeTypeID = 1;
//            adTextCreative.Name = "AdCreativeApiTest";
//            adTextCreative.DestinationURL = "http://www.baidu.com" + new string('正', 513);
//            adTextCreative.Title = "test 123";
//            bs.Request = new CreateNewAdTextCreativeRequestMessage
//                             {AccountId = 3659, UserId = 2087, AdTextCreative = adTextCreative, AdGroupId = 13853};
//            CreateNewAdTextCreativeProcessor createNewCampaignProcessor = new CreateNewAdTextCreativeProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdTextCreativeResponseMessage rspMsg = (CreateNewAdTextCreativeResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：DestinationURL不符合http开头的规则
//        /// 错误类型SystemErrors.BusinessObjError</remarks>
//        [TestMethod]
//        public void TestMethod7_3()
//        {
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.CreativeTypeID = 1;
//            adTextCreative.Name = "AdCreativeApiTest";
//            adTextCreative.DestinationURL = "www.baidu.com";
//            adTextCreative.Title = "test 123";
//            bs.Request = new CreateNewAdTextCreativeRequestMessage
//                             {AccountId = 3659, UserId = 2087, AdTextCreative = adTextCreative, AdGroupId = 13853};
//            CreateNewAdTextCreativeProcessor createNewCampaignProcessor = new CreateNewAdTextCreativeProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdTextCreativeResponseMessage rspMsg = (CreateNewAdTextCreativeResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：Name等于空
//        /// 错误类型SystemErrors.BusinessObjError</remarks>
//        [TestMethod]
//        public void TestMethod8_1()
//        {
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.CreativeTypeID = 1;
//            adTextCreative.Name = "";
//            adTextCreative.DestinationURL = "http://www.baidu.com";
//            adTextCreative.Title = "test 123";
//            bs.Request = new CreateNewAdTextCreativeRequestMessage
//                             {AccountId = 3659, UserId = 2087, AdTextCreative = adTextCreative, AdGroupId = 13853};
//            CreateNewAdTextCreativeProcessor createNewCampaignProcessor = new CreateNewAdTextCreativeProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdTextCreativeResponseMessage rspMsg = (CreateNewAdTextCreativeResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：Name大于200个字符
//        /// 错误类型SystemErrors.BusinessObjError</remarks>
//        [TestMethod]
//        public void TestMethod8_2()
//        {
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.CreativeTypeID = 1;
//            adTextCreative.Name = new string('正', 101);
//            adTextCreative.DestinationURL = "http://www.baidu.com";
//            adTextCreative.Title = "test 123";
//            bs.Request = new CreateNewAdTextCreativeRequestMessage
//                             {AccountId = 3659, UserId = 2087, AdTextCreative = adTextCreative, AdGroupId = 13853};
//            CreateNewAdTextCreativeProcessor createNewCampaignProcessor = new CreateNewAdTextCreativeProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdTextCreativeResponseMessage rspMsg = (CreateNewAdTextCreativeResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：Title等于空
//        /// 错误类型SystemErrors.BusinessObjError</remarks>
//        [TestMethod]
//        public void TestMethod9_1()
//        {
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.CreativeTypeID = 1;
//            adTextCreative.Name = "AdCreativeApiTest";
//            adTextCreative.DestinationURL = "http://www.baidu.com";
//            adTextCreative.Title = "";
//            bs.Request = new CreateNewAdTextCreativeRequestMessage
//                             {AccountId = 3659, UserId = 2087, AdTextCreative = adTextCreative, AdGroupId = 13853};
//            CreateNewAdTextCreativeProcessor createNewCampaignProcessor = new CreateNewAdTextCreativeProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdTextCreativeResponseMessage rspMsg = (CreateNewAdTextCreativeResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：Title大于200个字符
//        /// 错误类型SystemErrors.BusinessObjError</remarks>
//        [TestMethod]
//        public void TestMethod9_2()
//        {
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.CreativeTypeID = 1;
//            adTextCreative.Name = "AdCreativeApiTest";
//            adTextCreative.DestinationURL = "http://www.baidu.com";
//            adTextCreative.Title = new string('正', 101);
//            bs.Request = new CreateNewAdTextCreativeRequestMessage
//                             {AccountId = 3659, UserId = 2087, AdTextCreative = adTextCreative, AdGroupId = 13853};
//            CreateNewAdTextCreativeProcessor createNewCampaignProcessor = new CreateNewAdTextCreativeProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdTextCreativeResponseMessage rspMsg = (CreateNewAdTextCreativeResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：CreativeTypeID等于0
//        /// 错误类型SystemErrors.BusinessIsNotSupported</remarks>
//        [TestMethod]
//        public void TestMethod10()
//        {
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.CreativeTypeID = 0;
//            adTextCreative.Name = "AdCreativeApiTest";
//            adTextCreative.DestinationURL = "http://www.baidu.com";
//            adTextCreative.Title = "test 123";
//            bs.Request = new CreateNewAdTextCreativeRequestMessage
//                             {AccountId = 3659, UserId = 2087, AdTextCreative = adTextCreative, AdGroupId = 13853};
//            CreateNewAdTextCreativeProcessor createNewCampaignProcessor = new CreateNewAdTextCreativeProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdTextCreativeResponseMessage rspMsg = (CreateNewAdTextCreativeResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessIsNotSupported, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：AdGroupId不属于当前AccountId，授权失败
//        /// 错误类型SystemErrors.AuthoriztionFail</remarks>
//        [TestMethod]
//        public void TestMethod11()
//        {
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.CreativeTypeID = 1;
//            adTextCreative.Name = "AdCreativeApiTest";
//            adTextCreative.DestinationURL = "http://www.baidu.com";
//            adTextCreative.Title = "test 123";
//            bs.Request = new CreateNewAdTextCreativeRequestMessage
//                             {AccountId = 3659, UserId = 2087, AdTextCreative = adTextCreative, AdGroupId = 138};
//            CreateNewAdTextCreativeProcessor createNewCampaignProcessor = new CreateNewAdTextCreativeProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewAdTextCreativeResponseMessage rspMsg = (CreateNewAdTextCreativeResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.AuthoriztionFail, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }
//    }
//}
