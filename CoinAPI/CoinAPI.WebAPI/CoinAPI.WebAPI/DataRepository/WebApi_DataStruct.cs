using System;

namespace CoinAPI.WebAPI.DataRepository
{
    /// <summary>
    ///     各级别业务的Api使用的数据结构对象
    /// </summary>
    public class WebApi_DataStruct
    {
        /// <summary>
        ///     获取或设置主键Id
        /// </summary>
        public int DataStructId { get; set; }
        /// <summary>
        ///     获取或设置数据结构名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     获取或设置Api业务组编号
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        ///     获取或设置数据结构创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        ///     获取或设置数据结构更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        ///     获取或设置是否对外公开该数据结构的标志
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        ///     获取或设置数据结构描述信息
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///     获取或设置链接地址
        /// </summary>
        public string ActionUrl { get; set; }
    }
}