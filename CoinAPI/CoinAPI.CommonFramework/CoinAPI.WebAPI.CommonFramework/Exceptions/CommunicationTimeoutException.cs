namespace CoinAPI.CommonFramework.Exceptions
{
    /// <summary>
    ///     通讯超时异常
    /// </summary>
    public class CommunicationTimeoutException : System.Exception
    {
        #region Constructor

        /// <summary>
        ///     通讯超时异常
        /// </summary>
        public CommunicationTimeoutException(string message)
            : base(message)
        {

        }

        #endregion
    }
}