using System;
using KJFramework.Messages.Attributes;
using KJFramework.Messages.Contracts;

namespace CoinAPI.CommonFramework.Metadata
{
    /// <summary>
    ///     AdGroup实体结构
    /// </summary>
    public class AdGroup : IntellectObject
    {
        /// <summary>
        ///     获取或设置AdGroup编号
        /// </summary>
        [IntellectProperty(0, AllowDefaultNull = true)]
        public long GroupID { get; set; }
        /// <summary>
        ///     获取或设置所属的Campaign编号
        /// </summary>
        [IntellectProperty(1, AllowDefaultNull = true)]
        public long CampaignID { get; set; }
        /// <summary>
        ///     获取或设置AdGroup的名称
        ///     <para>* 不可为空 注：有200字符的最大长度限制</para>
        /// </summary>
        [IntellectProperty(2)]
        public string Name { get; set; }
        /// <summary>
        ///     获取或设置广告组出价
        ///     <para>* 广告组出价=广告主单价  注：目前AdsAPI中必填大于等于0的数字</para>
        /// </summary>
        [IntellectProperty(3, AllowDefaultNull = true)]
        public decimal BidPrice { get; set; }
        /// <summary>
        ///     获取或设置开发者出价
        ///     <para></para>
        /// </summary>
        [IntellectProperty(4, AllowDefaultNull = true)]
        public decimal AcBidPrice { get; set; }
        /// <summary>
        ///     获取或设置状态编号
        ///     <para>* 只接受1,2,3,4</para>
        /// </summary>
        [IntellectProperty(5, AllowDefaultNull = true)]
        public int AdStatusID { get; set; }
        /// <summary>
        ///     获取或设置
        ///     <para></para>
        /// </summary>
        [IntellectProperty(6, AllowDefaultNull = true)]
        public int DeliveryMode { get; set; }
        /// <summary>
        ///     获取或设置权重
        ///     <para></para>
        /// </summary>
        [IntellectProperty(7, AllowDefaultNull = true)]
        public long Weight { get; set; }
        /// <summary>
        ///     获取或设置单日单设备最大点击数
        ///     <para>* 控制banner广告的单日单设备最大点击数，属于Group级别的属性默认值为0，代表不限制</para>
        /// </summary>
        [IntellectProperty(8, AllowDefaultNull = true)]
        public long MaxClicksEveryDayByDevice { get; set; }
        /// <summary>
        ///     获取或设置单日单设备最大展示数
        ///     <para>* 控制banner广告的单日单设备最大展示数，属于Group级别的属性默认值为0，代表不限制</para>
        /// </summary>
        [IntellectProperty(9, AllowDefaultNull = true)]
        public long MaxImpressionsEveryDayByDevice { get; set; }
        /// <summary>
        ///     获取或设置7日单设备最大点击数
        ///     <para>* 控制banner广告的7日单设备最大点击数，属于Group级别的属性默认值为0，代表不限制</para>
        /// </summary>
        [IntellectProperty(10, AllowDefaultNull = true)]
        public long MaxClicksSevenDaysByDevice { get; set; }
        /// <summary>
        ///     获取或设置7日单设备最大展示数
        ///     <para>* 控制banner广告的7日单设备最大展示数，属于Group级别的属性默认值为0，代表不限制</para>
        /// </summary>
        [IntellectProperty(11, AllowDefaultNull = true)]
        public long MaxImpressionsSevenDaysByDevice { get; set; }
        /// <summary>
        ///     获取或设置单日广告组最大点击次数
        ///     <para>* 控制banner广告的单日广告组最大点击次数，属于Group级别的属性默认值为0，代表不限制</para>
        /// </summary>
        [IntellectProperty(12, AllowDefaultNull = true)]
        public long MaxClicksEveryDay { get; set; }
        /// <summary>
        ///     获取或设置单日广告组最大展示次数
        ///     <para>* 控制banner广告的单日广告组最大展示次数数，属于Group级别的属性默认值为0，代表不限制</para>
        /// </summary>
        [IntellectProperty(13, AllowDefaultNull = true)]
        public long MaxImpressionsEveryDay { get; set; }
        /// <summary>
        ///     获取或设置IOS类下载广告的ItunesAppID
        ///     <para>* 所推广IOS下载类广告的Itunes商店指纹ID</para>
        /// </summary>
        [IntellectProperty(14)]
        public long? ITunesAppID { get; set; }
        /// <summary>
        ///     获取或设置
        /// </summary>
        [IntellectProperty(15, AllowDefaultNull = true)]
        public int AdActionID { get; set; }
        /// <summary>
        ///     获取或设置要推广的内容的分类ID
        ///     <para>* 只开放2,3,4,5</para>
        /// </summary>
        [IntellectProperty(16, AllowDefaultNull = true)]
        public int AdContentID { get; set; }
        /// <summary>
        ///     获取或设置
        /// </summary>
        [IntellectProperty(17)]
        public string WeekTimePeriodIDs { get; set; }
        /// <summary>
        ///     获取或设置
        /// </summary>
        [IntellectProperty(18)]
        public int ThreepartyPlatformID { get; set; }
        /// <summary>
        ///     获取或设置设备类型
        ///     <para>* 不可为空 注：有200字符的最大长度限制</para>
        /// </summary>
        [IntellectProperty(19)]
        public string DeviceCategory { get; set; }
        /// <summary>
        ///     获取或设置
        /// </summary>
        [IntellectProperty(20)]
        public string DeviceNetworks { get; set; }
        /// <summary>
        ///     获取或设置
        /// </summary>
        [IntellectProperty(21)]
        public string AppCategorys { get; set; }
    }
}