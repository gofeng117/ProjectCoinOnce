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
//    public class UnitTestUpdateAdTextCreativeByIdProcessor
//    {
//        private readonly AdTextCreative adTextCreative;
//        private readonly BusinessMessageTransaction bs;

//        public UnitTestUpdateAdTextCreativeByIdProcessor()
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
//            adTextCreative=new AdTextCreative();
//        }

//        /// <summary>
//        /// 正确的分支
//        /// </summary>
//        [TestMethod]
//        public void TestMethod1()
//        {
//            adTextCreative.CreativeID = 24501;
//            adTextCreative.GroupID = 13853;
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.DestinationURL = "http://itunes.apple.com/cn/app/id397698342?mt=8";
//            adTextCreative.Title = "sp调用测试——CreativeName";
//            adTextCreative.Name = "sp调用测试——UpdateAdCreative";
//            adTextCreative.ImageURL = "http://mws.adsage.com/mobisage/bcadm/13853/test.jpg";
//            adTextCreative.CreativeTypeID = 1;
//            UpdateAdTextCreativeByIdRequestMessage ResMsg=new UpdateAdTextCreativeByIdRequestMessage();
            
//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 2087;
//            ResMsg.AdTextCreative = adTextCreative;
//            bs.Request = ResMsg;
//            UpdateAdTextCreativeByIdProcessor updateAdTextCreativeByIdProcessor = new UpdateAdTextCreativeByIdProcessor();

//            try
//            {
//                updateAdTextCreativeByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdTextCreativeByIdResponseMessage rspMsg = (UpdateAdTextCreativeByIdResponseMessage)bs.Response;

//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId,SystemErrors.Ok);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：AdCreative为null</remarks>
//        [TestMethod]
//        public void TestMethod2()
//        {
           
//            UpdateAdTextCreativeByIdRequestMessage ResMsg = new UpdateAdTextCreativeByIdRequestMessage();
//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 2087;
//            bs.Request = ResMsg;
//            UpdateAdTextCreativeByIdProcessor updateAdTextCreativeByIdProcessor = new UpdateAdTextCreativeByIdProcessor();

//            try
//            {
//                updateAdTextCreativeByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdTextCreativeByIdResponseMessage rspMsg = (UpdateAdTextCreativeByIdResponseMessage)bs.Response;
//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjError);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：UserId等于0</remarks>
//        [TestMethod]
//        public void TestMethod3()
//        {
//            adTextCreative.CreativeID = 24501;
//            adTextCreative.GroupID = 13853;
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.DestinationURL = "http://itunes.apple.com/cn/app/id397698342?mt=8";
//            adTextCreative.Title = "sp调用测试——CreativeName";
//            adTextCreative.Name = "sp调用测试——UpdateAdCreative";
//            adTextCreative.ImageURL = "http://mws.adsage.com/mobisage/bcadm/13853/test.jpg";
//            adTextCreative.CreativeTypeID = 1;
//            UpdateAdTextCreativeByIdRequestMessage ResMsg = new UpdateAdTextCreativeByIdRequestMessage();

//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 0;
//            ResMsg.AdTextCreative = adTextCreative;
//            bs.Request = ResMsg;
//            UpdateAdTextCreativeByIdProcessor updateAdTextCreativeByIdProcessor = new UpdateAdTextCreativeByIdProcessor();

//            try
//            {
//                updateAdTextCreativeByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdTextCreativeByIdResponseMessage rspMsg = (UpdateAdTextCreativeByIdResponseMessage)bs.Response;
//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjError);
//        }

//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：AccountId等于0</remarks>
//        [TestMethod]
//        public void TestMethod4()
//        {
//            adTextCreative.CreativeID = 24501;
//            adTextCreative.GroupID = 13853;
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.DestinationURL = "http://itunes.apple.com/cn/app/id397698342?mt=8";
//            adTextCreative.Title = "sp调用测试——CreativeName";
//            adTextCreative.Name = "sp调用测试——UpdateAdCreative";
//            adTextCreative.ImageURL = "http://mws.adsage.com/mobisage/bcadm/13853/test.jpg";
//            adTextCreative.CreativeTypeID = 1;
//            UpdateAdTextCreativeByIdRequestMessage ResMsg = new UpdateAdTextCreativeByIdRequestMessage();

//            ResMsg.AccountId = 0;
//            ResMsg.UserId = 2087;
//            ResMsg.AdTextCreative = adTextCreative;
//            bs.Request = ResMsg;
//            UpdateAdTextCreativeByIdProcessor updateAdTextCreativeByIdProcessor = new UpdateAdTextCreativeByIdProcessor();

//            try
//            {
//                updateAdTextCreativeByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdTextCreativeByIdResponseMessage rspMsg = (UpdateAdTextCreativeByIdResponseMessage)bs.Response;
//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjError);
//        }
//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：CreativeID等于0</remarks>
//        [TestMethod]
//        public void TestMethod5()
//        {
//            adTextCreative.CreativeID = 0;
//            adTextCreative.GroupID = 13853;
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.DestinationURL = "http://itunes.apple.com/cn/app/id397698342?mt=8";
//            adTextCreative.Title = "sp调用测试——CreativeName";
//            adTextCreative.Name = "sp调用测试——UpdateAdCreative";
//            adTextCreative.ImageURL = "http://mws.adsage.com/mobisage/bcadm/13853/test.jpg";
//            adTextCreative.CreativeTypeID = 1;
//            UpdateAdTextCreativeByIdRequestMessage ResMsg = new UpdateAdTextCreativeByIdRequestMessage();

