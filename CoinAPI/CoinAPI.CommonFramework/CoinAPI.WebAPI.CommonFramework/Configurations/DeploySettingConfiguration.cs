using KJFramework.Attribute;

namespace CoinAPI.CommonFramework.Configurations
{
    /// <summary>
    ///   Addresses������
    /// </summary>
    public sealed class DeploySettingConfiguration
    {
        /// <summary>
        ///   CSN�����ַ
        /// </summary>
        [CustomerField("CSN")]
        public string CSN;
    }
}