using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoinAPI.WebAPI.Models
{
    /// <summary>
    ///     创建资源成功的实体结构
    /// </summary>
    public class Model
    {
        /// <summary>
        ///     获取或设置创建资源成功的主键Id
        /// </summary>
        public ulong Id { get; set; }
    }
}