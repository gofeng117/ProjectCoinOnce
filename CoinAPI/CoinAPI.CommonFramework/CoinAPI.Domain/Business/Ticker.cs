using Newtonsoft.Json;

namespace CoinAPI.Domain.Business
{
    /// <summary>
    ///     Ticker领域对象
    /// </summary>
    public class Ticker
    {
        /// <summary>
        /// 
        /// </summary>
        public double BuyPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double SellPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double HignPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double LowPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double LastPrice { get; set; }
        /// <summary>
        ///     交易量
        /// </summary>
        public double Vol { get; set; }

        public override string ToString()
        {
            return
                JsonConvert.SerializeObject(new Ticker
                {
                    BuyPrice = BuyPrice,
                    SellPrice = SellPrice,
                    HignPrice = HignPrice,
                    LowPrice = LowPrice,
                    LastPrice = LastPrice,
                    Vol = Vol
                });
        }    
    }
}
