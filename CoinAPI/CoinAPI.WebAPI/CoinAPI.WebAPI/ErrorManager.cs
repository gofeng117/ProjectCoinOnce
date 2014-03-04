using CoinAPI.CommonFramework.Enums;

namespace CoinAPI.WebAPI
{
    /// <summary>
    ///     错误管理器
    /// </summary>
    internal static class ErrorManager
    {
        #region Methods

        /// <summary>
        ///     根据错误编号获取相应的错误信息
        /// </summary>
        /// <param name="errorId">错误编号</param>
        /// <returns>返回错误信息</returns>
        public static string GetErrorMessageById (uint errorId)
        {
            switch (errorId)
            {
                case 401:
                    return "请求参数不正确，请检查您提交的对象参数。";
                case 402:
                    return "权限验证失败，请重新登录。";
                case 403:
                    return "您上传的文件过大。";
                case 404:
                    return "指定的关系不能在系统中被找到。";
                case 405:
                    return "您上传的图片尺寸不正确。";
                case 406:
                    return "系统中无法找到同等尺寸的大小数据。";
                case 407:
                    return "参数不能为空。";
                case 500:
                    return "系统内部错误, 请联系网站拥有者。";
                case SystemErrors.AccountExists:
                    return "当前账户已经存在。";
                case SystemErrors.AuthoriztionFail:
                    return "授权失败，请重新登录。";
                case SystemErrors.BusinessIsNotSupported:
                    return "您请求的业务在当前系统内不被支持。";
                case SystemErrors.BusinessObjError:
                    return "您请求的业务对象错误。";
                case SystemErrors.BusinessObjIsNotSupported:
                    return "您请求的业务对象内部，存在不被支持的业务数据。";
                case SystemErrors.Malformed:
                    return "您请求的业务对象内部格式不正确。";
                case SystemErrors.NotFound:
                    return "指定条件的数据未被找到。";
                case SystemErrors.SecurityQuotaFail:
                    return "很抱歉，由于您当前的请求频率过于频繁，所以我们阻止了您的本次请求，请稍后再试。";
                case SystemErrors.SensitiveWordsExist:
                    return "很抱歉，您当前的请求业务中存在敏感词汇，请仔细检查。";
                case SystemErrors.Timeout:
                    return "系统内部请求超时，请稍后再试。";
                case SystemErrors.Unknown:
                    return "系统内部请求错误，请稍后再试。";
            }
            return null;
        }

        #endregion
    }
}