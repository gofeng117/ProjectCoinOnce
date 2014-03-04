using CoinAPI.CommonFramework.DbAccess;

namespace CoinAPI.WebAPI
{
    /// <summary>
    ///     全局文件类
    /// </summary>
    internal static class Global
    {
        #region Members

        /// <summary>
        ///     获取或设置website域名
        /// </summary>
        public static string DomainName { get; set; }
        /// <summary>
        ///     获取或设置数据库对象
        /// </summary>
        public static Database DbMySql;
        /// <summary>
        ///     获取或设置数据库操作命令执行的时间（单位为秒）
        /// </summary>
        public static int CommandTimeOut = 120;
        /// <summary>
        ///     MySql(主库)
        /// </summary>
        public static readonly string MySql = "MySQL";

        #endregion
    }
}