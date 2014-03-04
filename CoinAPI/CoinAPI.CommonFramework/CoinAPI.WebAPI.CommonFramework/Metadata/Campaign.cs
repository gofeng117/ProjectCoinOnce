using System;
using KJFramework.Messages.Attributes;
using KJFramework.Messages.Contracts;

namespace CoinAPI.CommonFramework.Metadata
{
    /// <summary>
    ///     Campaign实体结构
    /// </summary>
    public class Campaign : IntellectObject
    {
        /// <summary>
        ///     获取或设置Campaign名称
        ///     <para>* 有200字符的最大长度限制</para>
        /// </summary>
        [IntellectProperty(0)]
        public string Name { get; set; }
        /// <summary>
        ///     获取或设置账户编号
        /// </summary>
        [IntellectProperty(1,AllowDefaultNull = true)]
        public long AccountID { get; set; }
        /// <summary>
        ///     获取或设置联盟编号
        /// </summary>
        [IntellectProperty(2, AllowDefaultNull = true)]
        public int AffiliateID { get; set; }
        /// <summary>
        ///     获取或设置联盟旗下业务的具体编号
        /// </summary>
        [IntellectProperty(3, AllowDefaultNull = true)]
        public int BusinessID { get; set; }
        /// <summary>
        ///     获取或设置Campaign编号
        /// </summary>
        [IntellectProperty(4, AllowDefaultNull = true)]
        public long CampaignID { get; set; }
        /// <summary>
        ///     获取或设置状态编号
        ///     <para>* 只接受1,2,3,4, 默认: 2</para>
        /// </summary>
        [IntellectProperty(5, AllowDefaultNull = true)]
        public int AdStatusID { get; set; }
        /// <summary>
        ///     获取或设置日预算
        ///     <para>* 0为不限</para>
        /// </summary>
        [IntellectProperty(6, AllowDefaultNull = true)]
        public decimal DailyBudget { get; set; }
        /// <summary>
        ///     获取或设置剩余预算
        /// </summary>
        [IntellectProperty(7, AllowDefaultNull = true)]
        public decimal RemainingBudget { get; set; }
        /// <summary>
        ///     获取或设置广告组出价
        /// </summary>
        [IntellectProperty(8, AllowDefaultNull = true)]
        public int BidType { get; set; }
        /// <summary>
        ///     获取或设置开始时间
        /// </summary>
        [IntellectProperty(9, AllowDefaultNull = true)]
        public DateTime StartDate { get; set; }
        /// <summary>
        ///     获取或设置结束时间
        /// </summary>
        [IntellectProperty(10)]
        public DateTime? EndDate { get; set; }
        /// <summary>
        ///     获取或设置地理定向编号
        /// </summary>
        [IntellectProperty(11)]
        public string GeoIDs { get; set; }
        /// <summary>
        ///     获取或设置
        /// </summary>
        [IntellectProperty(12, AllowDefaultNull = true)]
        public int MediaTypeID { get; set; }
        /// <summary>
        ///     获取或设置是否需要水印
        /// </summary>
        [IntellectProperty(13, AllowDefaultNull = true)]
        public bool WaterMark { get; set; }
    }
}