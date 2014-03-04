using System;

namespace CoinAPI.WebAPI.DataRepository
{
    /// <summary>
    ///     Api信息实体
    /// </summary>
    public class WebApi_BaseInfo
    {
        #region Members

        /// <summary>
        ///     获取或设置主键Id
        /// </summary>
        public int BaseInfoId { get; set; }
        /// <summary>
        ///     获取或设置Api业务组编号
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        ///     获取或设置Api名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     获取或设置Api访问地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        ///     获取或设置Api描述信息
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///     获取或设置Api开放时间
        /// </summary>
        public DateTime OpenTime { get; set; }
        /// <summary>
        ///     获取或设置Api创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        ///     获取或设置Api对外开放状态
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        ///     获取或设置Api授权标志
        /// </summary>
        public bool IsAuthorize { get; set; }
        /// <summary>
        ///     获取或设置返回结果
        /// </summary>
        public string ReturnResult { get; set; }
        /// <summary>
        ///     获取或设置Api的请求类型
        /// </summary>
        public byte HttpType { get; set; }
        /// <summary>
        ///     获取或设置Api的请求头
        /// </summary>
        public string RequestHeader { get; set; }
        /// <summary>
        ///     获取或设置Api的请求体
        /// </summary>
        public string RequestBody { get; set; }
        /// <summary>
        ///     获取或设置Api的更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        ///     获取或设置Api所属的业务级别
        /// </summary>
        public string ApiGroupName { get; set; }
        /// <summary>
        ///     获取或设置Api业务组描述信息
        /// </summary>
        public string GroupDesc { get; set; }
        /// <summary>
        ///     获取或设置Api需要的数据结构
        /// </summary>
        public WebApi_DataStruct DataStruct { get; set; }
        /// <summary>
        ///     获取或设置跳转链接地址
        /// </summary>
        public string ActionUrl { get; set; }
        /// <summary>
        ///     获取或设置Api业务组跳转链接地址
        /// </summary>
        public string GroupUrl { get; set; }
        /// <summary>
        ///     设置或获取Querystring的请求示例
        /// </summary>
        public string DemoStr { get; set; }

        #endregion
    }
}