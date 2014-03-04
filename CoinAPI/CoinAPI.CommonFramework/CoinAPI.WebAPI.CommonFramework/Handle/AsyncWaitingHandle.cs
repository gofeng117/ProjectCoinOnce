using KJFramework.EventArgs;
using KJFramework.Net.Transaction;
using KJFramework.Net.Transaction.Messages;
using CoinAPI.CommonFramework.Exceptions;
using System;
using System.Threading.Tasks;

namespace CoinAPI.CommonFramework.Handle
{
    /// <summary>
    ///     异步等待句柄
    /// </summary>
    internal class AsyncWaitingHandle : IAsyncWaitingHandle<BaseMessage>
    {
        #region Constructor

        /// <summary>
        ///     异步等待句柄
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <param name="timeout">通讯超时处理委托</param>
        /// <param name="failed">通讯失败处理委托</param>
        /// <exception cref="System.ArgumentNullException">transaction不能为空</exception>
        public AsyncWaitingHandle(BusinessMessageTransaction transaction, Action timeout, Action failed)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            _transaction = transaction;
            //begin setup task.
            TaskCompletionSource<BaseMessage> completionSource = new TaskCompletionSource<BaseMessage>();
            Task<BaseMessage> task = completionSource.Task;
            _transaction.ResponseArrived += delegate(object sender, LightSingleArgEventArgs<BaseMessage> e) { completionSource.SetResult(e.Target); };
            _transaction.Timeout += delegate
            {
                if (timeout != null) timeout();
                completionSource.SetException(new CommunicationTimeoutException(string.Format("Transaction: {0} was Timeout!", _transaction.Identity)));
            };
            _transaction.Failed += delegate
            {
                if (failed != null) failed();
                completionSource.SetException(new CommunicationTimeoutException(string.Format("Transaction: {0} was Failed!", _transaction.Identity)));
            };
            _task = task;
        }

        #endregion

        #region Members

        private readonly Task<BaseMessage> _task; 
        private readonly BusinessMessageTransaction _transaction;

        #endregion

        #region Methods

        /// <summary>
        ///     获取异步结果
        /// </summary>
        /// <returns>返回异步结果</returns>
        /// <exception cref="CommunicationFailException">通讯失败异常</exception>
        /// <exception cref="CommunicationTimeoutException">通讯超时异常</exception>
        public async Task<BaseMessage> GetAsyncResult()
        {
            return await _task;
        }

        #endregion
    }
}