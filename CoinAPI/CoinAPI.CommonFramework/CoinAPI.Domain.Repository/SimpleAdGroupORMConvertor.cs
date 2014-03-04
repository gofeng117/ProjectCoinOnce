using System;
using System.Data;
using MobiSage.AdsAPI.Domain.Business.Dmp;
using MobiSage.AdsAPI.Domain.Enums;

namespace MobiSage.AdsAPI.Domain.Repository
{
    /// <summary>
    ///     简易版广告组ORM转换器
    /// </summary>
    public class SimpleAdGroupORMConvertor : IORMConvertor<SimpleAdGroup>
    {
        #region Members

        public static readonly SimpleAdGroupORMConvertor Instance = new SimpleAdGroupORMConvertor();

        #endregion

        #region Implementation of IORMConvertor<SimpleUserTag>

        /// <summary>
        ///     将关系型数据转化成领域对象
        /// </summary>
        /// <param name="dataRow">关系型数据对象</param>
        /// <returns>领域对象实例</returns>
        public SimpleAdGroup ConvertToDomain(DataRow dataRow)
        {
            if (dataRow == null) return null;
            return new SimpleAdGroup(ushort.Parse(dataRow["AdGroupId"].ToString()),
                                     (Convert.IsDBNull(dataRow["Name"]) ? null : dataRow["Name"].ToString()),
                                     (AdGroupTagStatusTypes) byte.Parse(dataRow["Status"].ToString()),
                                     (UserTagDirectionTypes) byte.Parse(dataRow["Direction"].ToString()));
        }

        #endregion
    }
}