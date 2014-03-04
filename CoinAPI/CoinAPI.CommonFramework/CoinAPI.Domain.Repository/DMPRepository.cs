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
    ///     DMP�ִ�
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
        ///     ��ȡָ������󶨵ı�ǩ��Ϣ����
        /// </summary>
        /// <param name="campaignId">����</param>
        /// <returns>ִ�н��</returns>
        public IExecuteResult<SimpleUserTag[]> GetUserTagsByCampaignId(ulong campaignId)
        {
            try
            {
                //��ȡ������ֵ
                object[] parameterValues = new object[] { campaignId };
                //��֤CampaignId��AccountId������ϵ
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
        ///     ��ȡָ����������󶨵ı�ǩ��Ϣ����
        /// </summary>
        /// <param name="adgroupId">�������</param>
        /// <returns>ִ�н��</returns>
        public IExecuteResult<SimpleUserTag[]> GetUserTagsByAdGroupId(ulong adgroupId)
        {
            try
            {
                //��ȡ������ֵ
                object[] parameterValues = new object[] { adgroupId };
                //��֤CampaignId��AccountId������ϵ
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
        ///     ���ָ������Ƿ���ڲ����������ʸ�Ĺ������Ϣ
        /// </summary>
        /// <param name="campaignId">����</param>
        /// <returns>����ִ�н��</returns>
        public IExecuteResult<SimpleAdGroup[]> CheckRunStatusByCampaignId(ulong campaignId)
        {
            try
            {
                //��ȡ������ֵ
                object[] parameterValues = new object[] { campaignId };
                //��֤CampaignId��AccountId������ϵ
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
        ///     ��ȡָ����µ����б�ǩ���
        /// </summary>
        /// <param name="campaignId">����</param>
        /// <returns>����ִ�н��</returns>
        public IExecuteResult<ushort[]> GetTagIdByCampaignId(ulong campaignId)
        {
            try
            {
                //��ȡ������ֵ
                object[] parameterValues = new object[] { campaignId };
                //��֤CampaignId��AccountId������ϵ
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
        ///     ���ָ��������ż������Ƿ���ڲ����������ʸ�Ĺ������Ϣ
        /// </summary>
        /// <param name="adgroupIds">������ż���</param>
        /// <returns>����ִ�н��</returns>
        public IExecuteResult<SimpleAdGroup[]> CheckRunStatusByAdGroupIds(ulong[] adgroupIds)
        {
            try
            {
                //��ȡ������ֵ
                object[] parameterValues = new object[] { adgroupIds.Concat() };
                //��֤CampaignId��AccountId������ϵ
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
        ///     ��̬ע��dbʵ��
        /// </summary>
        /// <param name="masterDB">��DB</param>
        /// <param name="slaveDB">��DB</param>
        /// <param name="convertor">����ת����</param>
        /// <param name="otherConvertor">���װ��������ת����</param>
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