using System.Threading;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CoinAPI.WebAPI.Handlers
{
    /// <summary>
    ///     入口请求，权限判断
    /// </summary>
    internal class AuthorizationCheckHandler : DelegatingHandler
    {
        #region Methods

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string authToken = HttpContext.Current.Request.Cookies.Get("Token") == null ? null : HttpContext.Current.Request.Cookies.Get("Token").Value;
            if (request.RequestUri.LocalPath.ToLower() != "/api/user" && string.IsNullOrEmpty(authToken))
            {
                return Task.Factory.StartNew(() =>
                {
                    HttpResponseMessage rspMessage = new HttpResponseMessage(HttpStatusCode.Accepted);
                    if (rspMessage.Content != null)
                        rspMessage.Content.Headers.ContentType = request.Content.Headers.ContentType;
                    rspMessage.Content = new StringContent(JsonConvert.SerializeObject(new ErrorModel { ErrorId = 402, Desc = ErrorManager.GetErrorMessageById(402) }));
                    return rspMessage;
                });
            }
            return base.SendAsync(request, cancellationToken);
        }

        #endregion
    }
}