using System;
using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.Domain.Results;

namespace MobiSage.AdsAPI.Domain.Repository
{
    /// <summary>
    ///     ���ִ���ִ��ӿ�
    /// </summary>
    public interface IAdTextCreativeRepository
    {
        /// <summary>
        ///     ����һ���µ����ִ���
        /// </summary>
        /// <param name="userId">�û����</param>
        /// <param name="accountId">�˻����</param>
        /// <param name="adCreative">���ִ���ʵ��</param>
        /// <returns>����ִ�к�Ľ��</returns>
        /// <exception cref="ArgumentNullException">��������Ϊ��</exception>
        IExecuteResult CreateTextCreative(uint userId, ulong accountId, TextAdCreative adCreative);
        /// <summary>
        ///     ����һ�����ִ���
        /// </summary>
        /// <param name="userId">�û����</param>
        /// <param name="accountId">�˻����</param>
        /// <param name="adCreative">���ִ���ʵ��</param>
        /// <returns>����ִ�к�Ľ��</returns>
        /// <exception cref="ArgumentNullException">��������Ϊ��</exception>
        IExecuteResult UpdateTextCreative(uint userId, ulong accountId, TextAdCreative adCreative);
        /// <summary>
        ///     ����ָ����Ż�ȡһ�����ִ���
        /// </summary>
        /// <param name="userId">�û����</param>
        /// <param name="adCreativeId">��洴����</param>
        /// <returns>����ִ�к�Ľ��</returns>
        IExecuteResult GetAdTextCreativeById(uint userId, ulong adCreativeId);
        /// <summary>
        ///     ����һ���µļ��ƻ�����
        /// </summary>
        /// <param name="userId">�û����</param>
        /// <param name="accountId">�˻����</param>
        /// <param name="adCreative">���ƻ�����ʵ��</param>
        /// <returns>����ִ�к�Ľ��</returns>
        /// <exception cref="ArgumentNullException">��������Ϊ��</exception>
        IExecuteResult CreateNewRecommandCreative(uint userId, ulong accountId, RecommandAdCreative adCreative);
        /// <summary>
        ///     ����һ�����ƻ�����
        /// </summary>
        /// <param name="userId">�û����</param>
        /// <param name="accountId">�˻����</param>
        /// <param name="adCreative">���ƻ�����ʵ��</param>
        /// <returns>����ִ�к�Ľ��</returns>
        /// <exception cref="ArgumentNullException">��������Ϊ��</exception>
        IExecuteResult UpdateRecommandCreative(uint userId, ulong accountId, RecommandAdCreative adCreative);
        /// <summary>
        ///     ����ָ����Ż�ȡһ�����ƻ�����
        /// </summary>
        /// <param name="userId">�û����</param>
        /// <param name="adCreativeId">���ƻ�������</param>
        /// <returns>����ִ�к�Ľ��</returns>
        IExecuteResult GetRecommandCreativeById(uint userId, ulong adCreativeId);
    }
}