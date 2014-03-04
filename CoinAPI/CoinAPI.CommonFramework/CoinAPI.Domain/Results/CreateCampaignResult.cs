namespace CoinAPI.Domain.Results
{
    /// <summary>
    ///     创建Campaign的执行结果
    /// </summary>
    public class CreateCampaignResultObj
    {
        #region Members

        /// <summary>
        ///     获取创建成功后的Campaign编号
        /// </summary>
        public long CampaignId { get; internal set; }

        #endregion
    }
}