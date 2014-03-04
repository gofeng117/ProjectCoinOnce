namespace CoinAPI.CommonFramework.Exceptions
{
    /// <summary>
    ///     通讯失败异常
    /// </summary>
    public class CommunicationFailException : System.Exception
    {
        #region Constructor

        /// <summary>
        ///     通讯失败异常
        /// </summary>
        public CommunicationFailException(string message)
            : base(message)
        {

        }

        #endregion
    }
}