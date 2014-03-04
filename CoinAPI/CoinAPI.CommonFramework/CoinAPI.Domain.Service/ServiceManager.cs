using System.Collections.Generic;

namespace CoinAPI.Domain.Service
{
    /// <summary>
    /// 业务服务管理器
    /// </summary>
    public sealed class ServiceManager
    {
        #region Members

        public static readonly ServiceManager Instance = new ServiceManager();
        private readonly IDictionary<string, object> _serviceContainer = new Dictionary<string, object>();

        #endregion

        #region Methods

        /// <summary>
        ///     初始化需要提前装配或者注入业务实例
        /// </summary>
        public void Initialize()
        {
            //RemoteApi初始化
           // CampaignRepository.Inject(DatabaseManager.Instance.MasterDB, DatabaseManager.Instance.SlaveDB, CampaignORMConvertor.Instance, CampaignSimpleORMConvertor.Instance);
            _serviceContainer.Add(typeof(RemoteApiService).FullName, new RemoteApiService());
        }

        /// <summary>
        ///     获取一个业务服务
        /// </summary>
        /// <typeparam name="T">要获取的业务服务类型</typeparam>
        /// <returns>如果找不到该服务类型，将返回null</returns>
        public T GetService<T>()
        {
            object c;
            return _serviceContainer.TryGetValue(typeof(T).FullName, out c) ? (T)c : default(T);
        }

        #endregion
    }
}
