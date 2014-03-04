using System;

namespace CoinAPI.WebAPI.DataRepository
{
    /// <summary>
    ///     数据结构的属性对象
    /// </summary>
    public class WebApi_DataStruct_Details
    {
        /// <summary>
        ///     获取或设置主键Id
        /// </summary>
        public int DetailsId { get; set; }
        /// <summary>
        ///     获取或设置属性名称
        /// </summary>
        public string DetailsName { get; set; }
        /// <summary>
        ///     获取或设置描述信息
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///     获取或设置示例
        /// </summary>
        public string DataDemo { get; set; }
        /// <summary>
        ///     获取或设置数据类型
        /// </summary>
        public string DataType { get; set; }
        /// <summary>
        ///     获取或设置创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        ///     获取或设置开放时间
        /// </summary>
        public DateTime OpenTime { get; set; }
        /// <summary>
        ///     获取或设置更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        ///     获取或设置数据结构编号
        /// </summary>
        public int DataStructId { get; set; }
        /// <summary>
        ///     获取或设置属性公开与否的标志
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        ///     获取或设置数据结构的描述信息
        /// </summary>
        public string StructDesc { get; set; }
        /// <summary>
        ///     获取或设置数据结构名称
        /// </summary>
        public string DataStructName { get; set; }
        /// <summary>
        ///     获取或设置Api业务组编号
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        ///     获取或设置数据结构的跳转链接地址
        /// </summary>
        public string ActionUrl { get; set; }
        /// <summary>
        ///     获取或设置父节点数据结构的链接地址
        /// </summary>
        public string ParentActionUrl { get; set; }
    }
}