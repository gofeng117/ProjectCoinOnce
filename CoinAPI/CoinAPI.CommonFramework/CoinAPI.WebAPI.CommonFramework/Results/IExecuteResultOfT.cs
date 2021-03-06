using CoinAPI.Domain.Enums;

namespace CoinAPI.CommonFramework.Results
{
    /// <summary>
    ///     执行结果接口
    /// </summary>
    /// <typeparam name="T">返回值类型</typeparam>
    public interface IExecuteResult<T>
    {
        /// <summary>
        ///     获取执行结果的状态
        /// </summary>
        ExecuteResults State { get; }
        /// <summary>
        ///     获取内部系统的错误编号
        /// </summary>
        byte ErrorId { get; }
        /// <summary>
        ///     获取错误信息
        /// </summary>
        string Error { get; }
        /// <summary>
        ///     获取内部所包含的调用结果对象
        /// </summary>
        /// <typeparam name="T">调用结果对象的类型</typeparam>
        /// <returns>返回调用结果</returns>
        T GetResult();
    }
}