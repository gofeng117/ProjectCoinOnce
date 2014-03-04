using KJFramework.Tracing;
using System;

namespace CoinAPI.WebAPI.Handlers
{
    /// <summary>
    ///     异常日志记录
    /// </summary>
    public class ErrorLog
    {
        #region Members

        private static readonly ITracing _tracing = TracingManager.GetTracing(typeof(ExceptionManager));

        #endregion

        #region Mothods

        /// <summary>
        /// 记录网站异常日志
        /// </summary>
        /// <param name="ex">异常信息</param>
        public static void WriteError(Exception ex)
        {
            _tracing.Error(ex,null);
        }

        #endregion
    }
}