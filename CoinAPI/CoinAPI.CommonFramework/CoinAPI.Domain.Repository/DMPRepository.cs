using System;
using System.Data;
using KJFramework.Tracing;
using MobiSage.AdsAPI.CommonFramework.DbAccess;
using MobiSage.AdsAPI.CommonFramework.Enums;
using MobiSage.AdsAPI.Domain.Business.Dmp;
using MobiSage.AdsAPI.Domain.Midiator.Extentions;
using MobiSage.AdsAPI.Domain.Repository.Objects;
using MobiSage.AdsAPI.Domain.Results;

namespace MobiSage.AdsAPI.Domain.Repository
{
    /// <summary>
    ///     DMP仓储
    /// </summary>
    public class DMPRepository : IDMPRepository
    {
        #region Members

        private static Database _masterDB;
        private static Database _slaveDB;
        private static readonly ITracing _tracing = TracingManager.GetTracing(typeof(CampaignRepository));
        private static IORMConvertor<SimpleUserTag> _convertor;
        private static IORMConvertor<SimpleAdGroup> _otherConvertor;

        #endregion

        #region Implementation of IDMPRepository

        /// <summary>
        ///     获取指定活动所绑定的标签信息集合
        /// </summary>
        /// <param name="campaignId">活动编号</param>
        /// <returns>执行结果</returns>
        public IExecuteResult<SimpleUserTag[]> GetUserTagsByCampaignId(ulong campaignId)
        {
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { campaignId };
                //验证CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetUserTagsByCampaignId, ParametersObject.GetUserTagsByCampaignId, parameterValues);
                if (dt.Rows.Count <= 0) return ExecuteResult<SimpleUserTag[]>.Succeed(null);
                SimpleUserTag[] tags = new SimpleUserTag[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                    tags[i] = _convertor.ConvertToDomain(dt.Rows[i]);
                return ExecuteResult<SimpleUserTag[]>.Succeed(tags);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult<SimpleUserTag[]>.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     获取指定广告组所绑定的标签信息集合
        /// </summary>
        /// <param name="adgroupId">广告组编号</param>
        /// <returns>执行结果</returns>
        public IExecuteResult<SimpleUserTag[]> GetUserTagsByAdGroupId(ulong adgroupId)
        {
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { adgroupId };
                //验证CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetUserTagsByAdGroupId, ParametersObject.GetUserTagsByAdGroupId, parameterValues);
                if (dt.Rows.Count <= 0) return ExecuteResult<SimpleUserTag[]>.Succeed(null);
                SimpleUserTag[] tags = new SimpleUserTag[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                    tags[i] = _convertor.ConvertToDomain(dt.Rows[i]);
                return ExecuteResult<SimpleUserTag[]>.Succeed(tags);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult<SimpleUserTag[]>.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     检查指定活动下是否存在不具有启动资格的广告组信息
        /// </summary>
        /// <param name="campaignId">活动编号</param>
        /// <returns>返回执行结果</returns>
        public IExecuteResult<SimpleAdGroup[]> CheckRunStatusByCampaignId(ulong campaignId)
        {
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { campaignId };
                //验证CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpCheckRunStatusByCampaignId, ParametersObject.CheckRunStatusByCampaignId, parameterValues);
                if (dt.Rows.Count <= 0) return ExecuteResult<SimpleAdGroup[]>.Succeed(null);
                SimpleAdGroup[] tags = new SimpleAdGroup[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                    tags[i] = _otherConvertor.ConvertToDomain(dt.Rows[i]);
                return ExecuteResult<SimpleAdGroup[]>.Succeed(tags);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult<SimpleAdGroup[]>.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     获取指定活动下的所有标签编号
        /// </summary>
        /// <param name="campaignId">活动编号</param>
        /// <returns>返回执行结果</returns>
        public IExecuteResult<ushort[]> GetTagIdByCampaignId(ulong campaignId)
        {
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { campaignId };
                //验证CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpGetTagIdByCampaignId, ParametersObject.GetTagIdByCampaignId, parameterValues);
                if (dt.Rows.Count <= 0) return ExecuteResult<ushort[]>.Succeed(null);
                ushort[] tags = new ushort[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++) tags[i] = ushort.Parse(dt.Rows[i]["Id"].ToString());
                return ExecuteResult<ushort[]>.Succeed(tags);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult<ushort[]>.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        /// <summary>
        ///     检查指定广告组编号集合中是否存在不具有启动资格的广告组信息
        /// </summary>
        /// <param name="adgroupIds">广告组编号集合</param>
        /// <returns>返回执行结果</returns>
        public IExecuteResult<SimpleAdGroup[]> CheckRunStatusByAdGroupIds(ulong[] adgroupIds)
        {
            try
            {
                //获取参数的值
                object[] parameterValues = new object[] { adgroupIds.Concat() };
                //验证CampaignId和AccountId从属关系
                DataTable dt = _slaveDB.SpExecuteTable(SpName.SpCheckRunStatusByAdGroupIds, ParametersObject.CheckRunStatusByAdGroupIds, parameterValues);
                if (dt.Rows.Count <= 0) return ExecuteResult<SimpleAdGroup[]>.Succeed(null);
                SimpleAdGroup[] tags = new SimpleAdGroup[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                    tags[i] = _otherConvertor.ConvertToDomain(dt.Rows[i]);
                return ExecuteResult<SimpleAdGroup[]>.Succeed(tags);
            }
            catch (Exception ex)
            {
                _tracing.Error(ex, null);
                return ExecuteResult<SimpleAdGroup[]>.Fail(SystemErrors.Unknown, ex.Message);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     静态注入db实例
        /// </summary>
        /// <param name="masterDB">主DB</param>
        /// <param name="slaveDB">从DB</param>
        /// <param name="convertor">对象转换器</param>
        /// <param name="otherConvertor">简易版广告组对象转换器</param>
        public static void Inject(Database masterDB, Database slaveDB, IORMConvertor<SimpleUserTag> convertor, IORMConvertor<SimpleAdGroup> otherConvertor)
        {
            if (null == masterDB) throw new ArgumentNullException("masterDB");
            if (null == convertor) throw new ArgumentNullException("convertor");
            if (null == otherConvertor) throw new ArgumentNullException("otherConvertor");
            _masterDB = masterDB;
            _slaveDB = slaveDB;
            _convertor = convertor;
            _otherConvertor = otherConvertor;
        }

        #endregion
    }
}