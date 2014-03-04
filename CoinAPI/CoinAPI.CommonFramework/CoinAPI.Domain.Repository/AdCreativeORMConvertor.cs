using System;
using System.Data;
using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.Domain.Business.Actions;
using MobiSage.AdsAPI.Domain.Business.CoreAreas;
using MobiSage.AdsAPI.Domain.Enums;
using MobiSage.AdsAPI.Domain.ProtocolMetadata;
using MobiSage.AdsAPI.Domain.ProtocolMetadata.Themes.Templates;
using MobiSage.AdsAPI.Domain.Utilities;

namespace MobiSage.AdsAPI.Domain.Repository
{
    /// <summary>
    ///     创意对象数据映射转换器
    /// </summary>
    public class AdCreativeORMConvertor
    {
        #region Members

        public static readonly AdCreativeORMConvertor Instance = new AdCreativeORMConvertor();

        #endregion

        #region Methods

        /// <summary>
        ///     将关系型数据转化成领域对象
        /// </summary>
        /// <param name="dataRow">关系型数据对象</param>
        /// <returns>领域对象实例</returns>
        public ImageAdCreative ConvertToImageCreative(DataRow dataRow)
        {
            if (dataRow == null) throw new ArgumentNullException("dataRow");
            ImageAdCreative adCreative = ImageAdCreative.Create(ulong.Parse(dataRow["CampaignID"].ToString()),
                                                                ulong.Parse(dataRow["AdGroupID"].ToString()),
                                                                ulong.Parse(dataRow["AdCreativeID"].ToString()),
                                                                ulong.Parse(dataRow["Weight"].ToString()),
                                                                new AdAction(uint.Parse(dataRow["AdActionID"].ToString())));
            adCreative.MediaTypeId = ushort.Parse(dataRow["MediaTypeID"].ToString());
            adCreative.DestinationUrl = Convert.IsDBNull(dataRow["DestinationURL"]) ? null : (string)dataRow["DestinationURL"];
            adCreative.AdCreativeDeliveryType = (CreativeDeliveryTypes)byte.Parse(dataRow["AdCreativeDeliveryType"].ToString());
            adCreative.Status = (AdStatus)byte.Parse(dataRow["Status"].ToString());
            adCreative.Name = Convert.IsDBNull(dataRow["AdCreativeName"]) ? null : (string)dataRow["AdCreativeName"];
            adCreative.ThirdPartyTrackingUrl = Convert.IsDBNull(dataRow["TrackingURL"]) ? null : (string)dataRow["TrackingURL"];
            adCreative.PackageName = Convert.IsDBNull(dataRow["PackageName"]) ? null : (string)dataRow["PackageName"];
            adCreative.IsAutoOpen = !Convert.IsDBNull(dataRow["IsAutoopen"]) && byte.Parse(dataRow["IsAutoopen"].ToString()) == 1;
            adCreative.ImageUrl = Convert.IsDBNull(dataRow["ImageURL"]) ? null : (string)dataRow["ImageURL"];
            adCreative.DownloadType = byte.Parse(dataRow["DownloadType"].ToString()) == 1;
            adCreative.Size = new AdSize
            {
                Height = double.Parse(dataRow["Height"].ToString()),
                Width = double.Parse(dataRow["Width"].ToString()),
                ImageSizeId = ushort.Parse(dataRow["ImageSize"].ToString())
            };
            adCreative.CoreArea = new CoreArea
            {
                Width = !Convert.IsDBNull(dataRow["CoreWidth"]) ? float.Parse(dataRow["CoreWidth"].ToString()) : 0F,
                Height = !Convert.IsDBNull(dataRow["CoreHeight"]) ? float.Parse(dataRow["CoreHeight"].ToString()) : 0F,
                CenterX = !Convert.IsDBNull(dataRow["CorePointX"]) ? float.Parse(dataRow["CorePointX"].ToString()) : 0F,
                CenterY = !Convert.IsDBNull(dataRow["CorePointY"]) ? float.Parse(dataRow["CorePointY"].ToString()) : 0F
            };
            return adCreative;
        }

