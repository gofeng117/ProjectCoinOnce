//using System;
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
//    public class UnitTestUpdateCampaignByIdProcessor
//    {
//        private readonly BusinessMessageTransaction bs;
//        private readonly Campaign campaign;

//        public UnitTestUpdateCampaignByIdProcessor()
//        {
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

//            bs = new BusinessMessageTransaction { };
//            campaign = new Campaign();
//        }

//        /// <summary>
//        /// 正确的分支
//        /// </summary>
//        [TestMethod]
//        public void TestMethod1()
//        {
//            campaign.CampaignID = 12821;
//            campaign.Name = "ApiTest1234";
//            campaign.DailyBudget = 80;
//            campaign.AdStatusID = 2;
//            //campaign.StartDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:00:00"));
//            campaign.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(6).ToString("yyyy-MM-dd HH:00:00"));
//            //campaign.StartDate = DateTime.Now.AddDays(1);
//            //campaign.EndDate = DateTime.Now.AddDays(-1);
//            bs.Request = new UpdateCampaignByIdRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            UpdateCampaignByIdProcessor createNewCampaignProcessor = new UpdateCampaignByIdProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateCampaignByIdResponseMessage rspMsg = (UpdateCampaignByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.Ok, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }


//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：CampaignID等于0</remarks>
//        [TestMethod]
//        public void TestMethod2()
//        {
//            campaign.CampaignID = 0;
//            campaign.Name = "ApiTest123";
//            campaign.DailyBudget = 20;
//            campaign.AdStatusID = 2;
//            bs.Request = new UpdateCampaignByIdRequestMessage {AccountId = 3659, UserId = 2087, Campaign = campaign};
//            UpdateCampaignByIdProcessor createNewCampaignProcessor = new UpdateCampaignByIdProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateCampaignByIdResponseMessage rspMsg = (UpdateCampaignByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：AccountId等于0</remarks>
//        [TestMethod]
//        public void TestMethod3()
//        {
//            campaign.CampaignID = 12495;
//            campaign.Name = "ApiTest123";
//            campaign.DailyBudget = 20;
//            campaign.AdStatusID = 2;
//            bs.Request = new UpdateCampaignByIdRequestMessage { AccountId = 0, UserId = 2087, Campaign = campaign };
//            UpdateCampaignByIdProcessor createNewCampaignProcessor = new UpdateCampaignByIdProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateCampaignByIdResponseMessage rspMsg = (UpdateCampaignByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：UserId等于0</remarks>
//        [TestMethod]
//        public void TestMethod4()
//        {
//            campaign.CampaignID = 12495;
//            campaign.Name = "ApiTest123";
//            campaign.DailyBudget = 20;
//            campaign.AdStatusID = 2;
//            bs.Request = new UpdateCampaignByIdRequestMessage { AccountId = 3659, UserId = 0, Campaign = campaign };
//            UpdateCampaignByIdProcessor createNewCampaignProcessor = new UpdateCampaignByIdProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateCampaignByIdResponseMessage rspMsg = (UpdateCampaignByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：Campaign等于null</remarks>
//        [TestMethod]
//        public void TestMethod5()
//        {
//            campaign.CampaignID = 12495;
//            campaign.Name = "ApiTest123";
//            campaign.DailyBudget = 20;
//            campaign.AdStatusID = 2;
//            bs.Request = new UpdateCampaignByIdRequestMessage { AccountId = 3659, UserId = 2087, Campaign = null };
//            UpdateCampaignByIdProcessor createNewCampaignProcessor = new UpdateCampaignByIdProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateCampaignByIdResponseMessage rspMsg = (UpdateCampaignByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：DailyBudget小于0</remarks>
//        [TestMethod]
//        public void TestMethod6()
//        {
//            campaign.CampaignID = 12495;
//            campaign.Name = "ApiTest123";
//            campaign.DailyBudget = -1;
//            campaign.AdStatusID = 2;
//            bs.Request = new UpdateCampaignByIdRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            UpdateCampaignByIdProcessor createNewCampaignProcessor = new UpdateCampaignByIdProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateCampaignByIdResponseMessage rspMsg = (UpdateCampaignByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：EndDate的分钟不符合规范</remarks>
//        [TestMethod]
//        public void TestMethod7_1()
//        {
//            campaign.CampaignID = 12495;
//            campaign.Name = "ApiTest123";
//            campaign.DailyBudget = 20;
//            campaign.AdStatusID = 2;
//            campaign.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:18:00"));
//            bs.Request = new UpdateCampaignByIdRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            UpdateCampaignByIdProcessor createNewCampaignProcessor = new UpdateCampaignByIdProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateCampaignByIdResponseMessage rspMsg = (UpdateCampaignByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：EndDate的秒不符合规范</remarks>
//        [TestMethod]
//        public void TestMethod7_2()
//        {
//            campaign.CampaignID = 12495;
//            campaign.Name = "ApiTest123";
//            campaign.DailyBudget = 20;
//            campaign.AdStatusID = 2;
//            campaign.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:00:01"));
//            bs.Request = new UpdateCampaignByIdRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            UpdateCampaignByIdProcessor createNewCampaignProcessor = new UpdateCampaignByIdProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateCampaignByIdResponseMessage rspMsg = (UpdateCampaignByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：EndDate的秒不符合规范</remarks>
//        [TestMethod]
//        public void TestMethod7_3()
//        {
//            campaign.CampaignID = 12495;
//            campaign.Name = "ApiTest123";
//            campaign.DailyBudget = 20;
//            campaign.AdStatusID = 2;
//            campaign.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:15:01"));
//            bs.Request = new UpdateCampaignByIdRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            UpdateCampaignByIdProcessor createNewCampaignProcessor = new UpdateCampaignByIdProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateCampaignByIdResponseMessage rspMsg = (UpdateCampaignByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：EndDate的秒不符合规范</remarks>
//        [TestMethod]
//        public void TestMethod7_4()
//        {
//            campaign.CampaignID = 12495;
//            campaign.Name = "ApiTest123";
//            campaign.DailyBudget = 20;
//            campaign.AdStatusID = 2;
//            campaign.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:30:01"));
//            bs.Request = new UpdateCampaignByIdRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            UpdateCampaignByIdProcessor createNewCampaignProcessor = new UpdateCampaignByIdProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateCampaignByIdResponseMessage rspMsg = (UpdateCampaignByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：EndDate的秒不符合规范</remarks>
//        [TestMethod]
//        public void TestMethod7_5()
//        {
//            campaign.CampaignID = 12495;
//            campaign.Name = "ApiTest123";
//            campaign.DailyBudget = 20;
//            campaign.AdStatusID = 2;
//            campaign.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:45:01"));
//            bs.Request = new UpdateCampaignByIdRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            UpdateCampaignByIdProcessor createNewCampaignProcessor = new UpdateCampaignByIdProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateCampaignByIdResponseMessage rspMsg = (UpdateCampaignByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：EndDate的秒不符合规范</remarks>
//        [TestMethod]
//        public void TestMethod7_6()
//        {
//            campaign.CampaignID = 12495;
//            campaign.Name = "ApiTest123";
//            campaign.DailyBudget = 20;
//            campaign.AdStatusID = 2;
//            campaign.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:59:01"));
//            bs.Request = new UpdateCampaignByIdRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            UpdateCampaignByIdProcessor createNewCampaignProcessor = new UpdateCampaignByIdProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateCampaignByIdResponseMessage rspMsg = (UpdateCampaignByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：Name等于""</remarks>
//        [TestMethod]
//        public void TestMethod8_1()
//        {
//            campaign.CampaignID = 12495;
//            campaign.Name = "";
//            campaign.DailyBudget = 20;
//            campaign.AdStatusID = 2;
//            bs.Request = new UpdateCampaignByIdRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            UpdateCampaignByIdProcessor createNewCampaignProcessor = new UpdateCampaignByIdProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateCampaignByIdResponseMessage rspMsg = (UpdateCampaignByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：Name大于200个字符</remarks>
//        [TestMethod]
//        public void TestMethod8_2()
//        {
//            campaign.CampaignID = 12495;
//            campaign.Name = new string('正', 101);
//            campaign.DailyBudget = 20;
//            campaign.AdStatusID = 2;
//            bs.Request = new UpdateCampaignByIdRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            UpdateCampaignByIdProcessor createNewCampaignProcessor = new UpdateCampaignByIdProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateCampaignByIdResponseMessage rspMsg = (UpdateCampaignByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }
//    }
//}
