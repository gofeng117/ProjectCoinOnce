using CoinAPI.CommonFramework.Enums;
using System;

namespace CoinAPI.WebAPI.Handlers
{
    /// <summary>
    ///     异常处理器
    /// </summary>
    public class ExceptionManager
    {
        #region Methods

        /// <summary>
        ///     异常处理信息
        /// </summary>
        /// <param name="ex">捕获到的系统异常信息</param>
        /// <returns>返回错误的结构实体ErrorModel</returns>
        public static ErrorModel HandleException(Exception ex)
        {
            switch (ex.GetType().Name)
            {
                case "RelationNotFoundException"://指定的关系不能在数据库中被找到
                    return new ErrorModel {ErrorId = 404, Desc = ErrorManager.GetErrorMessageById(404)};
                case "MatchSizeNotFoundException"://无法找到同等尺寸的大小数据
                    return new ErrorModel {ErrorId = 406, Desc = ErrorManager.GetErrorMessageById(406)};
                case "FileTooLargeException"://上传文件太大
                    return new ErrorModel {ErrorId = 403,Desc = ErrorManager.GetErrorMessageById(403)};
                case "BadImageFormatException"://图片尺寸不正确
                    return new ErrorModel {ErrorId = 405,Desc = ErrorManager.GetErrorMessageById(405)};
                case "ArgumentNullException"://参数不能为空
                    return new ErrorModel { ErrorId = 407, Desc = ex.Message };
                default://系统内部错误异常
                    return new ErrorModel { ErrorId = SystemErrors.Unknown, Desc = ErrorManager.GetErrorMessageById(SystemErrors.Unknown) };
            }
        } 

        #endregion
    }
}