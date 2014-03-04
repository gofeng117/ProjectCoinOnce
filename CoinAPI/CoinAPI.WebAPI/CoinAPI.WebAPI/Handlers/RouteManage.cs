using System.Collections.Generic;
using System.Web.Http.Routing;

namespace CoinAPI.WebAPI.Handlers
{
    /// <summary>
    ///     路由匹配管理器
    /// </summary>
    public class RouteManage : IHttpRouteConstraint
    {
        #region Methods

        /// <summary>
        ///     controller跟action的选择
        /// </summary>
        /// <param name="request">Http请求信息</param>
        /// <param name="route">路由表</param>
        /// <param name="parameterName">action参数</param>
        /// <param name="values">路由字典</param>
        /// <param name="routeDirection">路由指向</param>
        /// <returns>返回true</returns>
        public bool Match(System.Net.Http.HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (values[parameterName] == null && values["controller"].ToString().ToLower() == "user" && request.Method.ToString().ToLower() != "get")
                values[parameterName] = request.Method.ToString();
            return true;
        }

        #endregion
    }

}