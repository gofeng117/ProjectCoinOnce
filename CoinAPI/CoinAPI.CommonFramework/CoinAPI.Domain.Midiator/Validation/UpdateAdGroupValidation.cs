using System.Globalization;
using System.Text;
using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.Domain.Enums;

namespace MobiSage.AdsAPI.Domain.Midiator.Validation
{
    /// <summary>
    ///     更新AdGroup时候的验证器
    /// </summary>
    public class UpdateAdGroupValidation : IBusinessValidation
    {
        #region Implementation of IBusinessValidation

        /// <summary>
        ///     获取验证规则的类型
        /// </summary>
        public ValidationTypes ValidationType { get { return ValidationTypes.UpdateAdGroup; } }

        /// <summary>
        ///     验证一个对象是否符合业务规则
        /// </summary>
        /// <param name="obj">需要验证的对象</param>
        /// <returns>返回验证结果</returns>
        public bool Validate(object obj)
        {
            AdGroup adGroup = obj as AdGroup;
            if (adGroup == null) return false;
            //AdGroup不能为null
            //UserId大于0
            //AccountId大于0
            //CampaignID大于0
            //Name不能为空
            //Name有200字符的最大长度限制
            bool isOk = adGroup.AdGroupId > 0
                        && adGroup.CampaignId > 0
                        && adGroup.ContentType != null 
                        && adGroup.ContentType.AdContentId >= 2U
                        && (byte)adGroup.ActionType >= 1
                        && (byte)adGroup.ActionType <= 4
                        && (byte)adGroup.Status >= 1
                        && (byte)adGroup.Status <= 4
                        && !string.IsNullOrEmpty(adGroup.Name)
                        && !string.IsNullOrEmpty(adGroup.Name.Trim())
                        && Encoding.Default.GetByteCount(adGroup.Name) <= 200
                        && adGroup.BidPrice >= 0m
                        && adGroup.CPAPrice >= 0m
                        //&& ValidationRegex.RegexTwoDecimalPoint.IsMatch(adGroup.BidPrice.ToString(CultureInfo.InvariantCulture))
                        //&& !string.IsNullOrEmpty(adGroup.DeviceCategory)
                        && adGroup.Targetings.FrequencyTargeting.MaxImpressionsEveryDay >= 0
                        && ValidationRegex.RegexMaxImpressionsEveryDay.
                               IsMatch(adGroup.Targetings.FrequencyTargeting.MaxImpressionsEveryDay.ToString(CultureInfo.InvariantCulture));
            if (!isOk) return false;
            //ITunesAppID大于0,且符合最大位数
            if (!string.IsNullOrEmpty(adGroup.ITunesAppId.ToString()))
                if (adGroup.ITunesAppId <= 0 || !ValidationRegex.RegexITunesAppId.IsMatch(adGroup.ITunesAppId.ToString())) return false;
            return true;
        }

        /// <summary>
        ///     验证一个对象是否符合更新业务规则
        /// </summary>
        /// <param name="obj">需要验证的对象</param>
        /// <returns>返回验证结果</returns>
        public ValidateResult UpdateValidate(object obj)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     验证一个对象是否符合创建业务规则
        /// </summary>
        /// <param name="obj">需要验证的对象</param>
        /// <returns>返回验证结果</returns>
        public ValidateResult CreateValidate(object obj)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}