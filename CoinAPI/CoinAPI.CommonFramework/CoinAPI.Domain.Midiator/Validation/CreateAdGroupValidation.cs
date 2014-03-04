using System.Globalization;
using System.Linq;
using System.Text;
using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.Domain.Enums;

namespace MobiSage.AdsAPI.Domain.Midiator.Validation
{
    /// <summary>
    ///     创建AdGroup时候的验证器
    /// </summary>
    public class CreateAdGroupValidation : IBusinessValidation
    {
        #region Implementation of IBusinessValidation

        /// <summary>
        ///     获取验证规则的类型
        /// </summary>
        public ValidationTypes ValidationType { get { return ValidationTypes.CreateAdGroup; } }

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
            //只有AdContentID为2,3,4时，iTunesAppID才有效
            bool isOk =  adGroup.CampaignId > 0
                        && (byte)adGroup.ActionType >= 1
                        && (byte)adGroup.ActionType <= 4
                        && (byte)adGroup.Status >= 1
                        && (byte)adGroup.Status <= 4
                        && adGroup.ContentType != null
                        && !string.IsNullOrEmpty(adGroup.Name)
                        && !string.IsNullOrEmpty(adGroup.Name.Trim())
                        && Encoding.Default.GetByteCount(adGroup.Name) <= 200
                        && adGroup.BidPrice >= 0m
                        && adGroup.CPAPrice >= 0m
                        //&& ValidationRegex.RegexTwoDecimalPoint.IsMatch(adGroup.BidPrice.ToString(CultureInfo.InvariantCulture))
                        && !string.IsNullOrEmpty(adGroup.DeviceCategory)
                        && adGroup.Targetings.FrequencyTargeting.MaxImpressionsEveryDay >= 0
                        && ValidationRegex.RegexMaxImpressionsEveryDay.IsMatch(adGroup.Targetings.FrequencyTargeting.MaxImpressionsEveryDay.ToString(CultureInfo.InvariantCulture));
            if (!isOk) return false;

            //AdContentID只开放2,3,4,5
            if (adGroup.ContentType.AdContentId < 2) return false;
            //AdContentID等于5的时候，ITunesAppID不能传值
            if (adGroup.ContentType.AdContentId == 5 && !string.IsNullOrEmpty(adGroup.ITunesAppId.ToString())) return false;
            //ITunesAppID大于0,且符合最大位数
            if (!string.IsNullOrEmpty(adGroup.ITunesAppId.ToString()))
                if (adGroup.ITunesAppId <= 0 || !ValidationRegex.RegexITunesAppId.IsMatch(adGroup.ITunesAppId.ToString())) return false;
            if (adGroup.ContentType.AdContentId == 7 || adGroup.ContentType.AdContentId == 8) return true;
            //AdContentID = 2,DeviceCategory的范围在2,3,4,5,6,7之内
            //AdContentID = 3,DeviceCategory的范围在2,4,5,7之内
            //AdContentID = 4,DeviceCategory的范围在3,6之内
            //AdContentID = 5,DeviceCategory只能是8
            string[] reqMsgDeviceCategory = adGroup.DeviceCategory.Split(',');
            string adContentId = adGroup.ContentType.AdContentId.ToString(CultureInfo.InvariantCulture);
            return !reqMsgDeviceCategory.Any(t => MidiatorGlobal.DeviceCategory[adContentId].Contains(t) == false);
        }

        /// <summary>
        ///     验证一个对象是否符合创建业务规则
        /// </summary>
        /// <param name="obj">需要验证的对象</param>
        /// <returns>返回验证结果</returns>
        public ValidateResult CreateValidate(object obj)
        {
            AdGroup adGroup = obj as AdGroup;
            if (adGroup == null) return new ValidateResult(false, "#Target object cannot be null.");
            if (adGroup.CampaignId == 0) return new ValidateResult(false, "#CampaignId MUST > 0.");
            if ((byte)adGroup.ActionType < 1 || (byte)adGroup.ActionType > 4) return new ValidateResult(false, "#ActionType value must be in 1~4 range.");
            if ((byte)adGroup.Status < 1 || (byte)adGroup.Status > 4) return new ValidateResult(false, "#Status value must be in 1~4 range.");
            if (adGroup.ContentType == null) return new ValidateResult(false, "#ContentType cannot be null.");
            if (string.IsNullOrEmpty(adGroup.Name) || string.IsNullOrEmpty(adGroup.Name.Trim())) return new ValidateResult(false, "#Name cannot be null or empty.");
            if (adGroup.BidPrice < 0M) return new ValidateResult(false, "#BidPrice MUST >= 0.");
            if (adGroup.CPAPrice < 0M) return new ValidateResult(false, "#CPAPrice MUST >= 0.");
            if (Encoding.Default.GetByteCount(adGroup.Name) > 200) return new ValidateResult(false, "#Name max length cannot over 200.");
            if (string.IsNullOrEmpty(adGroup.DeviceCategory)) return new ValidateResult(false, "#DeviceCategory cannot be null or empty.");
            if (!ValidationRegex.RegexMaxImpressionsEveryDay.IsMatch(adGroup.Targetings.FrequencyTargeting.MaxImpressionsEveryDay.ToString(CultureInfo.InvariantCulture)))
                return new ValidateResult(false, "#MaxImpressionsEveryDay cannot pass the specific regex condition.");
            //AdContentID只开放2,3,4,5
            if (adGroup.ContentType.AdContentId < 2) return new ValidateResult(false, "#AdContentId only open in 2~5 value range.");
            if (adGroup.ContentType.AdContentId == 5 && !string.IsNullOrEmpty(adGroup.ITunesAppId.ToString()))
                return new ValidateResult(false, "#ITunesAppId cannot be null or empty when the AdContentId == 5.");
            //ITunesAppID大于0,且符合最大位数
            if (!string.IsNullOrEmpty(adGroup.ITunesAppId.ToString()))
                if (adGroup.ITunesAppId <= 0 || !ValidationRegex.RegexITunesAppId.IsMatch(adGroup.ITunesAppId.ToString()))
                    return new ValidateResult(false, "#ITunesAppId MUST > 0 or ITunesAppId cannot pass the specific regex condition.");
            if (adGroup.ContentType.AdContentId == 7 || adGroup.ContentType.AdContentId == 8) return new ValidateResult(true, null);
            //AdContentID = 2,DeviceCategory的范围在2,3,4,5,6,7之内
            //AdContentID = 3,DeviceCategory的范围在2,4,5,7之内
            //AdContentID = 4,DeviceCategory的范围在3,6之内
            //AdContentID = 5,DeviceCategory只能是8
            string[] reqMsgDeviceCategory = adGroup.DeviceCategory.Split(',');
            string adContentId = adGroup.ContentType.AdContentId.ToString(CultureInfo.InvariantCulture);
            if(reqMsgDeviceCategory.Any(t => MidiatorGlobal.DeviceCategory[adContentId].Contains(t) == false))
                return new ValidateResult(false, "#DeviceCategory values cannot in the specific range.");
            return new ValidateResult(true, null);
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

        #endregion
    }
}