using MobiSage.AdsAPI.Domain.Business.CoreAreas;
using MobiSage.AdsAPI.Domain.Enums;

namespace MobiSage.AdsAPI.Domain.Repository.Objects
{
    /// <summary>
    ///     图片大小查询临时对象
    /// </summary>
    public struct ImageSizeSearchObject
    {
        #region Members
        
        /// <summary>
        ///     获取或设置应用操作系统类型
        /// </summary>
        public AppOSTypes AppTypeOSId;
        /// <summary>
        ///     获取或设置图片尺寸的类别编号
        /// </summary>
        public ushort ImageSizeCategoryId;
        /// <summary>
        ///     获取或设置图片的高度
        /// </summary>
        public ushort Height;
        /// <summary>
        ///     获取或设置图片的宽度
        /// </summary>
        public ushort Width;
        /// <summary>
        ///     获取或设置图片尺寸的编号
        /// </summary>
        public ushort ImageSizeId;
        /// <summary>
        ///     获取或设置媒体编号
        /// </summary>
        public ushort MediaTypeId;
        /// <summary>
        ///     获取或设置适配组编号
        /// </summary>
        public ushort AdaptiveGroupId;
        /// <summary>
        ///     获取或设置核心区域的宽度
        /// </summary>
        public float CoreWidth;
        /// <summary>
        ///     获取或设置核心区域的高度
        /// </summary>
        public float CoreHeight;
        /// <summary>
        ///     获取或设置核心区域点的X轴坐标
        /// </summary>
        public float CorePointX;
        /// <summary>
        ///     获取或设置核心区域的Y轴坐标
        /// </summary>
        public float CorePointY;

        #endregion
    }
}