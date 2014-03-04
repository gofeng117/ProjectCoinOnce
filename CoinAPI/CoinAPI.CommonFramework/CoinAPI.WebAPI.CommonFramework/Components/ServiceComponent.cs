using System;
using System.Configuration;
using KJFramework.Dynamic.Components;
using KJFramework.Net.Channels.Disconvery;
using KJFramework.Net.Channels.Disconvery.Protocols;
using KJFramework.Tracing;
using CoinAPI.CommonFramework.Configurations.Settings;
using CoinAPI.CommonFramework.Core;

namespace CoinAPI.CommonFramework.Components
{
    /// <summary>
    ///     Discovery Common Component
    /// </summary>
    public abstract class ServiceComponent : DynamicDomainComponent
    {
        #region Members

        private bool _isInitialized;
        private DiscoveryInputPin _inputPin;
        private const int _monitorPort = 55555;
        protected readonly string _serviceRole;
        private static readonly ITracing _tracing = TracingManager.GetTracing(typeof (ServiceComponent));
        private readonly string _environment = (string.IsNullOrEmpty(ConfigurationManager.AppSettings["Environment"]) ? "Dev" : ConfigurationManager.AppSettings["Environment"]);

        #endregion 

        #region Method

        /// <summary>
        ///     组件服务初始化构造器
        /// </summary>
        /// <param name="serviceRole">服务角色名称</param>
        protected ServiceComponent(string serviceRole)
        {
            _serviceRole = serviceRole;
        }

        /// <summary>
        /// 开始执行
        /// </summary>
        protected override void InnerStart()
        {
            if (_inputPin != null) _inputPin.Start();
        }

        /// <summary>
        /// 停止执行
        /// </summary>
        protected override void InnerStop()
        {
            if (_inputPin != null) _inputPin.Stop();
            CoreInnerStop();
        }

        /// <summary>
        /// 加载后需要做的动作
        /// </summary>
        protected override void InnerOnLoading()
        {
            //Local configuration's CSN address is the highest priority option.
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CSN"]))
            {
                _tracing.Info("#Use local configuration CSN address...");
                Console.WriteLine("     #Use local configuration CSN address...");
                _isInitialized = true;
                if (_serviceRole != "LGS") SystemWorker.Instance.Initialize(_serviceRole, true, null, ConfigurationManager.AppSettings["CSN"]);
                else
                {
                    RemoteConfigurationSetting configurationSetting = new RemoteConfigurationSetting();
                    configurationSetting.IsCustomizeKJFrameworkConfig = true;
                    configurationSetting.IsSpecific_Customer_Profile_Config = true;
                    SystemWorker.Instance.Initialize("LGS", true, configurationSetting, null, ConfigurationManager.AppSettings["CSN"]);
                }
                CoreInnerOnLoading();
                CoreInnerStart();
            }
            else
            {
                _tracing.Info("     #Starting discovery mode and waiting specific event...");
                Console.WriteLine("     #Starting discovery mode and waiting specific event...");
                _inputPin = new DiscoveryInputPin(_monitorPort);
                //Discovery pattern.
                _inputPin.AddNotificationEvent("CSN", delegate(CommonBoradcastProtocol obj)
                {
                    if (_isInitialized) return;
                    if (obj.Environment != _environment) return;
                    _tracing.Info("     #Discovery event had been received, running business component now...");
                    Console.WriteLine("     #Discovery event had been received, running business component now...");
                    _isInitialized = true;
                    if (_serviceRole != "LGS") SystemWorker.Instance.Initialize(_serviceRole, true, null, obj.Value);
                    else
                    {
                        RemoteConfigurationSetting configurationSetting = new RemoteConfigurationSetting();
                        configurationSetting.IsCustomizeKJFrameworkConfig = true;
                        configurationSetting.IsSpecific_Customer_Profile_Config = true;
                        SystemWorker.Instance.Initialize("LGS", true, configurationSetting, null, obj.Value);
                    }
                    CoreInnerOnLoading();
                    CoreInnerStart();
                    _tracing.Info("     #Business component run at discovery mode start succeed!");
                    Console.WriteLine("     #Business component run at discovery mode start succeed!");
                });
            }
        }

        #region Methods

        /// <summary>
        ///     内部服务加载后需要做的动作
        /// </summary>
        protected abstract void CoreInnerOnLoading();
        /// <summary>
        ///     内部服务开始执行
        /// </summary>
        protected abstract void CoreInnerStart();
        /// <summary>
        ///     内部服务停止执行
        /// </summary>
        protected abstract void CoreInnerStop();

        #endregion

        #endregion
    }
}
