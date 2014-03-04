using System.Data;
using KJFramework.Tracing;
using MobiSage.AdsAPI.CommonFramework.DbAccess;
using MobiSage.AdsAPI.CommonFramework.Enums;
using MobiSage.AdsAPI.Domain.Enums;
using MobiSage.AdsAPI.Domain.Repository.Objects;
using MobiSage.AdsAPI.Domain.Results;
using System;

namespace MobiSage.AdsAPI.Domain.Repository
{
    /// <summary>
    ///     AdAccount仓储
    /// </summary>
#if TDD
    public sealed class AdAccountRepository : MarshalByRefObject, IAdAccountRepository
#else
    public sealed class AdAccountRepository : IAdTextCreativeRepository
#endif
    {
        #region Members

        private static Database _masterDB;
        private static Database _slaveDB;
        private static readonly ITracing _tracing = TracingManager.GetTracing(typeof(AdCreativeRepository));


        #endregion

        #region Methods

        /// <summary>
        ///     正常新建Account进入BC落地
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="accountId">账户ID</param>
        /// <param name="sourceId">账户来源编号</param>
        /// <returns>结果</returns>
        public IExecuteResult CreateAccountNormal(uint userId, uint accountId, ushort sourceId)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, userId, sourceId };
                int count = _masterDB.SpExecuteNonQuery(SpName.SpAddAccount, ParametersObject.SpAddAccount, parameterValues);
                return count == 0
                              ? ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpAddAccount + ":Executed count = 0")
                              : ExecuteResult.Succeed(true);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     新建AccountId与CustomId的关系在OA中
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="accountId">账户ID</param>
        /// <param name="customId">广告主ID</param>
        /// <returns>结果</returns>
        public IExecuteResult CreateCustomerRel(uint userId, uint accountId, ushort customId)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { accountId, customId, userId };
                int count = _masterDB.SpExecuteNonQuery(SpName.SpOA_AddCustomerRelationship, ParametersObject.SpAddCustomerRelationship, parameterValues);
                return count == 0
                              ? ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpOA_AddCustomerRelationship + ":Executed count = 0")
                              : ExecuteResult.Succeed(true);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     修改账户类型（目前只对自投放开放）
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="accountId">账户ID</param>
        /// <param name="typeId">类型ID *自投放默认为3</param>
        /// <returns>结果</returns>
        public IExecuteResult UpdateAccountType(uint userId, uint accountId, uint typeId = 3)
        {
            if (userId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal user id.");
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            try
            {
                // 获取参数的值 自投放
                object[] parameterValues = new object[] { accountId, typeId, userId };
                int count = _masterDB.SpExecuteNonQuery(SpName.SpUpdateAccountType, ParametersObject.SpUpdateAccountType, parameterValues);
                return count == 0
                              ? ExecuteResult.Fail(SystemErrors.NotFound, SpName.SpUpdateAccountType + ":Executed count = 0")
                              : ExecuteResult.Succeed(true);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     获得账户列表信息
        /// </summary>
        /// <param name="accountId">账户ID</param>
        /// <returns>执行结果</returns>
        public IExecuteResult GetAccountTypeByAccountId(ulong accountId)
        {
            if (accountId == 0) return ExecuteResult.Fail(SystemErrors.Malformed, "#Illegal account id.");
            try
            {
                // account type.
                DataTable accountTypeDt = _slaveDB.SpExecuteTable(SpName.SpGetAccountTypeFromAccountDetail, ParametersObject.GetAccountTypeFromAccountDetail, new[] { accountId });
                if (accountTypeDt.Rows.Count == 0) return ExecuteResult.Fail(SystemErrors.NotFound, "#There isn't any accout accords current condition: AccountId: " + accountId);
                AccountTypes accountTypes = (AccountTypes)byte.Parse(accountTypeDt.Rows[0]["AccountTypeID"].ToString());
                return ExecuteResult.Succeed(accountTypes);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }


        /// <summary>
        ///     静态注入db实例
        /// </summary>
        /// <param name="masterDB">主DB</param>
        public static void Inject(Database masterDB, Database slaveDB)
        {
            if (null == masterDB) throw new ArgumentNullException("masterDB");
            if (null == slaveDB) throw new ArgumentNullException("slaveDB");
            _masterDB = masterDB;
            _slaveDB = slaveDB;
        }

        #endregion

    }
}