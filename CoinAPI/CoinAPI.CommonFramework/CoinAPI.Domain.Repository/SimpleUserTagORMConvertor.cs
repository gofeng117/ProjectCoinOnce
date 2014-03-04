using System;
using System.Data;
using MobiSage.AdsAPI.Domain.Business.Dmp;
using MobiSage.AdsAPI.Domain.Enums;

namespace MobiSage.AdsAPI.Domain.Repository
{
    /// <summary>
    ///     简易版标签ORM转换器
    /// </summary>
    public class SimpleUserTagORMConvertor : IORMConvertor<SimpleUserTag>
    {
        #region Members

        public static readonly SimpleUserTagORMConvertor Instance = new SimpleUserTagORMConvertor();

        #endregion

        #region Implementation of IORMConvertor<SimpleUserTag>

        /// <summary>
        ///     将关系型数据转化成领域对象
        /// </summary>
        /// <param name="dataRow">关系型数据对象</param>
        /// <returns>领域对象实例</returns>
        public SimpleUserTag ConvertToDomain(DataRow dataRow)
        {
            if (dataRow == null) return null;
            return new SimpleUserTag(ushort.Parse(dataRow["UserTagID"].ToString()), (Convert.IsDBNull(dataRow["UserTag"]) ? null : dataRow["UserTag"].ToString()), (UserTagDirectionTypes) byte.Parse(dataRow["Direction"].ToString()));
        }

        #endregion
    }
}