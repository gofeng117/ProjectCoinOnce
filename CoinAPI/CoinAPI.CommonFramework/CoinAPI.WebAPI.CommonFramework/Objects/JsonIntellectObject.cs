using KJFramework.Messages.Contracts;
using Newtonsoft.Json;

namespace CoinAPI.CommonFramework.Objects
{
    public class JsonIntellectObject : IntellectObject
    {
        #region Members

        /// <summary>
        ///     获取或设置二进制数据体
        /// </summary>
        [JsonIgnore]
        public override byte[] Body
        {
            get { return base.Body; }
            set { base.Body = value; }
        }

        /// <summary>
        ///     获取一个值，该值表示了当前实体类是不是以兼容模式解析的。
        /// </summary>
        [JsonIgnore]
        public override bool CompatibleMode
        {
            get { return base.CompatibleMode; }
        }

        /// <summary>
        ///     获取一个值，该值表示了当前是否已经从第三方客户数据转换为元数据。
        /// </summary>
        [JsonIgnore]
        public override bool IsBind
        {
            get { return base.IsBind; }
        }

        /// <summary>
        ///     获取一个值，该值表示了当前是否已经从元数据第转换为三方客户数据。
        /// </summary>
        [JsonIgnore]
        public override bool IsPickup
        {
            get { return base.IsPickup; }
        }

        #endregion
    }
}