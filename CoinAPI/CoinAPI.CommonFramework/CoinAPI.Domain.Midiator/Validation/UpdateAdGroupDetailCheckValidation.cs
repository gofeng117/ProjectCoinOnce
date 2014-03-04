using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.Domain.Enums;
using System;
using System.Data;
using System.Linq;

namespace MobiSage.AdsAPI.Domain.Midiator.Validation
{
    /// <summary>
    ///     此验证器用于验证更加详细的，适用于Update AdGroup级别的业务验证
    /// </summary>
    public class UpdateAdGroupDetailCheckValidation : IBusinessValidation
    {
        #region Implementation of IBusinessValidation

        /// <summary>
        ///     获取验证规则的类型
        /// </summary>
        public ValidationTypes ValidationType { get { return ValidationTypes.UpdateAdGroupDetail; } }

        /// <summary>
        ///     验证一个对象是否符合业务规则
        /// </summary>
        /// <param name="obj">需要验证的对象</param>
        /// <returns>返回验证结果</returns>
        public bool Validate(object obj)
        {
            object[] adgroups = obj as object[];
            if (adgroups == null || adgroups.Length != 2 || adgroups[0] == null || adgroups[1] == null) return false;
            AdGroup adGroup = (AdGroup) adgroups[0];
            DataTable dt = (DataTable) adgroups[1];
            uint dbAdContentId = (uint)(int)dt.Rows[0]["AdContentID"];
            string dbITunesApId = Convert.IsDBNull(dt.Rows[0]["ITunesAppID"]) ? null : dt.Rows[0]["ITunesAppID"].ToString();
            if (dbAdContentId < 2 || (dbAdContentId == 5 && adGroup.ITunesAppId != null)) return false;
            if (adGroup.ContentType.AdContentId == 7 || adGroup.ContentType.AdContentId == 8) return true;
            //AdContentID = 2,DeviceCategory的范围在2,3,4,5,6,7之内
            //AdContentID = 3,DeviceCategory的范围在2,4,5,7之内
            //AdContentID = 4,DeviceCategory的范围在3,6之内
            //AdContentID = 5,DeviceCategory只能是8
            string[] reqMsgDeviceCategory = adGroup.DeviceCategory.Split(',');
            if (reqMsgDeviceCategory.Any(t => MidiatorGlobal.DeviceCategory[dbAdContentId.ToString()].Contains(t) == false)) return false;
            //AdContentID等于5的时候，ITunesAppID不能传值
            if (dbAdContentId == 5 && !string.IsNullOrEmpty(adGroup.ITunesAppId.ToString())) return false;
            //ITunesAppID值已经存在时，不允许修改
            if (!string.IsNullOrEmpty(dbITunesApId) && ((ulong)long.Parse(dbITunesApId) != adGroup.ITunesAppId)) return false;
            return true;
        }

        /// <summary>
        ///     验证一个对象是否符合更新业务规则
        /// </summary>
        /// <param name="obj">需要验证的对象</param>
        /// <returns>返回验证结果</returns>
        public ValidateResult UpdateValidate(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     验证一个对象是否符合创建业务规则
        /// </summary>
        /// <param name="obj">需要验证的对象</param>
        /// <returns>返回验证结果</returns>
        public ValidateResult CreateValidate(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}