using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using CoinAPI.CommonFramework.Core;
using CoinAPI.WebAPI.Controllers;
using CoinAPI.WebAPI.Handlers;


namespace CoinAPI.WebAPI
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SystemWorker.Instance.Initialize("WebSite", true);
            Global.DomainName = SystemWorker.Instance.ConfigurationProxy.GetField("WebSite","WebSiteDomainName");
            ////配置数据库连接字符串
            //Global.DbMySql = new Database(SystemWorker.Instance.ConfigurationProxy.GetField("WebSite", Global.MySql), Global.CommandTimeOut);
            //DatabaseManager.Inject(Global.DbMySql, Global.DbMySql);
            //DataBaseOperator.Instance.Initialize();
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            RegisterApis(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configuration.Filters.Add(new NotImplExceptionFilter());
        }

        public static void RegisterApis(HttpConfiguration config)
        {
            config.Formatters.Insert(0, new JsonNetFormatter());
            //config.MessageHandlers.Add(new AuthorizationCheckHandler());
            config.MessageHandlers.Add(new CustomerMessageHandler());
        }
    }
}