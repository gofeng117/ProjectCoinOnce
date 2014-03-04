using System.Threading.Tasks;
using CoinAPI.CommonFramework.Exceptions;

namespace CoinAPI.CommonFramework.Handle
{
    /// <summary>
    ///     异步调用等待句柄接口
    /// </summary>
    public interface IAsyncWaitingHandle<T>
    {
        /// <summary>
        ///     获取异步结果
        /// </summary>
        /// <returns>返回异步结果</returns>
        /// <exception cref="CommunicationFailException">通讯失败异常</exception>
        /// <exception cref="CommunicationTimeoutException">通讯超时异常</exception>
        Task<T> GetAsyncResult();
    }
}