using System;

namespace CoinAPI.WebAPI.DataRepository
{
    /// <summary>
    ///     Api业务组实体
    /// </summary>
    public class WebApi_Group
    {
        /// <summary>
        ///     获取或设置组编号
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        ///     获取或设置名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     获取或设置描述信息
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///     获取或设置创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        ///     获取或设置新建时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        ///     获取或设置链接地址
        /// </summary>
        public string ActionUrl { get; set; }
    }
}