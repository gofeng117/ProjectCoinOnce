namespace CoinAPI.WebAPI
{
    /// <summary>
    ///     字符串处理类
    /// </summary>
    public class StringHandleHelper
    {
        /// <summary>
        ///     将<br/>字符串替换成""
        /// </summary>
        /// <param name="str">要替换的字符串</param>
        /// <returns>返回替换后的字符串</returns>
        public static string DecodeString(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";
            return str.Replace("<br/>", "");
        }
    }
}