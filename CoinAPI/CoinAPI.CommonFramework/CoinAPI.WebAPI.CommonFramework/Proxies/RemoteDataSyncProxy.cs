using KJFramework.Data.Synchronization;
using KJFramework.Data.Synchronization.Enums;
using KJFramework.Data.Synchronization.EventArgs;
using KJFramework.Data.Synchronization.Factories;
using KJFramework.EventArgs;
using CoinAPI.CommonFramework.Core;
using System;

namespace CoinAPI.CommonFramework.Proxies
{
    /// <summary>
    ///     远程数据同步代理器
    ///     <para>* 用于简化与SNC之间的通信交互</para>
    /// </summary>
    internal sealed class RemoteDataSyncProxy : IRemoteDataSyncProxy
    {
        #region Constructor

        /// <summary>
        ///     远程数据同步代理器
        ///     <para>* 用于简化与SNC之间的通信交互</para>
        /// </summary>
        public RemoteDataSyncProxy()
        {
            _sncGlobalConfigIep = SystemWorker.Instance.ConfigurationProxy.GetField("Common", "SAPS_Address");
        }

        #endregion

        #region Members

        private readonly string _sncGlobalConfigIep;

        #endregion

        #region Implementation of IRemoteDataSyncProxy

        /// <summary>
        ///     创建一个数据订阅者，并且订阅到远程SAPS上
        /// </summary>
        /// <param name="catalog">SAPS上的分类</param>
        /// <param name="callback">回调函数</param>
        /// <param name="isAutoReconnect">连接断开后的重连标识</param>
        /// <returns>返回订阅后的状态</returns>
        public IRemoteDataSubscriber<string, string> Regist(string catalog, EventHandler<LightSingleArgEventArgs<DataRecvEventArgs<string, string>>> callback, bool isAutoReconnect = false)
        {
            return Regist(catalog, _sncGlobalConfigIep, callback, isAutoReconnect);
        }

        /// <summary>
        ///     创建一个数据订阅者，并且订阅到远程SAPS上
        /// </summary>
        /// <typeparam name="K">Key类型</typeparam>
        /// <typeparam name="V">Value类型</typeparam>
        /// <param name="catalog">SAPS上的分类</param>
        /// <param name="iep">远程地址</param>
        /// <param name="callback">回调函数</param>
        /// <param name="isAutoReconnect">连接断开后的重连标识</param>
        /// <returns>返回订阅后的状态</returns>
        public IRemoteDataSubscriber<K, V> Regist<K, V>(string catalog, string iep, EventHandler<LightSingleArgEventArgs<DataRecvEventArgs<K, V>>> callback, bool isAutoReconnect = false)
        {
            if (string.IsNullOrEmpty(catalog)) throw new ArgumentNullException("catalog");
            if (callback == null) throw new ArgumentNullException("callback");
            IRemoteDataSubscriber<K, V> subscriber = DataSubscriberFactory.Instance.Create<K, V>(catalog, new NetworkResource(iep), isAutoReconnect);
            if (subscriber == null) throw new Exception("#Cannot regist remote data subscriber to SAPS. #iep: " + iep);
            SubscriberState state = subscriber.Open();
            if (state != SubscriberState.Subscribed && !isAutoReconnect) throw new Exception("#Cannot regist remote data subscriber to SAPS. #iep: " + iep);
            subscriber.MessageRecv += callback;
            return subscriber;
        }

        #endregion
    }
}