using System;
using System.Data;
using MobiSage.AdsAPI.Domain.Log;
using KJFramework.Tracing;

namespace MobiSage.AdsAPI.Domain.Repository
{
#if TDD
    public class LogORMConvertor : MarshalByRefObject
#else
    public class LogORMConvertor : IORMConvertor
#endif
    {
        #region Members

        public static readonly LogORMConvertor LogInstance = new LogORMConvertor();

        #endregion

        #region Methods

        /// <summary>
        /// 将关系型数据转化成领域对象
        /// </summary>
        /// <param name="dr">关系型数据对象</param>
        /// <returns>领域对象实例</returns>
        public LogData ConvertToLogDataDomain(DataRow dr)
        {
            if (null == dr) throw new ArgumentNullException("dr");
            LogData logData = new LogData();
            logData.ClassName = dr["ClassName"].ToString();
            logData.CreateTime = Convert.ToDateTime(dr["CreateTime"].ToString());
            logData.LogID = Convert.ToUInt64(dr["LogID"]);
            logData.ProcessDetail = dr["ProcessDetail"].ToString();
            logData.ProcessLevel = Enum.GetName(typeof(TracingLevel), dr["ProcessLevel"]);
            logData.ProcessTrace = dr["ProcessTrace"].ToString();
            logData.ServiceName = dr["ServiceName"].ToString();
            return logData;
        }


        /// <summary>
        /// 将关系型数据转化成领域对象
        /// </summary>
        /// <param name="dr">关系型数据对象</param>
        /// <returns>领域对象实例</returns>
        public LogStatic ConvertToLogStaticDomain(DataRow dr)
        {
            if (null == dr) throw new ArgumentNullException("dr");
            LogStatic logStatic = new LogStatic();
            logStatic.ServiceName = dr["ServiceName"].ToString();
            logStatic.ClassName = dr["ClassName"].ToString();
            logStatic.ErrorCount = Convert.ToUInt32(dr["ErrorCount"]);
            return logStatic;
        }


        #endregion

    }
}
