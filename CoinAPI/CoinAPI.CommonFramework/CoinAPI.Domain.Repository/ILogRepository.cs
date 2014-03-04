using System;
using MobiSage.AdsAPI.Domain.Results;
using MobiSage.AdsAPI.LGS.ProtocolStack;

namespace MobiSage.AdsAPI.Domain.Repository
{
    /// <summary>
    /// 日志落地接口
    /// </summary>
    public interface ILogRepository
    {
        /// <summary>
        ///     新增日志
        /// </summary>
        /// <param name="className">类名称</param>
        /// <param name="createTime">建立时间</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="processInfo">过程日志详细信息</param>
        /// <param name="processLevel">过程日志级别</param>
        /// <param name="processDetail">过程日志详细</param>
        /// <param name="processTrace">过程日志堆栈消息</param>
        /// <returns>执行结果</returns>
        IExecuteResult Create(string className, DateTime createTime, string serviceName, string processInfo, byte processLevel, string processDetail, string processTrace);

        /// <summary>
        ///     获得发送邮件的日志
        /// </summary>
        /// <returns>返回的执行结果（DataSet）</returns>
        IExecuteResult GetSendEmailLog();

        /// <summary>
        ///     新增质量圆环
        /// </summary>
        /// <param name="fatherTid">父级TID</param>
        /// <param name="qualityRingLogs">质量圆环日志</param>
        /// <returns>执行结果是否正确</returns>
        bool CreateQualityRing(string fatherTid, QualityRingLog[] qualityRingLogs);

        /// <summary>
        ///     获得发送质量圆环相关信息邮件的日志
        /// </summary>
        /// <returns>返回的执行结果（DataTable）</returns>
        IExecuteResult GetSendQualityRingLog();

        /// <summary>
        ///     获得发送质量圆环邮件的日志
        /// </summary>
        /// <param name="fatherTid">父级tid</param>
        /// <returns>返回的结果（DataTable）</returns>
        IExecuteResult GetSendQualityRingDetailLog(string fatherTid);
    }
}
