using KJFramework.Messages.Attributes;

namespace CoinAPI.CommonFramework.Metadata
{
    /// <summary>
    ///     AdTextCreative实体结构(文字广告)
    /// </summary>
    public class AdTextCreative : AdCreative
    {
        /// <summary>
        ///     获取或设置文字描述
        /// </summary>
        [IntellectProperty(20)]
        public string Title { get; set; }
        /// <summary>
        ///     获取或设置创意的点击链接
        /// </summary>
        [IntellectProperty(21)]
        public string DestinationURL { get; set; }
        /// <summary>
        ///     获取或设置DisplayURL
        /// </summary>
        [IntellectProperty(22)]
        public string DisplayURL { get; set; }
        /// <summary>
        ///     获取或设置Desc1
        /// </summary>
        [IntellectProperty(23)]
        public string Desc1 { get; set; }
        /// <summary>
        ///     获取或设置Desc2
        /// </summary>
        [IntellectProperty(24)]
        public string Desc2 { get; set; }
        /// <summary>
        ///     获取或设置文字创意中的logo地址
        /// </summary>
        [IntellectProperty(25)]
        public string ImageURL { get; set; }
        /// <summary>
        ///     获取或设置FontColor
        /// </summary>
        [IntellectProperty(26)]
        public string FontColor { get; set; }
        /// <summary>
        ///     获取或设置BackgroudColor
        /// </summary>
        [IntellectProperty(27)]
        public string BackgroudColor { get; set; }
        /// <summary>
        ///     获取或设置WildcardColor
        /// </summary>
        [IntellectProperty(28)]
        public string WildcardColor { get; set; }
        /// <summary>
        ///     获取或设置BannerBackgroundColor
        /// </summary>
        [IntellectProperty(29)]
        public string BannerBackgroundColor { get; set; }
        /// <summary>
        ///     获取或设置OperateBackgroundColor
        /// </summary>
        [IntellectProperty(30)]
        public string OperateBackgroundColor { get; set; }
        /// <summary>
        ///     获取或设置OperateColor
        /// </summary>
        [IntellectProperty(31)]
        public string OperateColor { get; set; }
        /// <summary>
        ///     获取或设置TemplateSolutionID
        /// </summary>
        [IntellectProperty(32, AllowDefaultNull = true)]
        public int TemplateSolutionID { get; set; }
    }
}