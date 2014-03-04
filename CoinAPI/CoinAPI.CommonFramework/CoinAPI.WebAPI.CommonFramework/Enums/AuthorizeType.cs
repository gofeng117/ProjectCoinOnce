namespace CoinAPI.CommonFramework.Enums
{
    /// <summary>
    /// 账户授权类型
    /// </summary>
    public enum AuthorizeType : byte
    {
        /// <summary>
        /// 可查看、可编辑、可授权
        /// </summary>
        All = 0x01,
        /// <summary>
        /// 可编辑，可查看
        /// </summary>
        Edit = 0x02,
        /// <summary>
        /// 可查看
        /// </summary>
        ReadOnly = 0x03
    }
}