        /// <summary>
        ///     将关系型数据转化成领域对象
        /// </summary>
        /// <param name="dataRow">关系型数据对象</param>
        /// <returns>领域对象实例</returns>
        public VideoAdCreative ConvertToVideoCreative(DataRow dataRow)
        {
            if (dataRow == null) throw new ArgumentNullException("dataRow");
            VideoAdCreative adCreative = VideoAdCreative.Create(ulong.Parse(dataRow["CampaignID"].ToString()),
                                                                ulong.Parse(dataRow["AdGroupID"].ToString()),
                                                                ulong.Parse(dataRow["AdCreativeID"].ToString()),
                                                                ulong.Parse(dataRow["Weight"].ToString()),
                                                                new AdAction(uint.Parse(dataRow["AdActionID"].ToString())));
            adCreative.MediaTypeId = ushort.Parse(dataRow["MediaTypeID"].ToString());
            adCreative.VideoUrl = Convert.IsDBNull(dataRow["DestinationURL"]) ? null : (string)dataRow["DestinationURL"];
            adCreative.AdCreativeDeliveryType = (CreativeDeliveryTypes)byte.Parse(dataRow["AdCreativeDeliveryType"].ToString());
            adCreative.Status = (AdStatus)byte.Parse(dataRow["Status"].ToString());
            adCreative.Name = Convert.IsDBNull(dataRow["AdCreativeName"]) ? null : (string)dataRow["AdCreativeName"];
            adCreative.ThirdPartyTrackingUrl = Convert.IsDBNull(dataRow["TrackingURL"]) ? null : (string)dataRow["TrackingURL"];
            adCreative.PackageName = Convert.IsDBNull(dataRow["PackageName"]) ? null : (string)dataRow["PackageName"];
            adCreative.IsAutoOpen = byte.Parse(dataRow["IsAutoopen"].ToString()) == 1;
            adCreative.VideoPreviewImageUrl = Convert.IsDBNull(dataRow["ImageURL"]) ? null : (string)dataRow["ImageURL"];
            adCreative.Size = new AdSize
            {
                ImageSizeId = ushort.Parse(dataRow["ImageSize"].ToString())
            };
            return adCreative;
        }


