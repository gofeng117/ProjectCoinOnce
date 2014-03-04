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
//    public class UnitTestCreateNewCampaignProcessor
//    {
//        private BusinessMessageTransaction bs;
//        private Campaign campaign;

//        public UnitTestCreateNewCampaignProcessor()
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

//        [TestMethod]
//        public void TestMethod()
//        {
//            TestMethod1_1();
//            TestMethod1_2();
//            TestMethod2();
//            TestMethod3();
//            TestMethod5_1();
//            TestMethod5_2();
//            TestMethod5_3();
//            TestMethod5_4();
//            TestMethod6();
//            TestMethod7();
//            TestMethod8();
//            TestMethod9();
//            TestMethod10();
//            TestMethod11();
//            TestMethod12();
//            TestMethod13();
//            TestMethod14();

//        }

//        /// <summary>
//        /// 正确的分支
//        /// </summary>
//        /// <remarks>没有EndDate</remarks>
//        [TestMethod]
//        public void TestMethod1_1()
//        {
//            campaign.Name = "ApiTest";
//            campaign.DailyBudget = 50;
//            campaign.StartDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:00:00"));
//            //campaign.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:00:00"));
//            //campaign.StartDate = DateTime.Now.AddDays(1);
//            //campaign.EndDate = DateTime.Now.AddDays(-1);
//            bs.Request = new CreateNewCampaignRequestMessage {AccountId = 3659, UserId = 2087, Campaign = campaign};
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.Ok, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 正确的分支
//        /// </summary>
//        /// <remarks>有EndDate</remarks>
//        [TestMethod]
//        public void TestMethod1_2()
//        {
//            campaign.Name = "ApiTest";
//            campaign.DailyBudget = 50;
//            campaign.StartDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:00:00"));
//            campaign.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:00:00"));
//            bs.Request = new CreateNewCampaignRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;
           
//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.Ok, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>DailyBudget小于0</remarks>
//        [TestMethod]
//        public void TestMethod2()
//        {
//            campaign.Name = "ApiTest";
//            //campaign.Name = "";
//            //campaign.Name = null;
//            //campaign.Name = new string('*', 201);
//            //campaign.Name = new string('正', 101);
//            //campaign.Name = new string('正', 99) + new string('*', 3);
//            campaign.DailyBudget = -1;
//            //campaign.DailyBudget = 0;
//            campaign.StartDate = DateTime.Now.AddDays(1);
//            //campaign.EndDate = DateTime.Now.AddDays(-1);
//            //campaign.StartDate = Convert.ToDateTime("2012-12-1 13:59:00");
//            //campaign.EndDate = Convert.ToDateTime("2012-12-1 15:00:00");
            
//            //campaign = null;

