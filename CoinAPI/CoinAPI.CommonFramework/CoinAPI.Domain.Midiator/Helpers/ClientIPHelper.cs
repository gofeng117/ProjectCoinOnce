using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using KJFramework.Tracing;

namespace CoinAPI.Domain.Midiator.Helpers
{
    /// <summary>
    ///     客户端IP帮助类
    /// </summary>
    public class ClientIPHelper
    {
        #region Members

        private static readonly ITracing _tracing = TracingManager.GetTracing(typeof(ClientIPHelper));

        /// <summary>
        ///     获取当前客户端IP
        /// </summary>
        public static IPEndPoint ClientIPv4 {
            get { return GetLoginClientIPv4(); }
        }

        #endregion

        #region Method

        /// <summary>
        ///     获得登录端的IPv4
        /// <para>*当返回值为空则说明ip不合法或者为线程调用</para>
        /// </summary>
        /// <returns>ip</returns>
        private static IPEndPoint GetLoginClientIPv4()
        {
            IPEndPoint ipEndPoint;
            try
            {
                string ip = string.Empty;
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                    {
                        if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
                        {
                            if (HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] != null)
                                ip = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
                            else
                                if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                        }
                        else
                            ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    }
                    else if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] != null)
                        ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    string hostName = Dns.GetHostName();
                    IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
                    IPAddress ipAddress = null;
                    for (int i = 0; i < ipEntry.AddressList.Length; i++)
                    {
                        if (ipEntry.AddressList[i].AddressFamily.ToString() == "InterNetwork")
                        {
                            ipAddress = ipEntry.AddressList[i];
                            break;
                        }
                    }
                    if (ipAddress != null) ip = ipAddress.Address.ToString();
                }
                ipEndPoint = string.IsNullOrEmpty(ip) ? null : new IPEndPoint(IPAddress.Parse(ip).MapToIPv4(), 0);
            }
            catch (Exception ex)
            {
                ipEndPoint = null;
                _tracing.Error(ex, null);
            }
            return ipEndPoint;
        }

        #endregion

    }
}
