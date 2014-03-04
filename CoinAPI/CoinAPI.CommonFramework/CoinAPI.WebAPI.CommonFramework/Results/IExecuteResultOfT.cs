using CoinAPI.Domain.Enums;

namespace CoinAPI.CommonFramework.Results
{
    /// <summary>
    ///     ִ�н���ӿ�
    /// </summary>
    /// <typeparam name="T">����ֵ����</typeparam>
    public interface IExecuteResult<T>
    {
        /// <summary>
        ///     ��ȡִ�н����״̬
        /// </summary>
        ExecuteResults State { get; }
        /// <summary>
        ///     ��ȡ�ڲ�ϵͳ�Ĵ�����
        /// </summary>
        byte ErrorId { get; }
        /// <summary>
        ///     ��ȡ������Ϣ
        /// </summary>
        string Error { get; }
        /// <summary>
        ///     ��ȡ�ڲ��������ĵ��ý������
        /// </summary>
        /// <typeparam name="T">���ý�����������</typeparam>
        /// <returns>���ص��ý��</returns>
        T GetResult();
    }
}