//            bs.Request = new CreateNewCampaignRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：StartDate比系统日期小</remarks>
//        [TestMethod]
//        public void TestMethod3()
//        {
//            campaign.Name = "ApiTest";
//            campaign.DailyBudget = 50;
//            campaign.StartDate = DateTime.Now.AddDays(-1);
//            bs.Request = new CreateNewCampaignRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：StartDate的分钟不符合规范</remarks>
//        [TestMethod]
//        public void TestMethod4()
//        {
//            campaign.Name = "ApiTest";
//            campaign.DailyBudget = 50;
//            campaign.StartDate = Convert.ToDateTime("2012-12-1 13:41:00");
//            bs.Request = new CreateNewCampaignRequestMessage {AccountId = 3659, UserId = 2087, Campaign = campaign};
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：StartDate的秒不符合规范</remarks>
//        [TestMethod]
//        public void TestMethod5_1()
//        {
//            campaign.Name = "ApiTest";
//            campaign.DailyBudget = 50;
//            campaign.StartDate = Convert.ToDateTime("2012-12-1 13:30:01");
//            bs.Request = new CreateNewCampaignRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：StartDate的秒不符合规范</remarks>
//        [TestMethod]
//        public void TestMethod5_2()
//        {
//            campaign.Name = "ApiTest";
//            campaign.DailyBudget = 50;
//            campaign.StartDate = Convert.ToDateTime("2012-12-1 13:45:01");
//            bs.Request = new CreateNewCampaignRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：StartDate的秒不符合规范</remarks>
//        [TestMethod]
//        public void TestMethod5_3()
//        {
//            campaign.Name = "ApiTest";
//            campaign.DailyBudget = 50;
//            campaign.StartDate = Convert.ToDateTime("2012-12-1 13:59:01");
//            bs.Request = new CreateNewCampaignRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：StartDate的秒不符合规范</remarks>
//        [TestMethod]
//        public void TestMethod5_4()
//        {
//            campaign.Name = "ApiTest";
//            campaign.DailyBudget = 50;
//            campaign.StartDate = Convert.ToDateTime("2012-12-1 13:15:01");
//            bs.Request = new CreateNewCampaignRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：EndDate的分钟不符合规范</remarks>
//        [TestMethod]
//        public void TestMethod6()
//        {
//            campaign.Name = "ApiTest";
//            campaign.DailyBudget = 50;
//            campaign.StartDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:00:00"));
//            campaign.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:18:00"));
//            bs.Request = new CreateNewCampaignRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：EndDate的秒不符合规范</remarks>
//        [TestMethod]
//        public void TestMethod7()
//        {
//            campaign.Name = "ApiTest";
//            campaign.DailyBudget = 50;
//            campaign.StartDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:00:00"));
//            campaign.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:00:01"));
//            bs.Request = new CreateNewCampaignRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：EndDate比StartDate小</remarks>
//        [TestMethod]
//        public void TestMethod8()
//        {
//            campaign.Name = "ApiTest";
//            campaign.DailyBudget = 50;
//            campaign.StartDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:00:00"));
//            campaign.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:00:00"));
//            bs.Request = new CreateNewCampaignRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：AccountId等于0</remarks>
//        [TestMethod]
//        public void TestMethod9()
//        {
//            campaign.Name = "ApiTest";
//            campaign.DailyBudget = 50;
//            campaign.StartDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:00:00"));
//            campaign.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:00:00"));
//            bs.Request = new CreateNewCampaignRequestMessage { AccountId = 0, UserId = 2087, Campaign = campaign };
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：UserId等于0</remarks>
//        [TestMethod]
//        public void TestMethod10()
//        {
//            campaign.Name = "ApiTest";
//            campaign.DailyBudget = 50;
//            campaign.StartDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:45:00"));
//            campaign.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:00:00"));
//            bs.Request = new CreateNewCampaignRequestMessage { AccountId = 3659, UserId = 0, Campaign = campaign };
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：campaign等于null</remarks>
//        [TestMethod]
//        public void TestMethod11()
//        {
//            bs.Request = new CreateNewCampaignRequestMessage { AccountId = 3659, UserId = 2087, Campaign = null };
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：Name等于null</remarks>
//        [TestMethod]
//        public void TestMethod12()
//        {
//            campaign.Name = null;
//            campaign.DailyBudget = 50;
//            campaign.StartDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:00:00"));
//            campaign.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:00:00"));
//            bs.Request = new CreateNewCampaignRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：Name等于""</remarks>
//        [TestMethod]
//        public void TestMethod13()
//        {
//            campaign.Name = "";
//            campaign.DailyBudget = 50;
//            campaign.StartDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:00:00"));
//            campaign.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:00:00"));
//            bs.Request = new CreateNewCampaignRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：Name大于200个字符</remarks>
//        [TestMethod]
//        public void TestMethod14()
//        {
//            campaign.Name = new string('正', 101);
//            campaign.DailyBudget = 50;
//            campaign.StartDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:00:00"));
//            campaign.EndDate = Convert.ToDateTime(DateTime.Now.AddDays(2).ToString("yyyy-MM-dd HH:00:00"));
//            bs.Request = new CreateNewCampaignRequestMessage { AccountId = 3659, UserId = 2087, Campaign = campaign };
//            CreateNewCampaignProcessor createNewCampaignProcessor = new CreateNewCampaignProcessor();

//            try
//            {
//                createNewCampaignProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            CreateNewCampaignResponseMessage rspMsg = (CreateNewCampaignResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(SystemErrors.BusinessObjError, rspMsg.ErrorId);
//            Debug.WriteLine(rspMsg);
//        }
//    }
//}
