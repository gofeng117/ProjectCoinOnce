using System.Data;
using KJFramework.Tracing;
using MobiSage.AdsAPI.CommonFramework.DbAccess;
using MobiSage.AdsAPI.CommonFramework.Enums;
using MobiSage.AdsAPI.Domain.Repository.Objects;
using MobiSage.AdsAPI.Domain.Results;
using System;
using MobiSage.AdsAPI.LGS.ProtocolStack;

namespace MobiSage.AdsAPI.Domain.Repository
{
    /// <summary>
    ///     Log落地类
    /// </summary>
#if TDD
    public sealed class LogRepository : MarshalByRefObject, ILogRepository
#else
    public sealed class LogRepository : ILogRepository
#endif
    {
        #region Members

        private static Database _masterDB;
        private static Database _slaveDB;
        private static readonly ITracing _tracing = TracingManager.GetTracing(typeof(LogRepository));

        #endregion

        #region Methods


        /// <summary>
        ///     新增日志
        /// </summary>
        /// <param name="className">类名称</param>
        /// <param name="createTime">建立时间</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="processInfo">过程日志详细信息</param>
        /// <param name="processLevel">过程日志级别</param>
        /// <param name="processDetail">过程日志详细</param>
        /// <param name="processTrace">过程日志堆栈信息</param>
        /// <returns>执行结果</returns>
        public IExecuteResult Create(string className, DateTime createTime, string serviceName, string processInfo, byte processLevel,string processDetail,string processTrace)
        {
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { className, createTime, serviceName, processInfo, processLevel, processDetail, processTrace };
                ulong logId = _masterDB.SpExecuteScalar<ulong>(SpName.SpAddLog, ParametersObject.SpAddLog, parameterValues);
                if (logId == 0) return ExecuteResult.Fail(SystemErrors.Unknown, "#LogId is 0");
                return ExecuteResult.Succeed(logId);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }
        
        /// <summary>
        ///     新增质量圆环日志
        /// </summary>
        /// <param name="fatherTid">父级TID</param>
        /// <param name="qualityRingLogs">质量圆环日志</param>
        /// <returns></returns>
        public bool CreateQualityRing(string fatherTid, QualityRingLog[] qualityRingLogs)
        {
            try
            {
                foreach (QualityRingLog qualityLog in qualityRingLogs)
                {
                    //获取参数的值
                    object[] parameterValues = new object[] { fatherTid, qualityLog.ChildTid, qualityLog.SendIP, qualityLog.ReveiveIP, qualityLog.SendServiceType, qualityLog.ReceiveServiceType, qualityLog.RequestMessage, qualityLog.SendDateTime, qualityLog.ReceiveDateTime, qualityLog.ExecuteTime};
                    int count = _masterDB.SpExecuteNonQuery(SpName.SpAddQualityRingLog, ParametersObject.AddQualityRingLog, parameterValues);
                    if (count == 0) 
                    {
                        _tracing.Error(string.Format("#AddQualityRingLog's Effect Row Count is 0, The Parameter is FatherTid:{0},ChildTid:{1},SendIP:{2},ReveiveIP:{3},SendServiceType:{4},ReceiveServiceType:{5},RequestMessage:{6},SendDateTime:{7},ReceiveDateTime:{8},ExecuteTime:{9}",
                            fatherTid, qualityLog.ChildTid, qualityLog.SendIP, qualityLog.ReveiveIP, qualityLog.SendServiceType, qualityLog.ReceiveServiceType, qualityLog.RequestMessage, qualityLog.SendDateTime, qualityLog.ReceiveDateTime, qualityLog.ExecuteTime));
                        return false;
                    }
                } 
                return true;
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return false;
            }
        }

        /// <summary>
        ///     获得发送邮件的日志
        /// </summary>
        /// <returns>返回的结果（DataSet）</returns>
        public IExecuteResult GetSendEmailLog()
        {
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { };
                DataSet ds = _masterDB.SpExecuteDataSet(SpName.SpBuildEmailLog, ParametersObject.SpBuildEmailLog, parameterValues);
                if (ds == null) return ExecuteResult.Fail(SystemErrors.Unknown, "Return DataSet is Null");
                if (ds.Tables.Count == 0) return ExecuteResult.Fail(SystemErrors.Unknown, "Not Return DataTable");
                if (ds.Tables.Count != 2) return ExecuteResult.Fail(SystemErrors.Unknown, "Not Return 2 Tables");

                return ExecuteResult.Succeed(ds);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }


        /// <summary>
        ///     获得发送质量圆环邮件的日志
        /// </summary>
        /// <returns>返回的结果（DataTable）</returns>
        public IExecuteResult GetSendQualityRingLog()
        {
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { };
                DataTable dt = _masterDB.SpExecuteTable(SpName.SpBuildQualityRingLog, ParametersObject.SpBuildQualityRingLog, parameterValues);
                if (dt == null) return ExecuteResult.Fail(SystemErrors.Unknown, "#Return DataTable is Null");

                return ExecuteResult.Succeed(dt);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     获得发送质量圆环邮件的日志
        /// </summary>
        /// <param name="fatherTid">父级tid</param>
        /// <returns>返回的结果（DataTable）</returns>
        public IExecuteResult GetSendQualityRingDetailLog(string fatherTid)
        {
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { fatherTid };
                DataTable dt = _masterDB.SpExecuteTable(SpName.SpBuildQualityRingDetailLog, ParametersObject.SpBuildQualityRingDetailLog, parameterValues);
                if (dt == null) return ExecuteResult.Fail(SystemErrors.Unknown, "#Return DataTable is Null");

                return ExecuteResult.Succeed(dt);
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
        /// <param name="slaveDB">副DB</param>
        public static void Inject(Database masterDB,Database slaveDB)
        {
            if (null == masterDB) throw new ArgumentNullException("masterDB");
            if (null == slaveDB) throw new ArgumentNullException("slaveDB");
            _masterDB = masterDB;
            _slaveDB = slaveDB;
        }

        #endregion



    }
}