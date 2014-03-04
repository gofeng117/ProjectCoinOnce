using KJFramework.Net.Transaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobiSage.AdsAPI.ABS.Components.AdComp.Processors;
using MobiSage.AdsAPI.ABS.ProtocolStack;
using MobiSage.AdsAPI.CommonFramework.Core;
using MobiSage.AdsAPI.CommonFramework.Enums;
using MobiSage.AdsAPI.CommonFramework.DbAccess;
using Global = MobiSage.AdsAPI.ABS.Components.AdComp.Global;

namespace MobiSage.AdsAPI.ABS.Test
{
    [TestClass]
    public class UnitTestGetCampaignByIdProcessor
    {
        [TestMethod]
        public void TestMethod1()
        {
            //注册processors
            SystemWorker.Instance.Initialize(ServiceRoles.ABS);
            //配置数据库连接字符串
            Global.DbMySql =
                new Database(SystemWorker.Instance.ConfigurationProxy.GetField(ServiceRoles.ABS, Global.MySql),
                             Global.CommandTimeOut);
            Global.DbMySqlReadOnly =
                new Database(SystemWorker.Instance.ConfigurationProxy.GetField(ServiceRoles.ABS, Global.MySqlReadOnly),
                             Global.CommandTimeOut);
            Global.DbMySqlAdConversions =
                new Database(
                    SystemWorker.Instance.ConfigurationProxy.GetField(ServiceRoles.ABS, Global.MySqlAdConversions),
                    Global.CommandTimeOut);
            Global.DbOaInfoExcutor =
                new Database(SystemWorker.Instance.ConfigurationProxy.GetField(ServiceRoles.ABS, Global.OaInfoExcutor),
                             Global.CommandTimeOut);

            BusinessMessageTransaction bs=new BusinessMessageTransaction{};
            bs.Request = new GetCampaignByIdRequestMessage {CampaignId = 12023, AccountId = 3659};
            GetCampaignByIdProcessor getAdGroupByIdProcessor = new GetCampaignByIdProcessor();

            try
            {
                getAdGroupByIdProcessor.Process(bs);
            }
            catch
            {
            }
            GetCampaignByIdResponseMessage rspMsg = (GetCampaignByIdResponseMessage)bs.Response;
            Assert.IsNotNull(rspMsg);
        }
    }
}
