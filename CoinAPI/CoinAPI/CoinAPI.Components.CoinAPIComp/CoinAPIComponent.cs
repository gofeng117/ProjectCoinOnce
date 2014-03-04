using System;
using CoinAPI.CommonFramework.Components;
using CoinAPI.CommonFramework.Core;
using CoinAPI.CommonFramework.DbAccess;
using CoinAPI.Components.AdComp.Processors.CoinAPI;
using CoinAPI.Domain.Service;
using KJFramework.Basic.Enum;
using KJFramework.EventArgs;
using KJFramework.Messages.Contracts;
using KJFramework.Net.Channels;
using KJFramework.Net.Channels.HostChannels;
using KJFramework.Net.Transaction.Agent;
using KJFramework.Net.Transaction.Comparers;
using KJFramework.Net.Transaction.Managers;
using KJFramework.Net.Transaction.ProtocolStack;
using KJFramework.Net.Transaction.Schedulers;
using KJFramework.Tracing;

namespace CoinAPI.Components.AdComp
{
    /// <summary>
    ///     Advertisment组件
    /// </summary>
    public class CoinAPIComponent : ServiceComponent
    {
        #region Constructor
        
        /// <summary>
        ///     初始化启动组件
        /// </summary>
        public CoinAPIComponent()
            : base("CoinAPI")
        {
        }

        #endregion

        #region Members

        private IHostTransportChannel _hostChannel;
        private static ITracing _tracing = TracingManager.GetTracing(typeof(CoinAPIComponent));
        private static MetadataMessageRequestScheduler _scheduler;
        private static MetadataTransactionManager _transactionManager;
        public static MetadataProtocolStack _protocolStack;


        #endregion

        #region Overrides of DynamicDomainComponent

        protected override void CoreInnerStart()
        {
            int port = int.Parse(SystemWorker.Instance.ConfigurationProxy.GetField("CoinAPI", "ServicePort"));
            _tracing.Info("     #Get client-port config succeed, port: " + port);
            Console.WriteLine("     #Get client-port config succeed, port: " + port);
            _hostChannel = new TcpHostTransportChannel(port);
            _tracing.Info("     #Initialize TCP host channel succeed!");
            Console.WriteLine("     #Initialize TCP host channel succeed!");
            if (!_hostChannel.Regist())
            {
                _tracing.Critical("     #Regist TCP port {0} failed!", port);
                Console.WriteLine(string.Format("       #Regist TCP port {0} failed!", port));
                throw new Exception(string.Format("     #Regist TCP port {0} failed!", port));
            }
            _tracing.Info("     #Regist TCP host channel succeed!");
            Console.WriteLine("     #Regist TCP host channel succeed!");
            _hostChannel.ChannelCreated += ChannelCreated;
        }

        protected override void CoreInnerStop()
        {
            if (_hostChannel == null) return;
            _hostChannel.ChannelCreated -= ChannelCreated;
            _hostChannel = null;
            _scheduler.DistoryDynamicCounters();
            _scheduler = null;
            _tracing.Info("     #Stop CoinAPI::BasicBusinessComponent succeed!");
        }

        protected override void CoreInnerOnLoading()
        {
            //注册processors
            //MidiatorGlobal.Initialize();
            //配置数据库连接字符串
            Global.DbMySql = new Database(SystemWorker.Instance.ConfigurationProxy.GetField("CoinAPI", Global.MySql), Global.CommandTimeOut);
           DatabaseManager.Inject(Global.DbMySql, Global.DbMySql);

            ServiceManager.Instance.Initialize();
            Global.RemoteApiService = ServiceManager.Instance.GetService<RemoteApiService>();
            Global.RemoteApiService.Initialize();
            _protocolStack = new MetadataProtocolStack();
            _tracing.Info("     #CoinAPI::Initialize protocol stack succeed!");
            Console.WriteLine("     #CoinAPI::Initialize protocol stack succeed!");
            _transactionManager = new MetadataTransactionManager(new TransactionIdentityComparer());
            _scheduler = new MetadataMessageRequestScheduler();

            //注册代理器
            _scheduler.Regist(new GetTickerFromOKCoinProcessor());

            _scheduler.CreateDynamicCounters();
            _tracing.Info("     #CoinAPI::Regist business processors succeed!");
            Console.WriteLine("     #CoinAPI::Regist business processors succeed!");
            _tracing.Info("     #CoinAPI::BasicBusinessComponent load succeed!");
            Console.WriteLine("     #CoinAPI::BasicBusinessComponent load succeed!");
        }

        /// <summary>
        ///     检查当前组件的健康状况
        /// </summary>
        /// <returns>
        ///     返回健康状况
        /// </returns>
        protected override HealthStatus InnerCheckHealth()
        {
            return _hostChannel == null ? HealthStatus.Death : HealthStatus.Normal;
        }

        #endregion

        #region Events

        //channel created event.
        void ChannelCreated(object sender, LightSingleArgEventArgs<ITransportChannel> e)
        {
            IMessageTransportChannel<MetadataContainer> msgChannel = new MessageTransportChannel<MetadataContainer>((IRawTransportChannel)e.Target, _protocolStack);
            IServerConnectionAgent<MetadataContainer> agent = new MetadataConnectionAgent(msgChannel, _transactionManager);
            _scheduler.Regist(agent);
        }

        #endregion
    }
}
