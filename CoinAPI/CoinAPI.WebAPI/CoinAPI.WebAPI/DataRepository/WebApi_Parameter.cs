using System;

namespace CoinAPI.WebAPI.DataRepository
{
    /// <summary>
    ///     请求API需要的Querystring参数对象
    /// </summary>
    public class WebApi_Parameter
    {
        /// <summary>
        ///     获取或设置参数Id
        /// </summary>
        public int ParameterId { get; set; }
        /// <summary>
        ///     获取或设置参数名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     获取或设置API基础信息主键Id
        /// </summary>
        public int BaseInfoId { get; set; }
        /// <summary>
        ///     获取或设置请求API时是否需要该参数的标志
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        ///     获取或设置参数的创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        ///     获取或设置参数的更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        ///     获取或设置参数的描述信息
        /// </summary>
        public string Description { get; set; }
    }
}