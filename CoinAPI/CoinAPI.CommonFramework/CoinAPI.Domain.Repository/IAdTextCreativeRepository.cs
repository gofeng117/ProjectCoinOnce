using System;
using MobiSage.AdsAPI.Domain.Business;
using MobiSage.AdsAPI.Domain.Results;

namespace MobiSage.AdsAPI.Domain.Repository
{
    /// <summary>
    ///     文字创意仓储接口
    /// </summary>
    public interface IAdTextCreativeRepository
    {
        /// <summary>
        ///     创建一个新的文字创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">文字创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        IExecuteResult CreateTextCreative(uint userId, ulong accountId, TextAdCreative adCreative);
        /// <summary>
        ///     更新一个文字创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">文字创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        IExecuteResult UpdateTextCreative(uint userId, ulong accountId, TextAdCreative adCreative);
        /// <summary>
        ///     根据指定编号获取一个文字创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="adCreativeId">广告创意编号</param>
        /// <returns>返回执行后的结果</returns>
        IExecuteResult GetAdTextCreativeById(uint userId, ulong adCreativeId);
        /// <summary>
        ///     创建一个新的荐计划创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">荐计划创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        IExecuteResult CreateNewRecommandCreative(uint userId, ulong accountId, RecommandAdCreative adCreative);
        /// <summary>
        ///     更新一个荐计划创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="accountId">账户编号</param>
        /// <param name="adCreative">荐计划创意实体</param>
        /// <returns>返回执行后的结果</returns>
        /// <exception cref="ArgumentNullException">参数不能为空</exception>
        IExecuteResult UpdateRecommandCreative(uint userId, ulong accountId, RecommandAdCreative adCreative);
        /// <summary>
        ///     根据指定编号获取一个荐计划创意
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="adCreativeId">荐计划创意编号</param>
        /// <returns>返回执行后的结果</returns>
        IExecuteResult GetRecommandCreativeById(uint userId, ulong adCreativeId);
    }
}