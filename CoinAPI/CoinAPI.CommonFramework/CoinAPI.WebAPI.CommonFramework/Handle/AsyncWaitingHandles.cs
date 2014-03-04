using System;
using KJFramework.Net.Transaction;
using KJFramework.Net.Transaction.Messages;

namespace CoinAPI.CommonFramework.Handle
{
    /// <summary>
    ///     异步等待句柄创建集
    /// </summary>
    public static class AsyncWaitingHandles
    {
        #region Methods

        /// <summary>
        ///     为一个事务创建一个异步等待句柄
        /// </summary>
        /// <param name="transaction">等待的事务</param>
        /// <returns>返回一个新的异步等待句柄</returns>
        /// <exception cref="System.ArgumentNullException">transaction参数不能为空</exception>
        public static IAsyncWaitingHandle<BaseMessage> Create(BusinessMessageTransaction transaction)
        {
            return new AsyncWaitingHandle(transaction, null, null);
        }

        /// <summary>
        ///     为一个事务创建一个异步等待句柄
        /// </summary>
        /// <param name="transaction">等待的事务</param>
        /// <param name="timeout">通讯超时处理委托</param>
        /// <param name="failed">通讯失败处理委托</param>
        /// <returns>返回一个新的异步等待句柄</returns>
        /// <exception cref="System.ArgumentNullException">transaction参数不能为空</exception>
        public static IAsyncWaitingHandle<BaseMessage> Create(BusinessMessageTransaction transaction, Action timeout, Action failed)
        {
            return new AsyncWaitingHandle(transaction, timeout, failed);
        }

        #endregion
    }
}