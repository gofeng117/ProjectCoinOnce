namespace CoinAPI.CommonFramework.Enums
{
    /// <summary>
    ///   消息类型枚举
    /// </summary>
    public enum MessageCommunicationTypes : byte
    {
        /// <summary>
        ///   请求消息
        /// </summary>
        REQ = 0x00,
        /// <summary>
        ///   应答消息
        /// </summary>
        RSP = 0x01
    }
}