using System;
using System.IO;
using System.Net;
using NUnit.Framework;

namespace CoinAPI.WebAPI.UnitTest
{
    /// <summary>
    ///     website现有业务单元测试类
    /// </summary>
    public class WebSiteUnit
    {

        #region Members
        /// <summary>
        ///     UAS令牌值
        /// </summary>
        private static string token = string.Empty;
        private static string htmlUrl = string.Empty;
        private static string imageUrl = string.Empty;
        private static byte cid;
        private static byte pid;
        private static ulong pushceid = 0;
        private static ulong htmlceid = 0;

        #endregion

        #region Methods

        [Test]
        public void GetCampaignById()
        {
            cid = 0;
            pid = 0;
            StreamReader read = null;
            string responseText = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://192.168.1.106:8181/api/CoinAPI?cid=" + cid +"&pid=" + pid);
            request.Method = "Get";
            request.Headers.Clear();
            request.ContentType = "application/json; charset=UTF-8";
            request.ContentLength = 0;
            HttpWebResponse response1 = (HttpWebResponse)request.GetResponse();
            Assert.IsNotNull(response1);
            Assert.IsTrue(response1.StatusCode == HttpStatusCode.OK);
            Console.WriteLine(response1.StatusCode);
            var headers = response1.Headers;
            Assert.IsTrue(headers.Count > 0);
            foreach (var item in headers.AllKeys)
            {
                Console.WriteLine(item + ":" + headers[item]);
            }
            Stream stream = response1.GetResponseStream();
            using (read = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8))
            {
                responseText += read.ReadToEnd();
            }
            Assert.IsNotNullOrEmpty(responseText);
            Assert.IsTrue(response1.StatusCode == (HttpStatusCode)200);
            Console.WriteLine("Campaign 相关信息:" + responseText);
        }

        #endregion
    }
}