        /// <summary>
        ///     将关系型数据转化成领域对象
        /// </summary>
        /// <param name="dataRow">关系型数据对象</param>
        /// <returns>领域对象实例</returns>
        public RecommandAdCreative ConvertToRecommandCreative(DataRow dataRow)
        {
            if (dataRow == null) throw new ArgumentNullException("dataRow");
            RecommandAdCreative adCreative = RecommandAdCreative.Create(ulong.Parse(dataRow["CampaignID"].ToString()),
                                                                ulong.Parse(dataRow["AdGroupID"].ToString()),
                                                                ulong.Parse(dataRow["AdCreativeID"].ToString()),
                                                                ulong.Parse(dataRow["Weight"].ToString()),
                                                                new AdAction(uint.Parse(dataRow["AdActionID"].ToString())));
            adCreative.MediaTypeId = ushort.Parse(dataRow["MediaTypeID"].ToString());
            adCreative.AppUrl = Convert.IsDBNull(dataRow["AppURL"]) ? null : (string)dataRow["AppURL"];
            adCreative.AdCreativeDeliveryType = (CreativeDeliveryTypes)byte.Parse(dataRow["AdCreativeDeliveryType"].ToString());
            adCreative.Status = (AdStatus)byte.Parse(dataRow["Status"].ToString());
            adCreative.Name = Convert.IsDBNull(dataRow["AdCreativeName"]) ? null : (string)dataRow["AdCreativeName"];
            adCreative.ThirdPartyTrackingUrl = Convert.IsDBNull(dataRow["TrackingURL"]) ? null : (string)dataRow["TrackingURL"];
            adCreative.PackageName = Convert.IsDBNull(dataRow["PackageName"]) ? null : (string)dataRow["PackageName"];
            adCreative.IsAutoOpen = !Convert.IsDBNull(dataRow["IsAutoopen"]) && (byte.Parse(dataRow["IsAutoopen"].ToString()) == 1);
            adCreative.DestinationUrl = dataRow["DestinationURL"].ToString();
            adCreative.Size = new AdSize();
            adCreative.AppInfo = new AppStoreInfo
            {
                Name = Convert.IsDBNull(dataRow["AppName"]) ? null : (string)dataRow["AppName"],
                AppIcon = Convert.IsDBNull(dataRow["AppIcon"]) ? null : (string)dataRow["AppIcon"],
                Category = Convert.IsDBNull(dataRow["AppCategory"]) ? null : (string)dataRow["AppCategory"],
                Description = Convert.IsDBNull(dataRow["AppDesc"]) ? null : (string)dataRow["AppDesc"],
                Version = Convert.IsDBNull(dataRow["AppVersion"]) ? null : (string)dataRow["AppVersion"]
            };
            ulong result1;
            decimal result2;
            uint result3;
            float result4;
            if (!Convert.IsDBNull(dataRow["DownloadNumber"]) && ulong.TryParse(dataRow["DownloadNumber"].ToString(), out result1)) adCreative.AppInfo.DownloadCount = result1;
            if (!Convert.IsDBNull(dataRow["AppFileSize"]) && ulong.TryParse(dataRow["AppFileSize"].ToString(), out result1)) adCreative.AppInfo.FileSize = result1;
            if (!Convert.IsDBNull(dataRow["AppRate"]) && float.TryParse(dataRow["AppRate"].ToString(), out result4)) adCreative.AppInfo.Grade = result4;
            if (!Convert.IsDBNull(dataRow["AppRateNumber"]) && ulong.TryParse(dataRow["AppRateNumber"].ToString(), out result1)) adCreative.AppInfo.GradeCount = result1;
            if (!Convert.IsDBNull(dataRow["Points"]) && uint.TryParse(dataRow["Points"].ToString(), out result3)) adCreative.AppInfo.Points = result3;
            if (!Convert.IsDBNull(dataRow["AppPrice"]) && decimal.TryParse(dataRow["AppPrice"].ToString(), out result2)) adCreative.AppInfo.Price = result2;
            adCreative.AppInfo.AppId = AppStoreUtils.GetAdCreativeExtendsAppId(adCreative.AppUrl);
            return adCreative;
        }


        /// <summary>
        ///     将关系型数据转化成领域对象
        /// </summary>
        /// <param name="dataRow">关系型数据对象</param>
        /// <returns>领域对象实例</returns>
        public TemplateSizeData ConvertToTemplateSizeCreative(DataRow dataRow)
        {
            if (dataRow == null) throw new ArgumentNullException("dataRow");
            TemplateSizeData templateSize = new TemplateSizeData();

            templateSize.ThemeId = ushort.Parse(dataRow["ThemeID"].ToString());
            templateSize.TemplateId = ushort.Parse(dataRow["TemplateID"].ToString());
            templateSize.SizeId = ushort.Parse(dataRow["SizeID"].ToString());
            templateSize.Width = double.Parse(dataRow["Width"].ToString());
            templateSize.Height = double.Parse(dataRow["Height"].ToString());
            return templateSize;
        }

        /// <summary>
        ///     将关系型数据转化成领域对象
        /// </summary>
        /// <param name="dataRow">关系型数据对象</param>
        /// <returns>领域对象实例</returns>
        public AdCreativeThemeData ConvertToAdCreativeTheme(DataRow dataRow)
        {
            if (dataRow == null) throw new ArgumentNullException("dataRow");
            AdCreativeThemeData adCreativeThemeData = new AdCreativeThemeData();
            adCreativeThemeData.Authority = ushort.Parse(dataRow["Authority"].ToString());
            adCreativeThemeData.CreateDate = Convert.ToDateTime(dataRow["CreateDate"]);
            adCreativeThemeData.Creator = ulong.Parse(dataRow["Creator"].ToString());
            adCreativeThemeData.DataType = ushort.Parse(dataRow["DataType"].ToString());
            adCreativeThemeData.Desc = dataRow["Desc"].ToString();
            adCreativeThemeData.MediaType = ushort.Parse(dataRow["MediaType"].ToString());
            adCreativeThemeData.ModifyBy = ulong.Parse(dataRow["ModifyBy"].ToString());
            adCreativeThemeData.ModifyDate = Convert.ToDateTime(dataRow["ModifyDate"]);
            adCreativeThemeData.Name = dataRow["Name"].ToString();
            adCreativeThemeData.ThemeId = ushort.Parse(dataRow["ThemeId"].ToString());
            adCreativeThemeData.ThemeURL = dataRow["ThemeURL"].ToString();
            return adCreativeThemeData;
        }

