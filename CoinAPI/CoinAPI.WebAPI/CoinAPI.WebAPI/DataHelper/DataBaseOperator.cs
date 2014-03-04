using CoinAPI.CommonFramework.DbAccess;

namespace CoinAPI.WebAPI.DataHelper
{
    /// <summary>
    ///     数据库操作管理者
    /// </summary>
    public class DataBaseOperator
    {
        #region Members

        public static readonly DataBaseOperator Instance = new DataBaseOperator();

        #endregion

        #region Methods

        /// <summary>
        ///     初始化需要提前装配或者注入业务实例
        /// </summary>
        public void Initialize()
        {
            DataBaseHelper.Inject(DatabaseManager.Instance.MasterDB, DatabaseManager.Instance.SlaveDB);
        }

        #endregion
    }
}