//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 2087;
//            ResMsg.AdTextCreative = adTextCreative;
//            bs.Request = ResMsg;
//            UpdateAdTextCreativeByIdProcessor updateAdTextCreativeByIdProcessor = new UpdateAdTextCreativeByIdProcessor();

//            try
//            {
//                updateAdTextCreativeByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdTextCreativeByIdResponseMessage rspMsg = (UpdateAdTextCreativeByIdResponseMessage)bs.Response;
//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjError);
//        }
//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：GroupID等于0</remarks>
//        [TestMethod]
//        public void TestMethod6()
//        {
//            adTextCreative.CreativeID = 24501;
//            adTextCreative.GroupID = 0;
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.DestinationURL = "http://itunes.apple.com/cn/app/id397698342?mt=8";
//            adTextCreative.Title = "sp调用测试——CreativeName";
//            adTextCreative.Name = "sp调用测试——UpdateAdCreative";
//            adTextCreative.ImageURL = "http://mws.adsage.com/mobisage/bcadm/13853/test.jpg";
//            adTextCreative.CreativeTypeID = 1;
//            UpdateAdTextCreativeByIdRequestMessage ResMsg = new UpdateAdTextCreativeByIdRequestMessage();

//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 2087;
//            ResMsg.AdTextCreative = adTextCreative;
//            bs.Request = ResMsg;
//            UpdateAdTextCreativeByIdProcessor updateAdTextCreativeByIdProcessor = new UpdateAdTextCreativeByIdProcessor();

//            try
//            {
//                updateAdTextCreativeByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdTextCreativeByIdResponseMessage rspMsg = (UpdateAdTextCreativeByIdResponseMessage)bs.Response;
//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjError);
//        }
//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：CampaignID等于0</remarks>
//        [TestMethod]
//        public void TestMethod7()
//        {
//            adTextCreative.CreativeID = 24501;
//            adTextCreative.GroupID = 13853;
//            adTextCreative.CampaignID = 0;
//            adTextCreative.DestinationURL = "http://itunes.apple.com/cn/app/id397698342?mt=8";
//            adTextCreative.Title = "sp调用测试——CreativeName";
//            adTextCreative.Name = "sp调用测试——UpdateAdCreative";
//            adTextCreative.ImageURL = "http://mws.adsage.com/mobisage/bcadm/13853/test.jpg";
//            adTextCreative.CreativeTypeID = 1;
//            UpdateAdTextCreativeByIdRequestMessage ResMsg = new UpdateAdTextCreativeByIdRequestMessage();

//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 2087;
//            ResMsg.AdTextCreative = adTextCreative;
//            bs.Request = ResMsg;
//            UpdateAdTextCreativeByIdProcessor updateAdTextCreativeByIdProcessor = new UpdateAdTextCreativeByIdProcessor();

//            try
//            {
//                updateAdTextCreativeByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdTextCreativeByIdResponseMessage rspMsg = (UpdateAdTextCreativeByIdResponseMessage)bs.Response;
//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjError);
//        }
//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：DestinationURL为空</remarks>
//        [TestMethod]
//        public void TestMethod8()
//        {
//            adTextCreative.CreativeID = 24501;
//            adTextCreative.GroupID = 13853;
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.DestinationURL = "";
//            adTextCreative.Title = "sp调用测试——CreativeName";
//            adTextCreative.Name = "sp调用测试——UpdateAdCreative";
//            adTextCreative.ImageURL = "http://mws.adsage.com/mobisage/bcadm/13853/test.jpg";
//            adTextCreative.CreativeTypeID = 1;
//            UpdateAdTextCreativeByIdRequestMessage ResMsg = new UpdateAdTextCreativeByIdRequestMessage();

//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 2087;
//            ResMsg.AdTextCreative = adTextCreative;
//            bs.Request = ResMsg;
//            UpdateAdTextCreativeByIdProcessor updateAdTextCreativeByIdProcessor = new UpdateAdTextCreativeByIdProcessor();

//            try
//            {
//                updateAdTextCreativeByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdTextCreativeByIdResponseMessage rspMsg = (UpdateAdTextCreativeByIdResponseMessage)bs.Response;
//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjError);
//        }
//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：Title为空</remarks>
//        [TestMethod]
//        public void TestMethod9()
//        {
//            adTextCreative.CreativeID = 24501;
//            adTextCreative.GroupID = 13853;
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.DestinationURL = "http://itunes.apple.com/cn/app/id397698342?mt=8";
//            adTextCreative.Title = "";
//            adTextCreative.Name = "sp调用测试——UpdateAdCreative";
//            adTextCreative.ImageURL = "http://mws.adsage.com/mobisage/bcadm/13853/test.jpg";
//            adTextCreative.CreativeTypeID = 1;
//            UpdateAdTextCreativeByIdRequestMessage ResMsg = new UpdateAdTextCreativeByIdRequestMessage();