        /// <summary>
        ///     将关系型数据转化成领域对象
        /// </summary>
        /// <param name="dataRow">关系型数据对象</param>
        /// <returns>领域对象实例</returns>
        public CustomElementData ConvertToCustomElement(DataRow dataRow)
        {
            if (dataRow == null) throw new ArgumentNullException("dataRow");
            CustomElementData customElementData = new CustomElementData
            {
                ElementId = ushort.Parse(dataRow["ElementID"].ToString()),
                Name = Convert.IsDBNull(dataRow["Name"]) ? null : (string)dataRow["Name"],
                Key = Convert.IsDBNull(dataRow["Key"]) ? null : (string)dataRow["Key"],
                DataType = ushort.Parse(dataRow["DataType"].ToString()),
                ControlType = ushort.Parse(dataRow["ControlType"].ToString()),
                RegString = Convert.IsDBNull(dataRow["RegString"]) ? null : (string)dataRow["RegString"],
                Comment = Convert.IsDBNull(dataRow["Comment"]) ? null : (string)dataRow["Comment"],
                IsRequired = bool.Parse(dataRow["IsRequired"].ToString()),
                Creator = ulong.Parse(dataRow["Creator"].ToString()),
                CreateDate = DateTime.Parse(dataRow["CreateDate"].ToString()),
                ModifyBy = ulong.Parse(dataRow["ModifyBy"].ToString()),
                ModifyDate = DateTime.Parse(dataRow["ModifyDate"].ToString()),
                ListDic = Convert.IsDBNull(dataRow["ListDic"]) ? null : (string)dataRow["ListDic"]
            };
            return customElementData;
        }

        /// <summary>
        ///     将关系型数据转化成领域对象
        /// </summary>
        /// <param name="dataRow">关系型数据对象</param>
        /// <returns>领域对象实例</returns>
        public CustomElementWithValueData ConvertToCustomElementWithValue(DataRow dataRow)
        {
            if (dataRow == null) throw new ArgumentNullException("dataRow");
            CustomElementWithValueData customElementWithValueData = new CustomElementWithValueData
            {
                AdCreativeId = ulong.Parse(dataRow["AdCreativeID"].ToString()),
                ElementId = ushort.Parse(dataRow["ElementID"].ToString()),
                Value = Convert.IsDBNull(dataRow["Value"]) ? null : (string)dataRow["Value"],
                Name = Convert.IsDBNull(dataRow["Name"]) ? null : (string)dataRow["Name"],
                Key = Convert.IsDBNull(dataRow["Key"]) ? null : (string)dataRow["Key"],
                DataType = ushort.Parse(dataRow["DataType"].ToString()),
                ControlType = ushort.Parse(dataRow["ControlType"].ToString()),
                RegString = Convert.IsDBNull(dataRow["RegString"]) ? null : (string)dataRow["RegString"],
                Comment = Convert.IsDBNull(dataRow["Comment"]) ? null : (string)dataRow["Comment"],
                IsRequired = bool.Parse(dataRow["IsRequired"].ToString()),
                ListDic = Convert.IsDBNull(dataRow["ListDic"]) ? null : (string)dataRow["ListDic"],
                Flag = bool.Parse(dataRow["flag"].ToString())
            };
            return customElementWithValueData;
        }

        #endregion
    }
}