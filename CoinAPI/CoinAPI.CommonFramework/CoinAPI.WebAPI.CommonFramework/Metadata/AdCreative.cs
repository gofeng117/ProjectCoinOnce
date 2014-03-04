using KJFramework.Messages.Attributes;
using KJFramework.Messages.Contracts;

namespace CoinAPI.CommonFramework.Metadata
{
    /// <summary>
    ///     AdCreative实体结构
    /// </summary>
    public class AdCreative : IntellectObject
    {
        /// <summary>
        ///     获取或设置AdCreative编号
        /// </summary>
        [IntellectProperty(0, AllowDefaultNull = true)]
        public long CreativeID { get; set; }
        /// <summary>
        ///     获取或设置AdCreative的名字
        /// </summary>
        [IntellectProperty(1)]
        public string Name { get; set; }
        /// <summary>
        ///     获取或设置所隶属的AdGroup编号
        /// </summary>
        [IntellectProperty(2, AllowDefaultNull = true)]
        public long GroupID { get; set; }
        /// <summary>
        ///     获取或设置所隶属的Campaign编号
        /// </summary>
        [IntellectProperty(3, AllowDefaultNull = true)]
        public long CampaignID { get; set; }
        /// <summary>
        ///     获取或设置广告创意的状态编号
        ///     <para>* 只接受1,2,3,4</para>
        /// </summary>
        [IntellectProperty(4, AllowDefaultNull = true)]
        public int AdStatusID { get; set; }
        /// <summary>
        ///     获取或设置广告创意类型ID
        ///     <para>* 只接受0,1</para>
        /// </summary>
        [IntellectProperty(5, AllowDefaultNull = true)]
        public int CreativeTypeID { get; set; }
        /// <summary>
        ///     获取或设置MediaTypeID
        /// </summary>
        [IntellectProperty(6, AllowDefaultNull = true)]
        public int MediaTypeID { get; set; }
        /// <summary>
        ///     获取或设置AdCreativeDeliveryType
        /// </summary>
        [IntellectProperty(7, AllowDefaultNull = true)]
        public int AdCreativeDeliveryType { get; set; }
        /// <summary>
        ///     获取或设置Weight
        /// </summary>
        [IntellectProperty(8, AllowDefaultNull = true)]
        public long Weight { get; set; }

    }
}