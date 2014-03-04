namespace CoinAPI.Domain.Midiator.Converters
{
    /// <summary>
    ///     转换器工厂
    /// </summary>
    public static class ConverterFactory
    {
        #region Constructor

        /// <summary>
        ///     转换器工厂
        /// </summary>
        static ConverterFactory()
        {
            _tickerConverter = new TickerConverter();
        }

        #endregion

        #region Members

        private static readonly TickerConverter _tickerConverter;


        #endregion

        #region Methods

        /// <summary>
        ///     获取一个公共的Ticker对象转换器
        /// </summary>
        /// <returns>返回Campaign转换器</returns>
        public static TickerConverter GetTickerConverter()
        {
            return _tickerConverter;
        }

        #endregion
    }
}