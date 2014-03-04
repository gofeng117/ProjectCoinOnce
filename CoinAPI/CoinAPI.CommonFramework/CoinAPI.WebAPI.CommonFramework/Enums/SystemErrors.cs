namespace CoinAPI.CommonFramework.Enums
{
    /// <summary>
    ///     系统错误枚举
    /// </summary>
    public sealed class SystemErrors
    {
        /// <summary>
        /// 系统未知错误
        /// </summary>
        public const byte Unknown = 255;
        /// <summary>
        /// 请求的格式错误
        /// </summary>
        public const byte Malformed = 254;
        /// <summary>
        /// 未找到
        /// </summary>
        public const byte NotFound = 253;
        /// <summary>
        /// 已经使用
        /// </summary>
        public const byte AccountExists = 252;
        /// <summary>
        ///     授权失败
        /// </summary>
        public const byte AuthoriztionFail = 251;
        /// <summary>
        ///     请求超时
        /// </summary>
        public const byte Timeout = 250;
        /// <summary>
        ///     业务数据错误
        /// </summary>
        public const byte BusinessObjError = 249;
        /// <summary>
        ///     业务不被支持
        /// </summary>
        public const byte BusinessIsNotSupported = 248;
       /// <summary>
        ///     业务数据项不被支持
        /// </summary>
        public const byte BusinessObjIsNotSupported = 247;
        /// <summary>
        ///     安全配额检测失败
        /// </summary>
        public const byte SecurityQuotaFail = 246;

        /// <summary>
        ///     敏感词信息检测存在
        /// </summary>
        public const byte SensitiveWordsExist = 245;

        /// <summary>
        ///     成功
        /// </summary>
        public const byte Ok = 0;
    }
}