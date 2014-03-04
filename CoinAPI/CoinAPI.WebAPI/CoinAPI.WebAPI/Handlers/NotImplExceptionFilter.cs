using CoinAPI.CommonFramework.Enums;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Web.Http.Filters;

namespace CoinAPI.WebAPI.Handlers
{
    /// <summary>
    ///     全局异常捕获过滤器
    /// </summary>
    public class NotImplExceptionFilter : ExceptionFilterAttribute
    {
        #region Methods
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is NotImplementedException)
            {
                context.Response.Content = new StringContent(JsonConvert.SerializeObject(new ErrorModel { ErrorId = SystemErrors.Unknown, Desc = ErrorManager.GetErrorMessageById(SystemErrors.Unknown) }));
            }
        } 
        #endregion
    }
}