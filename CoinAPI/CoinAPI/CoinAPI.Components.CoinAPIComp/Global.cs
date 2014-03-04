using System.Collections.Generic;
using CoinAPI.CommonFramework.DbAccess;
using CoinAPI.Domain.Service;

namespace CoinAPI.Components.AdComp
{
    public static class Global
    {
        public static Database DbMySql;

        public static Dictionary<string, IList<string>> DeviceCategory = new Dictionary<string, IList<string>>();
        /// <summary>
        ///     等待命令执行的时间（单位为秒）
        /// </summary>
        public static int CommandTimeOut = 120;

        public static RemoteApiService RemoteApiService;

        #region ConfigKey

        /// <summary>
        ///     MySql(主库)
        /// </summary>
        public static readonly string MySql = "MySQL";

        #endregion
    }
}
