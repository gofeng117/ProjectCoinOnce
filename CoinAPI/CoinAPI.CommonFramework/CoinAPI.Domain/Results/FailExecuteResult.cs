﻿using CoinAPI.Domain.Enums;

namespace CoinAPI.Domain.Results
{
    /// <summary>
    ///     失败的调用结果
    /// </summary>
    public sealed class FailExecuteResult : ExecuteResult
    {
        #region Constructor

        /// <summary>
        ///     失败的调用结果
        /// </summary>
        /// <param name="errorId">系统内部错误编号</param>
        /// <param name="reason">失败的原因</param>
        internal FailExecuteResult(byte errorId, string reason = null)
            : base(ExecuteResults.Failed, null, reason)
        {
            _errorId = errorId;
        }

        #endregion
    }
}