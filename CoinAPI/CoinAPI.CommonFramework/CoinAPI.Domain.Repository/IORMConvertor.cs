using System.Data;

namespace MobiSage.AdsAPI.Domain.Repository
{
    /// <summary>
    ///     关系型数据与领域模型的转化接口
    /// </summary>
    /// <typeparam name="T">要转化的领域对象类型</typeparam>
    public interface IORMConvertor<T>
    {
        /// <summary>
        ///     将关系型数据转化成领域对象
        /// </summary>
        /// <param name="dataRow">关系型数据对象</param>
        /// <returns>领域对象实例</returns>
        T ConvertToDomain(DataRow dataRow);
    }
}