//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 2087;
//            ResMsg.AdTextCreative = adTextCreative;
//            bs.Request = ResMsg;
//            UpdateAdTextCreativeByIdProcessor updateAdTextCreativeByIdProcessor = new UpdateAdTextCreativeByIdProcessor();

//            try
//            {
//                updateAdTextCreativeByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdTextCreativeByIdResponseMessage rspMsg = (UpdateAdTextCreativeByIdResponseMessage)bs.Response;
//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjError);
//        }
//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>覆盖分支：Title有200字符的最大长度限制</remarks>
//        [TestMethod]
//        public void TestMethod10()
//        {
//            adTextCreative.CreativeID = 24501;
//            adTextCreative.GroupID = 13853;
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.DestinationURL = "http://itunes.apple.com/cn/app/id397698342?mt=8";
//            adTextCreative.Title = "调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试"
//                                   + "调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试"
//                                   + "调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试"
//                                   + "调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试"
//                                   + "调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试"
//                                   +"调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试";
//            adTextCreative.Name = "sp调用测试——UpdateAdCreative";
//            adTextCreative.ImageURL = "http://mws.adsage.com/mobisage/bcadm/13853/test.jpg";
//            adTextCreative.CreativeTypeID = 1;
//            UpdateAdTextCreativeByIdRequestMessage ResMsg = new UpdateAdTextCreativeByIdRequestMessage();

//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 2087;
//            ResMsg.AdTextCreative = adTextCreative;
//            bs.Request = ResMsg;
//            UpdateAdTextCreativeByIdProcessor updateAdTextCreativeByIdProcessor = new UpdateAdTextCreativeByIdProcessor();

//            try
//            {
//                updateAdTextCreativeByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdTextCreativeByIdResponseMessage rspMsg = (UpdateAdTextCreativeByIdResponseMessage)bs.Response;
//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjError);
//        }
//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>Name不能为空</remarks>
//        [TestMethod]
//        public void TestMethod11()
//        {
//            adTextCreative.CreativeID = 24501;
//            adTextCreative.GroupID = 13853;
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.DestinationURL = "http://itunes.apple.com/cn/app/id397698342?mt=8";
//            adTextCreative.Title = "sp调用测试——CreativeName";
//            adTextCreative.Name = "";
//            adTextCreative.ImageURL = "http://mws.adsage.com/mobisage/bcadm/13853/test.jpg";
//            adTextCreative.CreativeTypeID = 1;
//            UpdateAdTextCreativeByIdRequestMessage ResMsg = new UpdateAdTextCreativeByIdRequestMessage();

//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 2087;
//            ResMsg.AdTextCreative = adTextCreative;
//            bs.Request = ResMsg;
//            UpdateAdTextCreativeByIdProcessor updateAdTextCreativeByIdProcessor = new UpdateAdTextCreativeByIdProcessor();

//            try
//            {
//                updateAdTextCreativeByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdTextCreativeByIdResponseMessage rspMsg = (UpdateAdTextCreativeByIdResponseMessage)bs.Response;
//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjError);
//        }
//        /// <summary>
//        /// 错误的分支
//        /// </summary>
//        /// <remarks>Name有200字符的最大长度限制</remarks>
//        [TestMethod]
//        public void TestMethod12()
//        {
//            adTextCreative.CreativeID = 24501;
//            adTextCreative.GroupID = 13853;
//            adTextCreative.CampaignID = 12735;
//            adTextCreative.DestinationURL = "http://itunes.apple.com/cn/app/id397698342?mt=8";
//            adTextCreative.Title = "sp调用测试——CreativeName";
//            adTextCreative.Name = "调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试"
//                                   + "调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试"
//                                   + "调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试"
//                                   + "调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试"
//                                   + "调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试"
//                                   + "调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试调用测试";
//            adTextCreative.ImageURL = "http://mws.adsage.com/mobisage/bcadm/13853/test.jpg";
//            adTextCreative.CreativeTypeID = 1;
//            UpdateAdTextCreativeByIdRequestMessage ResMsg = new UpdateAdTextCreativeByIdRequestMessage();

//            ResMsg.AccountId = 3659;
//            ResMsg.UserId = 2087;
//            ResMsg.AdTextCreative = adTextCreative;
//            bs.Request = ResMsg;
//            UpdateAdTextCreativeByIdProcessor updateAdTextCreativeByIdProcessor = new UpdateAdTextCreativeByIdProcessor();

//            try
//            {
//                updateAdTextCreativeByIdProcessor.Process(bs);
//            }
//            catch
//            {
//            }
//            UpdateAdTextCreativeByIdResponseMessage rspMsg = (UpdateAdTextCreativeByIdResponseMessage)bs.Response;
//            Assert.IsNotNull(rspMsg);
//            Assert.AreEqual(rspMsg.ErrorId, SystemErrors.BusinessObjError);
//        }
//    }
//}
